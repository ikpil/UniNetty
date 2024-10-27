// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace UniNetty.Common.Internal
{
    using System.Runtime.CompilerServices;
    using UniNetty.Common.Utilities;

    static class PlatformDependent0
    {
        internal static readonly int HashCodeAsciiSeed = unchecked((int)0xc2b2ae35);
        internal static readonly int HashCodeC1 = unchecked((int)0xcc9e2d51);
        internal static readonly int HashCodeC2 = 0x1b873593;

        internal static bool ByteArrayEquals(byte[] bytes1, int offset1, byte[] bytes2, int offset2, int length)
        {
            if (length <= 0)
            {
                return true;
            }

            int baseOffset1 = offset1;
            int baseOffset2 = offset2;
            int remainingBytes = length & 7;
            int end = baseOffset1 + remainingBytes;
            for (int i = baseOffset1 - 8 + length, j = baseOffset2 - 8 + length; i >= end; i -= 8, j -= 8)
            {
                if (BitConverter.ToInt64(bytes1, i) != BitConverter.ToInt64(bytes2, j))
                {
                    return false;
                }
            }

            if (remainingBytes >= 4)
            {
                remainingBytes -= 4;
                if (BitConverter.ToInt32(bytes1, baseOffset1 + remainingBytes) != BitConverter.ToInt32(bytes2, baseOffset2 + remainingBytes))
                {
                    return false;
                }
            }

            if (remainingBytes >= 2)
            {
                return BitConverter.ToInt16(bytes1, baseOffset1) == BitConverter.ToInt16(bytes2, baseOffset2)
                       && (remainingBytes == 2 || bytes1[baseOffset1 + 2] == bytes2[baseOffset2 + 2]);
            }

            return bytes1[baseOffset1] == bytes2[baseOffset2];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int HashCodeAscii(byte[] bytes, int offset, int length)
        {
            int hash = HashCodeAsciiSeed;
            int remainingBytes = length & 7;
            int end = offset + remainingBytes;
            for (int i = offset - 8 + length; i >= end; i -= 8)
            {
                hash = HashCodeAsciiCompute(BitConverter.ToInt64(bytes, i), hash);
            }

            switch (remainingBytes)
            {
                case 7:
                    return ((hash * HashCodeC1 + HashCodeAsciiSanitize(bytes[offset]))
                            * HashCodeC2 + HashCodeAsciiSanitize(BitConverter.ToInt16(bytes, offset + 1)))
                        * HashCodeC1 + HashCodeAsciiSanitize(BitConverter.ToInt32(bytes, offset + 3));
                case 6:
                    return (hash * HashCodeC1 + HashCodeAsciiSanitize(BitConverter.ToInt16(bytes, offset)))
                        * HashCodeC2 + HashCodeAsciiSanitize(BitConverter.ToInt32(bytes, offset + 2));
                case 5:
                    return (hash * HashCodeC1 + HashCodeAsciiSanitize(bytes[offset]))
                        * HashCodeC2 + HashCodeAsciiSanitize(BitConverter.ToInt32(bytes, offset + 1));
                case 4:
                    return hash * HashCodeC1 + HashCodeAsciiSanitize(BitConverter.ToInt32(bytes, offset));
                case 3:
                    return (hash * HashCodeC1 + HashCodeAsciiSanitize(bytes[offset]))
                        * HashCodeC2 + HashCodeAsciiSanitize(BitConverter.ToInt16(bytes, offset + 1));
                case 2:
                    return hash * HashCodeC1 + HashCodeAsciiSanitize(BitConverter.ToInt16(bytes, offset));
                case 1:
                    return hash * HashCodeC1 + HashCodeAsciiSanitize(bytes[offset]);
                default:
                    return hash;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int HashCodeAsciiCompute(long value, int hash)
        {
            // masking with 0x1f reduces the number of overall bits that impact the hash code but makes the hash
            // code the same regardless of character case (upper case or lower case hash is the same).
            unchecked
            {
                return hash * HashCodeC1 +
                       // Low order int
                       HashCodeAsciiSanitize((int)value) * HashCodeC2 +
                       // High order int
                       (int)(value & 0x1f1f1f1f00000000L).RightUShift(32);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int HashCodeAsciiSanitize(int value) => value & 0x1f1f1f1f;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int HashCodeAsciiSanitize(short value) => value & 0x1f1f;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int HashCodeAsciiSanitize(byte value) => value & 0x1f;
    }
}