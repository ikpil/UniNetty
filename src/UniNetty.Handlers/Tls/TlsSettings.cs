// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Handlers.Tls
{
    using System.Security.Authentication;

    public abstract class TlsSettings
    {
        protected TlsSettings(SslProtocols enabledProtocols, bool checkCertificateRevocation)
        {
            this.EnabledProtocols = enabledProtocols;
            this.CheckCertificateRevocation = checkCertificateRevocation;
        }

        public SslProtocols EnabledProtocols { get; }

        public bool CheckCertificateRevocation { get; }
    }
}