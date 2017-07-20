using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;

namespace SchoolMsViaEntityFramework.Logging
{
    public class Logger : ILogger
    {
        public void Error(string message)
        {
            Trace.TraceInformation(message);
        }

        public void Error(string format, params object[] args)
        {
            Trace.TraceError(format, args);
        }

        public void Error(Exception exception, string format, params object[] args)
        {
            Trace.TraceError(FormatExceptionMessage(exception, format, args));
        }

        public void Information(string message)
        {
            Trace.TraceInformation(message);
        }

        public void Information(string format, params object[] args)
        {
            Trace.TraceInformation(format, args);
        }

        public void Information(Exception exception, string format, params object[] args)
        {
            Trace.TraceInformation(FormatExceptionMessage(exception, format, args));
        }

        public void TraceApi(string componentName, string method, TimeSpan timeSpan)
        {
            TraceApi(componentName, method, timeSpan, string.Empty);
        }

        public void TraceApi(string componentName, string method, TimeSpan timeSpan, string properties)
        {
            var message = string.Concat("Component:", componentName, "method:", method, "timespan:", timeSpan, "properties:", properties);
            Trace.TraceInformation(message);
        }

        public void TraceApi(string componentName, string method, TimeSpan timeSpan, string format, params object[] args)
        {
            TraceApi(componentName, method, timeSpan, string.Format(format, args));
        }

        public void Warning(string message)
        {
            Trace.TraceWarning(message);
        }

        public void Warning(string format, params object[] args)
        {
            Trace.TraceWarning(format, args);
        }

        public void Warning(Exception exception, string format, params object[] args)
        {
            Trace.TraceWarning(FormatExceptionMessage(exception, format, args));
        }

        private static string FormatExceptionMessage(Exception exception, string format, object[] args)
        {
            var builder = new StringBuilder();
            builder.Append(string.Format(format, args));
            builder.Append(" Exception:");
            builder.Append(exception.ToString());
            return builder.ToString();
        }
    }
}