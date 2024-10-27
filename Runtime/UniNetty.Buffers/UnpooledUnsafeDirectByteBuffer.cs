// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// ReSharper disable ConvertToAutoProperty
namespace UniNetty.Buffers
{
    using System;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using UniNetty.Common.Internal;
    using UniNetty.Common.Utilities;

    public class UnpooledUnsafeDirectByteBuffer : AbstractReferenceCountedByteBuffer
    {
        readonly IByteBufferAllocator allocator;

        int capacity;
        bool doNotFree;
        byte[] buffer;

        public UnpooledUnsafeDirectByteBuffer(IByteBufferAllocator alloc, int initialCapacity, int maxCapacity)
            : base(maxCapacity)
        {
            Contract.Requires(alloc != null);
            Contract.Requires(initialCapacity >= 0);
            Contract.Requires(maxCapacity >= 0);

            if (initialCapacity > maxCapacity)
            {
                throw new ArgumentException($"initialCapacity({initialCapacity}) > maxCapacity({maxCapacity})");
            }

            this.allocator = alloc;
            this.SetByteBuffer(this.NewArray(initialCapacity), false);
        }

        protected UnpooledUnsafeDirectByteBuffer(IByteBufferAllocator alloc, byte[] initialBuffer, int maxCapacity, bool doFree)
            : base(maxCapacity)
        {
            Contract.Requires(alloc != null);
            Contract.Requires(initialBuffer != null);

            int initialCapacity = initialBuffer.Length;
            if (initialCapacity > maxCapacity)
            {
                throw new ArgumentException($"initialCapacity({initialCapacity}) > maxCapacity({maxCapacity})");
            }

            this.allocator = alloc;
            this.doNotFree = !doFree;
            this.SetByteBuffer(initialBuffer, false);
        }

        protected virtual byte[] AllocateDirect(int initialCapacity) => this.NewArray(initialCapacity);

        protected byte[] NewArray(int initialCapacity) => new byte[initialCapacity];

        protected virtual void FreeDirect(byte[] array)
        {
            // NOOP rely on GC.
        }

        void SetByteBuffer(byte[] array, bool tryFree)
        {
            if (tryFree)
            {
                byte[] oldBuffer = this.buffer;
                if (oldBuffer != null)
                {
                    if (this.doNotFree)
                    {
                        this.doNotFree = false;
                    }
                    else
                    {
                        this.FreeDirect(oldBuffer);
                    }
                }
            }
            this.buffer = array;
            this.capacity = array.Length;
        }

        public override bool IsDirect => true;

        public override int Capacity => this.capacity;

        public override IByteBuffer AdjustCapacity(int newCapacity)
        {
            this.CheckNewCapacity(newCapacity);

            int rIdx = this.ReaderIndex;
            int wIdx = this.WriterIndex;

            int oldCapacity = this.capacity;
            if (newCapacity > oldCapacity)
            {
                byte[] oldBuffer = this.buffer;
                byte[] newBuffer = this.AllocateDirect(newCapacity);
                PlatformDependent.CopyMemory(oldBuffer, 0, newBuffer, 0, oldCapacity);
                this.SetByteBuffer(newBuffer, true);
            }
            else if (newCapacity < oldCapacity)
            {
                byte[] oldBuffer = this.buffer;
                byte[] newBuffer = this.AllocateDirect(newCapacity);
                if (rIdx < newCapacity)
                {
                    if (wIdx > newCapacity)
                    {
                        this.SetWriterIndex(wIdx = newCapacity);
                    }
                    PlatformDependent.CopyMemory(oldBuffer, rIdx, newBuffer, 0, wIdx - rIdx);
                }
                else
                {
                    this.SetIndex(newCapacity, newCapacity);
                }
                this.SetByteBuffer(newBuffer, true);
            }
            return this;
        }

        public override IByteBufferAllocator Allocator => this.allocator;

        public override bool HasArray => true;

        public override byte[] Array
        {
            get
            {
                this.EnsureAccessible();
                return this.buffer;
            }
        }

        public override int ArrayOffset => 0;

        public override bool HasMemoryAddress => true;

        public override Memory<byte> GetPinnableMemoryAddress()
        {
            this.EnsureAccessible();
            return new Memory<byte>(this.buffer);
        }

        public override Span<byte> AddressOfPinnedMemory() => null;

        internal override byte _GetByte(int index) => this.buffer[index];

        internal override short _GetShort(int index)
        {
            var addr = this.Addr(index);
            return UnsafeByteBufferUtil.GetShort(addr);
        }

        internal override short _GetShortLE(int index)
        {
            var addr = this.Addr(index);
            return UnsafeByteBufferUtil.GetShortLE(addr);
        }

        internal override int _GetUnsignedMedium(int index)
        {
            var addr = this.Addr(index);
            return UnsafeByteBufferUtil.GetUnsignedMedium(addr);
        }

        internal override int _GetUnsignedMediumLE(int index)
        {
            var addr = this.Addr(index);
            return UnsafeByteBufferUtil.GetUnsignedMediumLE(addr);
        }

