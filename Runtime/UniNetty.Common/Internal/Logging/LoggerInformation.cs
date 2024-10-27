using UniNetty.Logging;

namespace UniNetty.Common.Internal.Logging
{
    internal readonly struct LoggerInformation
    {
        public readonly ILogger Logger;
        public readonly string Category;
        public readonly ProviderRegistration Registration;

        public LoggerInformation(ProviderRegistration registration, string category) : this()
        {
            Registration = registration;
            Logger = registration.Provider.CreateLogger(category);
            Category = category;
        }
    }
}