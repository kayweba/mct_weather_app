using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService
{
    internal class ConsoleLogger : ILogger
    {
        public void Trace(string message)
        {
            Console.WriteLine(message);
        }

        public void TraceError(string errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[E] {errorMessage}");
            Console.ResetColor();
        }

        public void TraceInfo(string info)
        {
            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine($"[I] {info}");
            Console.ResetColor();
        }

        public void TraceWarning(string warningMessage)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[W] {warningMessage}");
            Console.ResetColor();
        }
    }
}
