// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


namespace UniNetty.Codecs.Http
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UniNetty.Common.Utilities;
    using UniNetty.Common.Collections.Immutable;

    public class EmptyHttpHeaders : HttpHeaders
    {
        static readonly IEnumerator<HeaderEntry<AsciiString, ICharSequence>> EmptryEnumerator = 
            Enumerable.Empty<HeaderEntry<AsciiString, ICharSequence>>().GetEnumerator();

        public static readonly EmptyHttpHeaders Default = new EmptyHttpHeaders();

        protected EmptyHttpHeaders()
        {
        }

        public override bool TryGet(AsciiString name, out ICharSequence value)
        {
            value = default(ICharSequence);
            return false;
        }

        public override bool TryGetInt(AsciiString name, out int value)
        {
            value = default(int);
            return false;
        }

        public override int GetInt(AsciiString name, int defaultValue) => defaultValue;

        public override bool TryGetShort(AsciiString name, out short value)
        {
            value = default(short);
            return false;
        }

        public override short GetShort(AsciiString name, short defaultValue) => defaultValue;

        public override bool TryGetTimeMillis(AsciiString name, out long value)
        {
            value = default(long);
            return false;
        }

        public override long GetTimeMillis(AsciiString name, long defaultValue) => defaultValue;

        public override IList<ICharSequence> GetAll(AsciiString name) => UniImmutableArray<ICharSequence>.Empty;

        public override IList<HeaderEntry<AsciiString, ICharSequence>> Entries() => UniImmutableArray<HeaderEntry<AsciiString, ICharSequence>>.Empty;

        public override bool Contains(AsciiString name) => false;

        public override bool IsEmpty => true;

        public override int Size => 0;

        public override ISet<AsciiString> Names() => UniImmutableHashSet<AsciiString>.Empty;

        public override HttpHeaders AddInt(AsciiString name, int value) => throw new NotSupportedException("read only");

        public override HttpHeaders AddShort(AsciiString name, short value) => throw new NotSupportedException("read only");

        public override HttpHeaders Set(AsciiString name, object value) => throw new NotSupportedException("read only");

        public override HttpHeaders Set(AsciiString name, IEnumerable<object> values) =>  throw new NotSupportedException("read only");

        public override HttpHeaders SetInt(AsciiString name, int value) => throw new NotSupportedException("read only");

        public override HttpHeaders SetShort(AsciiString name, short value) => throw new NotSupportedException("read only");

        public override HttpHeaders Remove(AsciiString name) => throw new NotSupportedException("read only");

        public override HttpHeaders Clear() => throw new NotSupportedException("read only");

        public override HttpHeaders Add(AsciiString name, object value) => throw new NotSupportedException("read only");

        public override IEnumerator<HeaderEntry<AsciiString, ICharSequence>> GetEnumerator() => EmptryEnumerator;
    }
}
