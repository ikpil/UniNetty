// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Http.Cookies
{
    using UniNetty.Common.Utilities;

    public static class CookieHeaderNames
    {
        public static readonly AsciiString Path = AsciiString.Cached("Path");

        public static readonly AsciiString Expires = AsciiString.Cached("Expires");

        public static readonly AsciiString MaxAge = AsciiString.Cached("Max-Age");

        public static readonly AsciiString Domain = AsciiString.Cached("Domain");

        public static readonly AsciiString Secure = AsciiString.Cached("Secure");

        public static readonly AsciiString HttpOnly = AsciiString.Cached("HTTPOnly");
    }
}
