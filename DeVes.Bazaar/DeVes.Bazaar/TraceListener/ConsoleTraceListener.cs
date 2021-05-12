using System;
using System.Diagnostics;

namespace DeVes.Bazaar.TraceListener
{
    public sealed class ConsoleTraceListener : TextWriterTraceListener
    {
        private int _counter;

        public ConsoleTraceListener(string name = "Console") : base(Console.Out)
        {
            Name = name;
        }

        public override void Write(string message)
        {
            Writer?.Write(_counter == 0 ? FormatMessage(message) : message);
            _counter++;
        }

        public override void WriteLine(string message)
        {
            Writer?.WriteLine(_counter == 0 ? FormatMessage(message) : message);
            _counter = 0;
        }

        private string FormatMessage(string message)
        {
            return $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {new string(' ', IndentLevel * IndentSize)}{message}";
        }

        public override void TraceEvent(TraceEventCache eventCache,
                                        string          source,
                                        TraceEventType  severity,
                                        int             id,
                                        string          message)
        {
            if (Filter != null &&
                !Filter.ShouldTrace(eventCache, source, severity, id, message, null, null, null))
                return;

            var headLine = $": {source}[{id}]";

            WriteEventType(severity);
            WriteTxtLine(headLine);
            WriteTxtLine("      " + message);
        }

        private static void WriteEventType(TraceEventType severity)
        {
            string       txt;
            ConsoleColor color;
            switch (severity)
            {
                case TraceEventType.Error:
                    txt   = "error";
                    color = ConsoleColor.Red;
                    break;
                case TraceEventType.Information:
                    txt   = "info";
                    color = ConsoleColor.DarkGreen;
                    break;
                case TraceEventType.Warning:
                    txt   = "warn";
                    color = ConsoleColor.Yellow;
                    break;
                default:
                    txt   = $"{severity}";
                    color = ConsoleColor.Gray;
                    break;
            }

            WriteTxt($"{txt}", color);
        }

        private static void WriteTxt(string value, ConsoleColor? foregroundColor = null)
        {
            var foregroundColorOrg = Console.ForegroundColor;

            Console.ForegroundColor = foregroundColor ?? foregroundColorOrg;
            Console.Write(value);

            Console.ForegroundColor = foregroundColorOrg;
        }

        private static void WriteTxtLine(string value, ConsoleColor? foregroundColor = null)
        {
            var foregroundColorOrg = Console.ForegroundColor;

            Console.ForegroundColor = foregroundColor ?? foregroundColorOrg;
            Console.WriteLine(value);

            Console.ForegroundColor = foregroundColorOrg;
        }
    }
}