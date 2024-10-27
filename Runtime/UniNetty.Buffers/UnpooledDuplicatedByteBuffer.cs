// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Buffers
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using UniNetty.Common.Utilities;

    class UnpooledDuplicatedByteBuffer : AbstractDerivedByteBuffer
    {
        readonly AbstractByteBuffer buffer;

        public UnpooledDuplicatedByteBuffer(AbstractByteBuffer buffer) 
            : this(buffer, buffer.ReaderIndex, buffer.WriterIndex)
        {
        }

        internal UnpooledDuplicatedByteBuffer(AbstractByteBuffer buffer, int readerIndex, int writerIndex)
            : base(buffer.MaxCapacity)
        {
            if (buffer is UnpooledDuplicatedByteBuffer duplicated)
            {
                this.buffer = duplicated.buffer;
            }
            else if (buffer is AbstractPooledDerivedByteBuffer)
            {
                this.buffer = (AbstractByteBuffer)buffer.Unwrap();
            }
            else
            {
                this.buffer = buffer;
            }

            this.SetIndex0(readerIndex, writerIndex);
            this.MarkIndex(); // Mark read and writer index
        }

        public override IByteBuffer Unwrap() => this.UnwrapCore();

        public override IByteBuffer Copy(int index, int length) => this.Unwrap().Copy(index, length);

        protected AbstractByteBuffer UnwrapCore() => this.buffer;

        public override IByteBufferAllocator Allocator => this.Unwrap().Allocator;

        public override bool IsDirect => this.Unwrap().IsDirect;

        public override int Capacity => this.Unwrap().Capacity;

        public override IByteBuffer AdjustCapacity(int newCapacity) => this.Unwrap().AdjustCapacity(newCapacity);

        public override int IoBufferCount => this.Unwrap().IoBufferCount;

        public override bool HasArray => this.Unwrap().HasArray;

        public override byte[] Array => this.Unwrap().Array;

        public override int ArrayOffset => this.Unwrap().ArrayOffset;

        public override bool HasMemoryAddress => this.Unwrap().HasMemoryAddress;

        public override Memory<byte> GetPinnableMemoryAddress() => this.Unwrap().GetPinnableMemoryAddress();

        public override Span<byte> AddressOfPinnedMemory() => this.Unwrap().AddressOfPinnedMemory();

        internal override byte _GetByte(int index) => this.UnwrapCore()._GetByte(index);

        internal override short _GetShort(int index) => this.UnwrapCore()._GetShort(index);

        internal override short _GetShortLE(int index) => this.UnwrapCore()._GetShortLE(index);

        internal override int _GetUnsignedMedium(int index) => this.UnwrapCore()._GetUnsignedMedium(index);

        internal override int _GetUnsignedMediumLE(int index) => this.UnwrapCore()._GetUnsignedMediumLE(index);

        internal override int _GetInt(int index) => this.UnwrapCore()._GetInt(index);

        internal override int _GetIntLE(int index) => this.UnwrapCore()._GetIntLE(index);

        internal override long _GetLong(int index) => this.UnwrapCore()._GetLong(index);

        internal override long _GetLongLE(int index) => this.UnwrapCore()._GetLongLE(index);

        public override IByteBuffer GetBytes(int index, IByteBuffer destination, int dstIndex, int length) => this.Unwrap().GetBytes(index, destination, dstIndex, length);

        public override IByteBuffer GetBytes(int index, byte[] destination, int dstIndex, int length) => this.Unwrap().GetBytes(index, destination, dstIndex, length);

        public override IByteBuffer GetBytes(int index, Stream destination, int length) => this.Unwrap().GetBytes(index, destination, length);

        internal override void _SetByte(int index, int value) => this.UnwrapCore()._SetByte(index, value);

        internal override void _SetShort(int index, int value) => this.UnwrapCore()._SetShort(index, value);

        internal override void _SetShortLE(int index, int value) => this.UnwrapCore()._SetShortLE(index, value);

        internal override void _SetMedium(int index, int value) => this.UnwrapCore()._SetMedium(index, value);

        internal override void _SetMediumLE(int index, int value) => this.UnwrapCore()._SetMediumLE(index, value);

        public override IByteBuffer SetBytes(int index, IByteBuffer src, int srcIndex, int length) => this.Unwrap().SetBytes(index, src, srcIndex, length);

        public override Task<int> SetBytesAsync(int index, Stream src, int length, CancellationToken cancellationToken) => this.Unwrap().SetBytesAsync(index, src, length, cancellationToken);

        public override IByteBuffer SetBytes(int index, byte[] src, int srcIndex, int length) => this.Unwrap().SetBytes(index, src, srcIndex, length);

        internal override void _SetInt(int index, int value) => this.UnwrapCore()._SetInt(index, value);

        internal override void _SetIntLE(int index, int value) => this.UnwrapCore()._SetIntLE(index, value);

        internal override void _SetLong(int index, long value) => this.UnwrapCore()._SetLong(index, value);

        internal override void _SetLongLE(int index, long value) => this.UnwrapCore()._SetLongLE(index, value);

        public override int ForEachByte(int index, int length, IByteProcessor processor) => this.Unwrap().ForEachByte(index, length, processor);

        public override int ForEachByteDesc(int index, int length, IByteProcessor processor) => this.Unwrap().ForEachByteDesc(index, length, processor);
    }
}
