// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Transport.Channels
{
    using System;
    using System.Diagnostics.Contracts;
    using UniNetty.Common.Utilities;

    public sealed class ActionChannelInitializer<T> : ChannelInitializer<T>
        where T : IChannel
    {
        readonly Action<T> initializationAction;

        public ActionChannelInitializer(Action<T> initializationAction)
        {
            Contract.Requires(initializationAction != null);

            this.initializationAction = initializationAction;
        }

        protected override void InitChannel(T channel) => this.initializationAction(channel);

        public override string ToString() => nameof(ActionChannelInitializer<T>) + "[" + StringUtil.SimpleClassName(typeof(T)) + "]";
    }
}