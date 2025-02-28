using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService
{
    internal interface ILogger
    {
        public void TraceError(string errorMessage);
        public void TraceWarning(string warningMessage);
        public void TraceInfo(string info);
        public void Trace(string message);
    }
}
