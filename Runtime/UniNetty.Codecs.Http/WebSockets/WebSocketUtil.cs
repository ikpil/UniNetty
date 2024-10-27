// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Http.WebSockets
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using UniNetty.Codecs.Base64;
    using UniNetty.Buffers;
    using UniNetty.Common;
    using UniNetty.Common.Internal;

    static class WebSocketUtil
    {
        static readonly Random Random = PlatformDependent.GetThreadLocalRandom();

        static readonly ThreadLocalMD5 LocalMd5 = new ThreadLocalMD5();

        sealed class ThreadLocalMD5 : FastThreadLocal<MD5>
        {
            protected override MD5 GetInitialValue() => MD5.Create();
        }

        static readonly ThreadLocalSha1 LocalSha1 = new ThreadLocalSha1();

        sealed class ThreadLocalSha1 : FastThreadLocal<SHA1>
        {
            protected override SHA1 GetInitialValue() => SHA1.Create();
        }

        internal static byte[] Md5(byte[] data)
        {
            MD5 md5 = LocalMd5.Value;
            md5.Initialize();
            return md5.ComputeHash(data);
        }

        internal static byte[] Sha1(byte[] data)
        {
            SHA1 sha1 = LocalSha1.Value;
            sha1.Initialize();
            return sha1.ComputeHash(data);
        }

        internal static string Base64String(byte[] data)
        {
            IByteBuffer encodedData = Unpooled.WrappedBuffer(data);
            IByteBuffer encoded = Base64.Encode(encodedData);
            string encodedString = encoded.ToString(Encoding.UTF8);
            encoded.Release();
            return encodedString;
        }

        internal static byte[] RandomBytes(int size)
        {
            var bytes = new byte[size];
            Random.NextBytes(bytes);
            return bytes;
        }

        internal static int RandomNumber(int minimum, int maximum) => unchecked((int)(Random.NextDouble() * maximum + minimum));

        // Math.Random()
        internal static double RandomNext() => Random.NextDouble();
    }
}
