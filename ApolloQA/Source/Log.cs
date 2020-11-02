using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApolloQA
{

    public static class Log
    {

        public static void Debug(string text) => Write(Severity.DEBUG, text);
        public static void Info(string text) => Write(Severity.INFO, text);
        public static void Warn(string text) => Write(Severity.WARN, text);
        public static void Error(string text) => Write(Severity.ERROR, text);
        public static void Critical(string text) => Write(Severity.CRITICAL, text);

        /// <summary>
        /// Will log text to the the given severity
        /// </summary>
        /// <remarks>
        /// Examples: <br/>
        /// Log.write(Severity.INFO, "informational message") <br/>
        /// OR <br/>
        /// Log.info("informational message") <br/>
        /// </remarks>
        /// 
        public static void Write(Severity severity, string text)
        {
            var currentSev = Severity.parseLevel(Environment.GetEnvironmentVariable("CURRENT_SEVERITY_LEVEL")).Level;

            if(currentSev == 0)
            {
                return;
            }
            else if (severity.Level <= currentSev)
            {
              
                Console.WriteLine($"[{severity.Name}] {text}");
            }
        }




    }

    
}