        internal override int _GetInt(int index)
        {
            var addr = this.Addr(index);
            return UnsafeByteBufferUtil.GetInt(addr);
        }

        internal override int _GetIntLE(int index)
        {
            var addr = this.Addr(index);
            return UnsafeByteBufferUtil.GetIntLE(addr);
        }

        internal override long _GetLong(int index)
        {
            var addr = this.Addr(index);
            return UnsafeByteBufferUtil.GetLong(addr);
        }

        internal override long _GetLongLE(int index)
        {
            var addr = this.Addr(index);
            return UnsafeByteBufferUtil.GetLongLE(addr);
        }

        public override IByteBuffer GetBytes(int index, IByteBuffer dst, int dstIndex, int length)
        {
            this.CheckIndex(index, length);
            UnsafeByteBufferUtil.GetBytes(this, buffer, index, dst, dstIndex, length);
            return this;
        }

        public override IByteBuffer GetBytes(int index, byte[] dst, int dstIndex, int length)
        {
            this.CheckIndex(index, length);
            UnsafeByteBufferUtil.GetBytes(this, buffer, index, dst, dstIndex, length);
            return this;
        }

        internal override void _SetByte(int index, int value) => this.buffer[index] = unchecked((byte)value);

        internal override void _SetShort(int index, int value)
        {
            var addr = this.Addr(index);
            UnsafeByteBufferUtil.SetShort(addr, value);
        }

        internal override void _SetShortLE(int index, int value)
        {
            var addr = this.Addr(index);
            UnsafeByteBufferUtil.SetShortLE(addr, value);
        }

        internal override void _SetMedium(int index, int value)
        {
            var addr = this.Addr(index);
            UnsafeByteBufferUtil.SetMedium(addr, value);
        }

        internal override void _SetMediumLE(int index, int value)
        {
            var addr = this.Addr(index);
            UnsafeByteBufferUtil.SetMediumLE(addr, value);
        }

        internal override void _SetInt(int index, int value)
        {
            var addr = this.Addr(index);
            UnsafeByteBufferUtil.SetInt(addr, value);
        }

        internal override void _SetIntLE(int index, int value)
        {
            var addr = this.Addr(index);
            UnsafeByteBufferUtil.SetIntLE(addr, value);
        }

        internal override void _SetLong(int index, long value)
        {
            var addr = this.Addr(index);
            UnsafeByteBufferUtil.SetLong(addr, value);
        }

        internal override void _SetLongLE(int index, long value)
        {
            var addr = this.Addr(index);
            UnsafeByteBufferUtil.SetLongLE(addr, value);
        }

        public override IByteBuffer SetBytes(int index, IByteBuffer src, int srcIndex, int length)
        {
            this.CheckIndex(index, length);
            UnsafeByteBufferUtil.SetBytes(this, buffer, index, src, srcIndex, length);
            return this;
        }

        public override IByteBuffer SetBytes(int index, byte[] src, int srcIndex, int length)
        {
            this.CheckIndex(index, length);
            if (length != 0)
            {
                UnsafeByteBufferUtil.SetBytes(this, buffer, index, src, srcIndex, length);
            }
            return this;
        }

        public override IByteBuffer GetBytes(int index, Stream output, int length)
        {
            this.CheckIndex(index, length);
            UnsafeByteBufferUtil.GetBytes(this, buffer, index, output, length);
            return this;
        }

        public override Task<int> SetBytesAsync(int index, Stream src, int length, CancellationToken cancellationToken)
        {
            this.CheckIndex(index, length);
            return UnsafeByteBufferUtil.SetBytesAsync(this, buffer, index, src, length, cancellationToken);
        }

        public override int IoBufferCount => 1;

        public override ArraySegment<byte> GetIoBuffer(int index, int length)
        {
            this.CheckIndex(index, length);
            return new ArraySegment<byte>(this.buffer, index, length);
        }

        public override ArraySegment<byte>[] GetIoBuffers(int index, int length) => new[] { this.GetIoBuffer(index, length) };

        public override IByteBuffer Copy(int index, int length)
        {
            this.CheckIndex(index, length);
            return UnsafeByteBufferUtil.Copy(this, buffer, index, length);
        }

        protected internal override void Deallocate()
        {
            byte[] buf = this.buffer;
            if (buf == null)
            {
                return;
            }

            this.buffer = null;

            if (!this.doNotFree)
            {
                this.FreeDirect(buf);
            }
        }

        public override IByteBuffer Unwrap() => null;

        // TODO: ikpil test
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Span<byte> Addr(int index) => this.buffer.AsSpan(index);

        public override IByteBuffer SetZero(int index, int length)
        {
            this.CheckIndex(index, length);
            var addr = this.Addr(index);
            UnsafeByteBufferUtil.SetZero(addr, length);
            return this;
        }

        public override IByteBuffer WriteZero(int length)
        {
            this.EnsureWritable(length);
            int wIndex = this.WriterIndex;
            var addr = this.Addr(wIndex);
            UnsafeByteBufferUtil.SetZero(addr, length);
            this.SetWriterIndex(wIndex + length);
            return this;
        }
    }
}