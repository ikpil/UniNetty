using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UniNetty.Logging;

namespace UniNetty.Common.Internal.Logging
{
    internal class LoggerFactory : ILoggerFactory
    {
        private readonly object _sync = new object();
        private readonly ConcurrentDictionary<string, Logger> _loggers;
        private readonly List<ProviderRegistration> _providerRegistrations;
        private volatile bool _disposed;

        public LoggerFactory()
        {
            _loggers = new ConcurrentDictionary<string, Logger>();
            _providerRegistrations = new List<ProviderRegistration>();
        }

        protected virtual bool CheckDisposed() => _disposed;

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;

                foreach (ProviderRegistration registration in _providerRegistrations)
                {
                    try
                    {
                        if (registration.ShouldDispose)
                        {
                            registration.Provider.Dispose();
                        }
                    }
                    catch
                    {
                        // Swallow exceptions on dispose
                    }
                }
            }
        }

        public ILogger CreateLogger(string categoryName)
        {
            if (CheckDisposed())
            {
                throw new ObjectDisposedException(nameof(LoggerFactory));
            }

            if (!_loggers.TryGetValue(categoryName, out Logger logger))
            {
                lock (_sync)
                {
                    if (!_loggers.TryGetValue(categoryName, out logger))
                    {
                        logger = new Logger(categoryName, CreateLoggers(categoryName));
                        logger.MessageLoggers = ApplyFilters(logger.Loggers);
                        _loggers[categoryName] = logger;
                    }
                }
            }

            return logger;
        }


        public void AddProvider(ILoggerProvider provider)
        {
            AddProvider(provider, LogLevel.Trace, null);
        }

        public void AddProvider(ILoggerProvider provider, LogLevel minLevel, Func<string, string, LogLevel, bool> filter)
        {
            if (CheckDisposed())
            {
                throw new ObjectDisposedException(nameof(LoggerFactory));
            }

            if (provider == null)
            {
                throw new ArgumentNullException($"{nameof(provider)} cannot be null");
            }

            lock (_sync)
            {
                var providerRegistration = new ProviderRegistration(provider, true, minLevel, filter);
                _providerRegistrations.Add(providerRegistration);

                foreach (KeyValuePair<string, Logger> existingLogger in _loggers)
                {
                    Logger logger = existingLogger.Value;

                    // 
                    LoggerInformation[] loggerInformation = logger.Loggers;
                    int newLoggerIndex = loggerInformation.Length;
                    Array.Resize(ref loggerInformation, loggerInformation.Length + 1);
                    loggerInformation[newLoggerIndex] = new LoggerInformation(providerRegistration, existingLogger.Key);
                    logger.Loggers = loggerInformation;
                    logger.MessageLoggers = ApplyFilters(logger.Loggers);
                }
            }
        }


        private LoggerInformation[] CreateLoggers(string categoryName)
        {
            var loggers = new LoggerInformation[_providerRegistrations.Count];
            for (int i = 0; i < _providerRegistrations.Count; i++)
            {
                loggers[i] = new LoggerInformation(_providerRegistrations[i], categoryName);
            }

            return loggers;
        }

        private MessageLogger[] ApplyFilters(LoggerInformation[] loggers)
        {
            var messageLoggers = new List<MessageLogger>();
            foreach (LoggerInformation loggerInformation in loggers)
            {
                var minLevel = loggerInformation.Registration.MinLevel;
                var fullName = loggerInformation.Registration.Provider.GetType().FullName;
                var filter = loggerInformation.Registration.Filter;

                if (minLevel > LogLevel.Critical)
                    continue;

                if (null != filter && !filter.Invoke(fullName, loggerInformation.Category, minLevel))
                {
                    continue;
                }

                messageLoggers.Add(new MessageLogger(loggerInformation.Logger, loggerInformation.Category, fullName, minLevel, filter));
            }

            return messageLoggers.ToArray();
        }
    }
}