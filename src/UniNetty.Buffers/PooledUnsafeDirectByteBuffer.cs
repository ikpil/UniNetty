// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Buffers
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    sealed class PooledUnsafeDirectByteBuffer : PooledByteBuffer<byte[]>
    {
        Memory<byte> memoryAddress;

        internal static PooledUnsafeDirectByteBuffer NewInstance(int maxCapacity)
        {
            return new PooledUnsafeDirectByteBuffer(maxCapacity);
        }

        PooledUnsafeDirectByteBuffer(int maxCapacity)
            : base(maxCapacity)
        {
        }

        internal override void Init(PoolChunk<byte[]> chunk, long handle, int offset, int length, int maxLength,
            PoolThreadCache<byte[]> cache)
        {
            base.Init(chunk, handle, offset, length, maxLength, cache);
            this.InitMemoryAddress();
        }

        internal override void InitUnpooled(PoolChunk<byte[]> chunk, int length)
        {
            base.InitUnpooled(chunk, length);
            this.InitMemoryAddress();
        }

        void InitMemoryAddress()
        {
            this.memoryAddress = new Memory<byte>(this.Memory, this.Offset, Length);
        }

        public override bool IsDirect => true;

        internal void Reuse(int maxCapacity)
        {
            this.SetMaxCapacity(maxCapacity);
            this.SetReferenceCount(1);
            this.SetIndex0(0, 0);
            this.DiscardMarks();
        }

        internal override byte _GetByte(int index) => this.memoryAddress.Span[index];

        internal override short _GetShort(int index) => UnsafeByteBufferUtil.GetShort(this.Addr(index));

        internal override short _GetShortLE(int index) => UnsafeByteBufferUtil.GetShortLE(this.Addr(index));

        internal override int _GetUnsignedMedium(int index) => UnsafeByteBufferUtil.GetUnsignedMedium(this.Addr(index));

        internal override int _GetUnsignedMediumLE(int index) => UnsafeByteBufferUtil.GetUnsignedMediumLE(this.Addr(index));

        internal override int _GetInt(int index) => UnsafeByteBufferUtil.GetInt(this.Addr(index));

        internal override int _GetIntLE(int index) => UnsafeByteBufferUtil.GetIntLE(this.Addr(index));

        internal override long _GetLong(int index) => UnsafeByteBufferUtil.GetLong(this.Addr(index));

        internal override long _GetLongLE(int index) => UnsafeByteBufferUtil.GetLongLE(this.Addr(index));

        public override IByteBuffer GetBytes(int index, IByteBuffer dst, int dstIndex, int length)
        {
            this.CheckIndex(index, length);
            UnsafeByteBufferUtil.GetBytes(this, memoryAddress.Span, index, dst, dstIndex, length);
            return this;
        }

        public override IByteBuffer GetBytes(int index, byte[] dst, int dstIndex, int length)
        {
            this.CheckIndex(index, length);
            UnsafeByteBufferUtil.GetBytes(this, memoryAddress.Span, index, dst, dstIndex, length);
            return this;
        }

        public override IByteBuffer GetBytes(int index, Stream output, int length)
        {
            UnsafeByteBufferUtil.GetBytes(this, memoryAddress.Span, index, output, length);
            return this;
        }

        internal override void _SetByte(int index, int value) => this.memoryAddress.Span[index] = unchecked((byte)value);

        internal override void _SetShort(int index, int value) => UnsafeByteBufferUtil.SetShort(this.Addr(index), value);

        internal override void _SetShortLE(int index, int value) => UnsafeByteBufferUtil.SetShortLE(this.Addr(index), value);

        internal override void _SetMedium(int index, int value) => UnsafeByteBufferUtil.SetMedium(this.Addr(index), value);

        internal override void _SetMediumLE(int index, int value) => UnsafeByteBufferUtil.SetMediumLE(this.Addr(index), value);

        internal override void _SetInt(int index, int value) => UnsafeByteBufferUtil.SetInt(this.Addr(index), value);

        internal override void _SetIntLE(int index, int value) => UnsafeByteBufferUtil.SetIntLE(this.Addr(index), value);

        internal override void _SetLong(int index, long value) => UnsafeByteBufferUtil.SetLong(this.Addr(index), value);

        internal override void _SetLongLE(int index, long value) => UnsafeByteBufferUtil.SetLongLE(this.Addr(index), value);

        public override IByteBuffer SetBytes(int index, IByteBuffer src, int srcIndex, int length)
        {
            this.CheckIndex(index, length);
            UnsafeByteBufferUtil.SetBytes(this, memoryAddress.Span, index, src, srcIndex, length);
            return this;
        }

        public override IByteBuffer SetBytes(int index, byte[] src, int srcIndex, int length)
        {
            this.CheckIndex(index, length);
            UnsafeByteBufferUtil.SetBytes(this, memoryAddress.Span, index, src, srcIndex, length);
            return this;
        }

        public override Task<int> SetBytesAsync(int index, Stream src, int length, CancellationToken cancellationToken)
        {
            this.CheckIndex(index, length);
            return UnsafeByteBufferUtil.SetBytesAsync(this, memoryAddress, index, src, length, cancellationToken);
        }

        public override IByteBuffer Copy(int index, int length)
        {
            this.CheckIndex(index, length);
            return UnsafeByteBufferUtil.Copy(this, memoryAddress.Span, index, length);
        }

        public override int IoBufferCount => 1;

        public override ArraySegment<byte> GetIoBuffer(int index, int length)
        {
            this.CheckIndex(index, length);
            index = this.Idx(index);
            return new ArraySegment<byte>(this.Memory, index, length);
        }

        public override ArraySegment<byte>[] GetIoBuffers(int index, int length) => new[] { this.GetIoBuffer(index, length) };

        public override bool HasArray => true;

        public override byte[] Array
        {
            get
            {
                this.EnsureAccessible();
                return this.Memory;
            }
        }

        public override int ArrayOffset => this.Offset;

        public override bool HasMemoryAddress => true;

        public override Memory<byte> GetPinnableMemoryAddress()
        {
            this.EnsureAccessible();
            return new Memory<byte>(this.Memory, this.Offset, this.Length);
        }

        public override Span<byte> AddressOfPinnedMemory() => this.memoryAddress.Span;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Span<byte> Addr(int index) => this.memoryAddress.Span.Slice(index);

        public override IByteBuffer SetZero(int index, int length)
        {
            this.CheckIndex(index, length);
            UnsafeByteBufferUtil.SetZero(this.Addr(index), length);
            return this;
        }

        public override IByteBuffer WriteZero(int length)
        {
            this.EnsureWritable(length);
            int wIndex = this.WriterIndex;
            UnsafeByteBufferUtil.SetZero(this.Addr(wIndex), length);
            this.SetWriterIndex(wIndex + length);
            return this;
        }
    }
}
