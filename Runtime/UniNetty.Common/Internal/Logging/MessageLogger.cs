using System;
using UniNetty.Logging;

namespace UniNetty.Common.Internal.Logging
{
    internal readonly struct MessageLogger
    {
        internal readonly ILogger Logger;
        internal readonly string Category;
        internal readonly string ProviderTypeFullName;
        internal readonly LogLevel MinLevel;
        internal readonly Func<string, string, LogLevel, bool> Filter;

        public MessageLogger(ILogger logger, string category, string providerTypeFullName, LogLevel minLevel, Func<string, string, LogLevel, bool> filter)
        {
            Logger = logger;
            Category = category;
            ProviderTypeFullName = providerTypeFullName;
            MinLevel = minLevel;
            Filter = filter;
        }

        public bool IsEnabled(LogLevel level)
        {
            if (level < MinLevel)
            {
                return false;
            }

            if (Filter != null)
            {
                return Filter.Invoke(ProviderTypeFullName, Category, level);
            }

            return true;
        }
    }
}