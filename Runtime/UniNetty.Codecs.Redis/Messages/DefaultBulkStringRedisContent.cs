// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Codecs.Redis.Messages
{
    using System.Text;
    using UniNetty.Buffers;
    using UniNetty.Common.Utilities;

    public class DefaultBulkStringRedisContent : DefaultByteBufferHolder, IBulkStringRedisContent
    {
        public DefaultBulkStringRedisContent(IByteBuffer buffer)
            : base(buffer)
        {
        }

        public override IByteBufferHolder Replace(IByteBuffer content) => new DefaultBulkStringRedisContent(content);

        public override string ToString() =>
            new StringBuilder(StringUtil.SimpleClassName(this))
                .Append('[')
                .Append("content=")
                .Append(this.Content)
                .Append(']')
                .ToString();
    }
}