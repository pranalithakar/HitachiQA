using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HitachiQA
{

    public static class Log
    {

        public static void Debug(object text) => Write(Severity.DEBUG, text);
        public static void Info(object text) => Write(Severity.INFO, text);
        public static void Warn(object text) => Write(Severity.WARN, text);
        public static void Error(object text) => Write(Severity.ERROR, text);
        public static void Critical(object text) => Write(Severity.CRITICAL, text);

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
        public static void Write(Severity severity, object text)
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
