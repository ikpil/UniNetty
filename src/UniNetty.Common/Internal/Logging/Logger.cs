using System;
using System.Collections.Generic;
using UniNetty.Logging;

namespace UniNetty.Common.Internal.Logging
{
    internal class Logger : ILogger
    {
        private readonly string _categoryName;
        internal LoggerInformation[] Loggers;
        internal MessageLogger[] MessageLoggers;

        public Logger(string categoryName, LoggerInformation[] loggers)
        {
            _categoryName = categoryName;
            Loggers = loggers;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            MessageLogger[] loggers = MessageLoggers;
            if (loggers == null)
            {
                return;
            }

            List<Exception> exceptions = null;
            for (int i = 0; i < loggers.Length; i++)
            {
                ref readonly MessageLogger loggerInfo = ref loggers[i];
                if (!loggerInfo.IsEnabled(logLevel))
                {
                    continue;
                }

                LoggerLog(logLevel, eventId, loggerInfo.Logger, exception, formatter, ref exceptions, state);
            }

            if (exceptions != null && exceptions.Count > 0)
            {
                ThrowLoggingError(exceptions);
            }

            static void LoggerLog(LogLevel logLevel, EventId eventId, ILogger logger, Exception exception, Func<TState, Exception, string> formatter, ref List<Exception> exceptions, in TState state)
            {
                try
                {
                    logger.Log(logLevel, eventId, state, exception, formatter);
                }
                catch (Exception ex)
                {
                    exceptions ??= new List<Exception>();
                    exceptions.Add(ex);
                }
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            MessageLogger[] loggers = MessageLoggers;
            if (loggers == null)
            {
                return false;
            }

            List<Exception> exceptions = null;
            int i = 0;
            for (; i < loggers.Length; i++)
            {
                ref readonly MessageLogger loggerInfo = ref loggers[i];
                if (!loggerInfo.IsEnabled(logLevel))
                {
                    continue;
                }

                if (LoggerIsEnabled(logLevel, loggerInfo.Logger, ref exceptions))
                {
                    break;
                }
            }

            if (exceptions != null && exceptions.Count > 0)
            {
                ThrowLoggingError(exceptions);
            }

            return i < loggers.Length ? true : false;

            static bool LoggerIsEnabled(LogLevel logLevel, ILogger logger, ref List<Exception> exceptions)
            {
                try
                {
                    if (logger.IsEnabled(logLevel))
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    exceptions ??= new List<Exception>();
                    exceptions.Add(ex);
                }

                return false;
            }
        }

        private static void ThrowLoggingError(List<Exception> exceptions)
        {
            throw new AggregateException(message: "An error occurred while writing to logger(s).", innerExceptions: exceptions);
        }
    }
}