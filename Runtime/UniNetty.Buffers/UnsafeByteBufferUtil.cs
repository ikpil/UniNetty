// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace UniNetty.Buffers
{
    using System;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using UniNetty.Common.Internal;
    using UniNetty.Common.Utilities;

    public static class UnsafeByteBufferUtil
    {
        const byte Zero = 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short GetShort(Span<byte> bytes) =>
            unchecked((short)(((bytes[0]) << 8) | bytes[1]));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short GetShortLE(Span<byte> bytes) =>
            unchecked((short)((bytes[0]) | (bytes[1] << 8)));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetUnsignedMedium(Span<byte> bytes) =>
            bytes[0] << 16 |
            bytes[1] << 8 |
            bytes[2];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetUnsignedMediumLE(Span<byte> bytes) =>
            bytes[0] |
            bytes[1] << 8 |
            bytes[2] << 16;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetInt(Span<byte> bytes) =>
            (bytes[0] << 24) |
            (bytes[1] << 16) |
            (bytes[2] << 8) |
            (bytes[3]);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetIntLE(Span<byte> bytes) =>
            (bytes[0]) |
            (bytes[1] << 8) |
            (bytes[2] << 16) |
            (bytes[3] << 24);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long GetLong(Span<byte> bytes)
        {
            unchecked
            {
                int i1 = (bytes[0] << 24) | (bytes[1] << 16) | (bytes[2] << 8) | (bytes[3]);
                int i2 = (bytes[4] << 24) | (bytes[5] << 16) | (bytes[6] << 8) | (bytes[7]);
                return (uint)i2 | ((long)i1 << 32);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long GetLongLE(Span<byte> bytes)
        {
            unchecked
            {
                int i1 = (bytes[0]) | (bytes[1] << 8) | (bytes[2] << 16) | (bytes[3] << 24);
                int i2 = (bytes[4]) | (bytes[5] << 8) | (bytes[6] << 16) | (bytes[7] << 24);
                return (uint)i1 | ((long)i2 << 32);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetShort(Span<byte> bytes, int value)
        {
            unchecked
            {
                bytes[0] = (byte)((ushort)value >> 8);
                bytes[1] = (byte)value;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetShortLE(Span<byte> bytes, int value)
        {
            unchecked
            {
                bytes[0] = (byte)value;
                bytes[1] = (byte)((ushort)value >> 8);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetMedium(Span<byte> bytes, int value)
        {
            unchecked
            {
                uint unsignedValue = (uint)value;
                bytes[0] = (byte)(unsignedValue >> 16);
                bytes[1] = (byte)(unsignedValue >> 8);
                bytes[2] = (byte)unsignedValue;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetMediumLE(Span<byte> bytes, int value)
        {
            unchecked
            {
                uint unsignedValue = (uint)value;
                bytes[0] = (byte)unsignedValue;
                bytes[1] = (byte)(unsignedValue >> 8);
                bytes[2] = (byte)(unsignedValue >> 16);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetInt(Span<byte> bytes, int value)
        {
            unchecked
            {
                uint unsignedValue = (uint)value;
                bytes[0] = (byte)(unsignedValue >> 24);
                bytes[1] = (byte)(unsignedValue >> 16);
                bytes[2] = (byte)(unsignedValue >> 8);
                bytes[3] = (byte)unsignedValue;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetIntLE(Span<byte> bytes, int value)
        {
            unchecked
            {
                uint unsignedValue = (uint)value;
                bytes[0] = (byte)unsignedValue;
                bytes[1] = (byte)(unsignedValue >> 8);
                bytes[2] = (byte)(unsignedValue >> 16);
                bytes[3] = (byte)(unsignedValue >> 24);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLong(Span<byte> bytes, long value)
        {
            unchecked
            {
                ulong unsignedValue = (ulong)value;
                bytes[0] = (byte)(unsignedValue >> 56);
                bytes[1] = (byte)(unsignedValue >> 48);
                bytes[2] = (byte)(unsignedValue >> 40);
                bytes[3] = (byte)(unsignedValue >> 32);
                bytes[4] = (byte)(unsignedValue >> 24);
                bytes[5] = (byte)(unsignedValue >> 16);
                bytes[6] = (byte)(unsignedValue >> 8);
                bytes[7] = (byte)unsignedValue;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLongLE(Span<byte> bytes, long value)
        {
            unchecked
            {
                ulong unsignedValue = (ulong)value;
                bytes[0] = (byte)unsignedValue;
                bytes[1] = (byte)(unsignedValue >> 8);
                bytes[2] = (byte)(unsignedValue >> 16);
                bytes[3] = (byte)(unsignedValue >> 24);
                bytes[4] = (byte)(unsignedValue >> 32);
                bytes[5] = (byte)(unsignedValue >> 40);
                bytes[6] = (byte)(unsignedValue >> 48);
                bytes[7] = (byte)(unsignedValue >> 56);
            }
        }

        public static void SetZero(byte[] array, int index, int length)
        {
            if (length == 0)
            {
                return;
            }

            PlatformDependent.SetMemory(array, index, length, Zero);
        }

        internal static IByteBuffer Copy(AbstractByteBuffer buf, Span<byte> addr, int index, int length)
        {
            IByteBuffer copy = buf.Allocator.DirectBuffer(length, buf.MaxCapacity);
            if (length != 0)
            {
                if (copy.HasMemoryAddress)
                {
                    Span<byte> ptr = copy.AddressOfPinnedMemory();
                    if (ptr != null)
                    {
                        PlatformDependent.CopyMemory(addr, index, ptr, 0, length);
                    }
                    else
                    {
                        var dst = copy.GetPinnableMemoryAddress();
                        {
                            PlatformDependent.CopyMemory(addr, index, dst.Span, 0, length);
                        }
                    }

                    copy.SetIndex(0, length);
                }
                else
                {
                    copy.WriteBytes(buf, index, length);
                }
            }

            return copy;
        }

        internal static int SetBytes(AbstractByteBuffer buf, Span<byte> addr, int index, Stream input, int length)
        {
            if (length == 0)
            {
                return 0;
            }

            IByteBuffer tmpBuf = buf.Allocator.HeapBuffer(length);
            try
            {
                byte[] tmp = tmpBuf.Array;
                int offset = tmpBuf.ArrayOffset;
                int readBytes = input.Read(tmp, offset, length);
                if (readBytes > 0)
                {
                    PlatformDependent.CopyMemory(tmp, offset, addr, index, readBytes);
                }

                return readBytes;
            }
            finally
            {
                tmpBuf.Release();
            }
        }

        internal static Task<int> SetBytesAsync(AbstractByteBuffer buf, Memory<byte> addr, int index, Stream input, int length, CancellationToken cancellationToken)
        {
            if (length == 0)
            {
                return TaskEx.Zero;
            }

            IByteBuffer tmpBuf = buf.Allocator.HeapBuffer(length);
            return tmpBuf.SetBytesAsync(0, input, length, cancellationToken)
                .ContinueWith(t =>
                {
                    try
                    {
                        var read = t.Result;
                        if (read > 0)
                        {
                            PlatformDependent.CopyMemory(tmpBuf.Array, tmpBuf.ArrayOffset, addr.Span, index, read);
                        }

                        return read;
                    }
                    finally
                    {
                        tmpBuf.Release();
                    }
                });
        }

        internal static void GetBytes(AbstractByteBuffer buf, Span<byte> addr, int index, IByteBuffer dst, int dstIndex, int length)
        {
            Contract.Requires(dst != null);

            if (MathUtil.IsOutOfBounds(dstIndex, length, dst.Capacity))
            {
                ThrowHelper.ThrowIndexOutOfRangeException_DstIndex(dstIndex);
            }

            if (dst.HasMemoryAddress)
            {
                Span<byte> ptr = dst.AddressOfPinnedMemory();
                if (ptr != null)
                {
                    PlatformDependent.CopyMemory(addr, index, ptr, dstIndex, length);
                }
                else
                {
                    var destination = dst.GetPinnableMemoryAddress();
                    PlatformDependent.CopyMemory(addr, index, destination.Span, dstIndex, length);
                }
            }
            else if (dst.HasArray)
            {
                PlatformDependent.CopyMemory(addr, index, dst.Array, dst.ArrayOffset + dstIndex, length);
            }
            else
            {
                dst.SetBytes(dstIndex, buf, index, length);
            }
        }

        internal static void GetBytes(AbstractByteBuffer buf, Span<byte> addr, int index, byte[] dst, int dstIndex, int length)
        {
            Contract.Requires(dst != null);

            if (MathUtil.IsOutOfBounds(dstIndex, length, dst.Length))
            {
                ThrowHelper.ThrowIndexOutOfRangeException_DstIndex(dstIndex);
            }

            if (length != 0)
            {
                PlatformDependent.CopyMemory(addr, index, dst, dstIndex, length);
            }
        }

        internal static void SetBytes(AbstractByteBuffer buf, Span<byte> addr, int index, IByteBuffer src, int srcIndex, int length)
        {
            Contract.Requires(src != null);

            if (MathUtil.IsOutOfBounds(srcIndex, length, src.Capacity))
            {
                ThrowHelper.ThrowIndexOutOfRangeException_SrcIndex(srcIndex);
            }

            if (length != 0)
            {
                if (src.HasMemoryAddress)
                {
                    Span<byte> ptr = src.AddressOfPinnedMemory();
                    if (ptr != null)
                    {
                        PlatformDependent.CopyMemory(ptr, srcIndex, addr, index, length);
                    }
                    else
                    {
                        var source = src.GetPinnableMemoryAddress();
                        PlatformDependent.CopyMemory(source.Span, srcIndex, addr, index, length);
                    }
                }
                else if (src.HasArray)
                {
                    PlatformDependent.CopyMemory(src.Array, src.ArrayOffset + srcIndex, addr, index, length);
                }
                else
                {
                    src.GetBytes(srcIndex, buf, index, length);
                }
            }
        }

        // No need to check length zero, the calling method already done it
        internal static void SetBytes(AbstractByteBuffer buf, Span<byte> addr, int index, byte[] src, int srcIndex, int length) =>
            PlatformDependent.CopyMemory(src, srcIndex, addr, index, length);

        internal static void GetBytes(AbstractByteBuffer buf, Span<byte> addr, int index, Stream output, int length)
        {
            if (length != 0)
            {
                IByteBuffer tmpBuf = buf.Allocator.HeapBuffer(length);
                try
                {
                    byte[] tmp = tmpBuf.Array;
                    int offset = tmpBuf.ArrayOffset;
                    PlatformDependent.CopyMemory(addr, index, tmp, offset, length);
                    output.Write(tmp, offset, length);
                }
                finally
                {
                    tmpBuf.Release();
                }
            }
        }

        internal static void SetZero(Span<byte> addr, int length)
        {
            if (length == 0)
            {
                return;
            }

            PlatformDependent.SetMemory(addr, 0, length, Zero);
        }

        internal static string GetString(ReadOnlySpan<byte> src, int length, Encoding encoding)
        {
            // TODO: ikpil test 
            return encoding.GetString(src.Slice(0, length).ToArray());
        }


        internal static UnpooledUnsafeDirectByteBuffer NewUnsafeDirectByteBuffer(IByteBufferAllocator alloc, int initialCapacity, int maxCapacity) =>
            new UnpooledUnsafeDirectByteBuffer(alloc, initialCapacity, maxCapacity);
    }
}