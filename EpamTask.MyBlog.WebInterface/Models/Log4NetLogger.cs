namespace EpamTask.MyBlog.WebInterface.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using log4net;
    using log4net.Core;

    public class Log4NetLogger : ILogger
    {
        private ILog _logger;

        public Log4NetLogger()
        {
            _logger = LogManager.GetLogger(this.GetType());
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception x)
        {
            Error(LogUtility.BuildExceptionMessage(x));
        }

        public void Error(string message, Exception x)
        {
            _logger.Error(message, x);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(Exception x)
        {
            Fatal(LogUtility.BuildExceptionMessage(x));
        }

        bool ILogger.IsEnabledFor(Level level)
        {
            throw new NotImplementedException();
        }

        void ILogger.Log(LoggingEvent logEvent)
        {
            throw new NotImplementedException();
        }

        void ILogger.Log(Type callerStackBoundaryDeclaringType, Level level, object message, Exception exception)
        {
            throw new NotImplementedException();
        }

        string ILogger.Name
        {
            get { throw new NotImplementedException(); }
        }

        log4net.Repository.ILoggerRepository ILogger.Repository
        {
            get { throw new NotImplementedException(); }
        }
    }
}