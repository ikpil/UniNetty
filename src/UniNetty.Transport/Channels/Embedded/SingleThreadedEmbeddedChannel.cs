// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Transport.Channels.Embedded
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Net;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using UniNetty.Common;
    using UniNetty.Common.Internal.Logging;
    using UniNetty.Common.Utilities;

    public class SingleThreadedEmbeddedChannel : AbstractChannel, IEmbeddedChannel
    {
        static readonly EndPoint LOCAL_ADDRESS = new EmbeddedSocketAddress();
        static readonly EndPoint REMOTE_ADDRESS = new EmbeddedSocketAddress();

        enum State
        {
            Open,
            Active,
            Closed
        };

        static readonly IChannelHandler[] EMPTY_HANDLERS = new IChannelHandler[0];

        static readonly IInternalLogger logger = InternalLoggerFactory.GetInstance<EmbeddedChannel>();

        static readonly ChannelMetadata METADATA_NO_DISCONNECT = new ChannelMetadata(false);
        static readonly ChannelMetadata METADATA_DISCONNECT = new ChannelMetadata(true);

        readonly IEventLoop loop = new SingleThreadEventLoop();

        Queue<object> inboundMessages;
        Queue<object> outboundMessages;
        Exception lastException;
        State state;

        /// <summary>
        ///     Create a new instance with an empty pipeline.
        /// </summary>
        public SingleThreadedEmbeddedChannel(IEventLoop eventLoop = null)
            : this(EmbeddedChannelId.Instance, eventLoop, EMPTY_HANDLERS)
        {
        }

        /// <summary>
        ///     Create a new instance with an empty pipeline with the specified <see cref="IChannelId" />.
        /// </summary>
        /// <param name="channelId">The <see cref="IChannelId" /> of this channel. </param>
        public SingleThreadedEmbeddedChannel(IChannelId channelId, IEventLoop eventLoop = null)
            : this(channelId, eventLoop, EMPTY_HANDLERS)
        {
        }

        /// <summary>
        ///     Create a new instance with the pipeline initialized with the specified handlers.
        /// </summary>
        /// <param name="handlers">
        ///     The <see cref="IChannelHandler" />s that will be added to the <see cref="IChannelPipeline" />
        /// </param>
        public SingleThreadedEmbeddedChannel(IEventLoop eventLoop = null, params IChannelHandler[] handlers)
            : this(EmbeddedChannelId.Instance, eventLoop, handlers)
        {
        }

        public SingleThreadedEmbeddedChannel(IChannelId id, IEventLoop eventLoop = null, params IChannelHandler[] handlers)
            : this(id, false, eventLoop, handlers)
        {
        }

        /// <summary>Create a new instance with the pipeline initialized with the specified handlers.</summary>
        /// <param name="id">The <see cref="IChannelId" /> of this channel.</param>
        /// <param name="hasDisconnect">
        ///     <c>false</c> if this <see cref="IChannel" /> will delegate <see cref="DisconnectAsync" />
        ///     to <see cref="CloseAsync" />, <c>true</c> otherwise.
        /// </param>
        /// <param name="handlers">
        ///     The <see cref="IChannelHandler" />s that will be added to the <see cref="IChannelPipeline" />
        /// </param>
        public SingleThreadedEmbeddedChannel(IChannelId id, bool hasDisconnect, IEventLoop eventLoop = null, params IChannelHandler[] handlers)
            : this(id, hasDisconnect, true, eventLoop, handlers)
        { }

        public SingleThreadedEmbeddedChannel(IChannelId id, bool hasDisconnect, bool register, IEventLoop eventLoop = null, params IChannelHandler[] handlers)
            : base(null, id)
        {
            if (eventLoop != null)
            {
                this.loop = eventLoop;
            }

            this.Metadata = GetMetadata(hasDisconnect);
            this.Configuration = new DefaultChannelConfiguration(this);
            this.Setup(register, handlers);
        }

        public SingleThreadedEmbeddedChannel(IChannelId id, bool hasDisconnect, IChannelConfiguration config, IEventLoop eventLoop = null,
            params IChannelHandler[] handlers)
            : base(null, id)
        {
            Contract.Requires(config != null);

            if (eventLoop != null)
            {
                this.loop = eventLoop;
            }
            this.Metadata = GetMetadata(hasDisconnect);
            this.Configuration = config;
            this.Setup(true, handlers);
        }

        static ChannelMetadata GetMetadata(bool hasDisconnect) => hasDisconnect ? METADATA_DISCONNECT : METADATA_NO_DISCONNECT;

        void Setup(bool register, params IChannelHandler[] handlers)
        {
            Contract.Requires(handlers != null);

            IChannelPipeline p = this.Pipeline;
            p.AddLast(new ActionChannelInitializer<IChannel>(channel =>
            {
                IChannelPipeline pipeline = channel.Pipeline;
                foreach (IChannelHandler h in handlers)
                {
                    if (h == null)
                    {
                        break;
                        
                    }
                    pipeline.AddLast(h);
                }
            }));

            if (register)
            {
                Task future = this.loop.RegisterAsync(this);
                future.GetAwaiter().GetResult();
                Debug.Assert(future.IsCompleted);
            }
        }

        public void Register()
        {
            Task future = this.loop.RegisterAsync(this);
            // Debug.Assert(future.IsCompleted);
            this.Pipeline.AddLast(new LastInboundHandler(this.InboundMessages, this.RecordException));
            future.GetAwaiter().GetResult();
        }

        protected sealed override DefaultChannelPipeline NewChannelPipeline() => new SingleThreadedEmbeddedChannelPipeline(this);

        public override ChannelMetadata Metadata { get; }

        public override IChannelConfiguration Configuration { get; }

        /// <summary>
        ///     Returns the <see cref="Queue{T}" /> which holds all of the <see cref="object" />s that
        ///     were received by this <see cref="IChannel" />.
        /// </summary>
        public Queue<object> InboundMessages => this.inboundMessages ?? (this.inboundMessages = new Queue<object>());

        /// <summary>
        ///     Returns the <see cref="Queue{T}" /> which holds all of the <see cref="object" />s that
        ///     were written by this <see cref="IChannel" />.
        /// </summary>
        public Queue<object> OutboundMessages => this.outboundMessages ?? (this.outboundMessages = new Queue<object>());

        /// <summary>
        /// Return received data from this <see cref="IChannel"/>.
        /// </summary>
        public T ReadInbound<T>() => (T)Poll(this.inboundMessages);

        /// <summary>
        /// Read data from the outbound. This may return <c>null</c> if nothing is readable.
        /// </summary>
        public T ReadOutbound<T>() => (T)Poll(this.outboundMessages);

        protected override EndPoint LocalAddressInternal => this.Active ? LOCAL_ADDRESS : null;

        protected override EndPoint RemoteAddressInternal => this.Active ? REMOTE_ADDRESS : null;

        protected override IChannelUnsafe NewUnsafe() => new DefaultUnsafe(this);

        protected override bool IsCompatible(IEventLoop eventLoop) => true;

        protected override void DoBind(EndPoint localAddress)
        {
            //NOOP
        }

        protected override void DoRegister() => this.state = State.Active;

        protected override void DoDisconnect() => this.DoClose();

        protected override void DoClose() => this.state = State.Closed;

        protected override void DoBeginRead()
        {
            //NOOP
        }

        protected override void DoWrite(ChannelOutboundBuffer input)
        {
            for (;;)
            {
                object msg = input.Current;
                if (msg == null)
                {
                    break;
                }

                ReferenceCountUtil.Retain(msg);
                this.OutboundMessages.Enqueue(msg);
                input.Remove();
            }
        }

        public override bool Open => this.state != State.Closed;

        public override bool Active => this.state == State.Active;

        /// <summary>
        ///     Write messages to the inbound of this <see cref="IChannel" />
        /// </summary>
        /// <param name="msgs">The messages to be written.</param>
        /// <returns><c>true</c> if the write operation did add something to the inbound buffer</returns>
        public bool WriteInbound(params object[] msgs)
        {
            this.EnsureOpen();
            if (msgs.Length == 0)
            {
                return IsNotEmpty(this.inboundMessages);
            }

            IChannelPipeline p = this.Pipeline;
            foreach (object m in msgs)
            {
                p.FireChannelRead(m);
            }
            p.FireChannelReadComplete();
            this.CheckException();
            return IsNotEmpty(this.inboundMessages);
        }

        /// <summary>
        ///     Write messages to the outbound of this <see cref="IChannel" />.
        /// </summary>
        /// <param name="msgs">The messages to be written.</param>
        /// <returns><c>true</c> if the write operation did add something to the inbound buffer</returns>
        public bool WriteOutbound(params object[] msgs)
        {
            this.EnsureOpen();
            if (msgs.Length == 0)
            {
                return IsNotEmpty(this.outboundMessages);
            }

            ThreadLocalObjectList futures = ThreadLocalObjectList.NewInstance(msgs.Length);

            foreach (object m in msgs)
            {
                if (m == null)
                {
                    break;
                }
                futures.Add(this.WriteAsync(m));
            }
            this.Flush();

            int size = futures.Count;
            for (int i = 0; i < size; i++)
            {
                var future = (Task)futures[i];
                if (future.IsCompleted)
                {
                    this.RecordException(future);
                }
                else
                {
                    // The write may be delayed to run later by runPendingTasks()
                    future.ContinueWith(t => this.RecordException(t));
                }
            }
            futures.Return();

            this.CheckException();
            return IsNotEmpty(this.outboundMessages);
        }

        void RecordException(Task future)
        {
            switch (future.Status)
            {
                case TaskStatus.Canceled:
                case TaskStatus.Faulted:
                    this.RecordException(future.Exception);
                    break;
                default:
                    break;
            }
        }

        void RecordException(Exception cause)
        {
            if (this.lastException == null)
            {
                this.lastException = cause;
            }
            else
            {
                logger.Warn("More than one exception was raised. " + "Will report only the first one and log others.", cause);
            }
        }

        /// <summary>
        ///     Mark this <see cref="IChannel" /> as finished. Any further try to write data to it will fail.
        /// </summary>
        /// <returns>bufferReadable returns <c>true</c></returns>
        public bool Finish() => this.Finish(false);

        /// <summary>
        /// Marks this <see cref="IChannel"/> as finished and releases all pending message in the inbound and outbound
        /// buffer. Any futher try to write data to it will fail.
        /// </summary>
        /// <returns><c>true</c> if any of the used buffers has something left to read, otherwise <c>false</c>.</returns>
        public bool FinishAndReleaseAll() => this.Finish(true);

        /// <summary>
        /// Marks this <see cref="IChannel"/> as finished. Any futher attempt to write data to it will fail.
        /// </summary>
        /// <param name="releaseAll">If <c>true</c>, all pending messages in the inbound and outbound buffer are released.</param>
        /// <returns><c>true</c> if any of the used buffers has something left to read, otherwise <c>false</c>.</returns>
        bool Finish(bool releaseAll)
        {
            this.CloseSafe();
            try
            {
                this.CheckException();
                return IsNotEmpty(this.inboundMessages) || IsNotEmpty(this.outboundMessages);
            }
            finally
            {
                if (releaseAll)
                {
                    ReleaseAll(this.inboundMessages);
                    ReleaseAll(this.outboundMessages);
                }
            }
        }

        /// <summary>
        /// Releases all buffered inbound messages.
        /// </summary>
        /// <returns><c>true</c> if any were in the inbound buffer, otherwise <c>false</c>.</returns>
        public bool ReleaseInbound() => ReleaseAll(this.inboundMessages);

        /// <summary>
        /// Releases all buffered outbound messages.
        /// </summary>
        /// <returns><c>true</c> if any were in the outbound buffer, otherwise <c>false</c>.</returns>
        public bool ReleaseOutbound() => ReleaseAll(this.outboundMessages);

        static bool ReleaseAll(Queue<object> queue)
        {
            if (queue != null && queue.Count > 0)
            {
                for (;;)
                {
                    if (queue.Count == 0)
                    {
                        break;
                    }
                    object msg = queue.Dequeue();
                    ReferenceCountUtil.Release(msg);
                }
                return true;
            }
            return false;
        }

        public override Task CloseAsync()
        {
            // We need to call RunPendingTasks() before calling super.CloseAsync() as there may be something in the queue
            // that needs to be run before the actual close takes place.
            Task future = base.CloseAsync();
            return future;
        }

        public override Task DisconnectAsync()
        {
            Task future = base.DisconnectAsync();
            return future;
        }

        static bool IsNotEmpty(Queue<object> queue) => queue != null && queue.Count > 0;

        /// <summary>
        ///     Check to see if there was any <see cref="Exception" /> and rethrow if so.
        /// </summary>
        public void CheckException()
        {
            Exception e = this.lastException;
            if (e == null)
            {
                return;
            }

            this.lastException = null;
            ExceptionDispatchInfo.Capture(e).Throw();
        }

        /// <summary>
        ///     Ensure the <see cref="IChannel" /> is open and if not throw an exception.
        /// </summary>
        protected void EnsureOpen()
        {
            if (!this.Open)
            {
                this.RecordException(new ClosedChannelException());
                this.CheckException();
            }
        }

        static object Poll(Queue<object> queue) => IsNotEmpty(queue) ? queue.Dequeue() : null;

        class DefaultUnsafe : AbstractUnsafe
        {
            public DefaultUnsafe(AbstractChannel channel)
                : base(channel)
            {
            }

            public override Task ConnectAsync(EndPoint remoteAddress, EndPoint localAddress) => TaskEx.Completed;
        }

        internal sealed class LastInboundHandler : ChannelHandlerAdapter
        {
            readonly Queue<object> inboundMessages;
            readonly Action<Exception> recordException;

            public LastInboundHandler(Queue<object> inboundMessages, Action<Exception> recordException)
            {
                this.inboundMessages = inboundMessages;
                this.recordException = recordException;
            }

            public override void ChannelRead(IChannelHandlerContext context, object message) => this.inboundMessages.Enqueue(message);

            public override void ExceptionCaught(IChannelHandlerContext context, Exception exception) => this.recordException(exception);
        }

        sealed class SingleThreadedEmbeddedChannelPipeline : DefaultChannelPipeline
        {
            public SingleThreadedEmbeddedChannelPipeline(SingleThreadedEmbeddedChannel channel)
                : base(channel)
            {
            }

            protected override void OnUnhandledInboundException(Exception cause) => ((SingleThreadedEmbeddedChannel)this.Channel).RecordException(cause);

            protected override void OnUnhandledInboundMessage(object msg) => ((SingleThreadedEmbeddedChannel)this.Channel).InboundMessages.Enqueue(msg);
        }
    }
}