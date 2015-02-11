using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inisoft.Core
{
    /// <summary>
    /// Dodatkowa klasa służąca do debagu
    /// </summary>
    public static class DebugUtils
    {
        public static void TraceOut(string sourceClass, string sourceMethod, string message, params object[] p)
        {
#if DEBUG
            TraceOut(string.Format("{0}, {1}: {2}", sourceClass, sourceMethod, message), p);
#endif
        }

        /// <summary>
        /// Wypisuje komunikaty na Out
        /// </summary>
        /// <param name="message"></param>
        /// <param name="p"></param>
        public static void TraceOut(string message, params object[] p)
        {
#if DEBUG
            System.Diagnostics.Trace.WriteLine(string.Format(message, p));
            System.Diagnostics.Trace.Flush();
#endif
        }
    }
}