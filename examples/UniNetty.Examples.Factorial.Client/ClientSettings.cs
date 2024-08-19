// Copyright (c) Microsoft. All rights reserved.
// Copyright (c) Ikpil Choi ikpil@naver.com All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace UniNetty.Examples.Factorial.Client
{
    using UniNetty.Examples.Common;

    public class ClientSettings : UniNetty.Examples.Common.ClientSettings
    {
        public static int Count => int.Parse(ExampleHelper.Configuration["count"]);
    }
}