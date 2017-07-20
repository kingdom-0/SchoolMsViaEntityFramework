using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMsViaEntityFramework.Logging
{
    public interface ILogger
    {
        void Information(string message);
        void Information(string format, params object[] args);
        void Information(Exception exception, string format, params object[] args);

        void Warning(string message);
        void Warning(string format, params object[] args);
        void Warning(Exception exception, string format, params object[] args);

        void Error(string message);
        void Error(string format, params object[] args);
        void Error(Exception exception, string format, params object[] args);

        void TraceApi(string componentName, string method, TimeSpan timeSpan);
        void TraceApi(string componentName, string method, TimeSpan timeSpan, string properties);
        void TraceApi(string componentName, string method, TimeSpan timeSpan, string format, params object[] args);
    }
}