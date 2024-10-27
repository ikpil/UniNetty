using System;
using UniNetty.Logging;

namespace UniNetty.Common.Internal.Logging
{
    internal class ProviderRegistration
    {
        public readonly ILoggerProvider Provider;
        public readonly bool ShouldDispose;
        public readonly LogLevel MinLevel;
        public readonly Func<string, string, LogLevel, bool> Filter;

        public ProviderRegistration(ILoggerProvider provider, bool shouldDispose, LogLevel minLevel, Func<string, string, LogLevel, bool> filter)
        {
            Provider = provider;
            ShouldDispose = shouldDispose;
            MinLevel = minLevel;
            Filter = filter;
        }
    }
}