// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Examples.HttpServer
{
    sealed class MessageBody
    {
        public MessageBody(string message)
        {
            this.Message = message;
        }

        public string Message { get; }

        public string ToJsonFormat() => "{" + $"\"{nameof(MessageBody)}\" :" + "{" + $"\"{nameof(this.Message)}\"" + " :\"" + this.Message + "\"}" +"}";
    }
}
