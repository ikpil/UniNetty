// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace UniNetty.Codecs.Http
{
    using System.Text;
    using UniNetty.Common.Utilities;

    // Similar to java URLEncoder
    static class UrlEncoder
    {
        public static string Encode(string s, Encoding encoding)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            int length = s.Length;
            {
                int count = encoding.GetMaxByteCount(1);
                int total = length * count * 3; // bytes per char encoded maximum
                char[] bytes = new char[total];
                byte[] buf = new byte[count];

                int index = 0;
                for (int i = 0; i < length; i++)
                {
                    char ch = s[i];
                    if ((ch >= 'a' && ch <= 'z')
                        || (ch >= 'A' && ch <= 'Z')
                        || (ch >= '0' && ch <= '9'))
                    {
                        bytes[index++] = ch;
                    }
                    else
                    {
                        total = encoding.GetBytes(s, i, 1, buf, 0);
                        for (int j = 0; j < total; j++)
                        {
                            bytes[index++] = '%';
                            bytes[index++] = CharUtil.Digits[(buf[j] & 0xf0) >> 4];
                            bytes[index++] = CharUtil.Digits[buf[j] & 0xf];
                        }
                    }
                }

                return new string(bytes, 0, index);
            }
        }
    }
}
