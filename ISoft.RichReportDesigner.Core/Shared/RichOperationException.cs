using System;

namespace ISoft.RichReportDesigner.Core.Shared
{
    public class RichOperationException : Exception
    {
        public RichOperationException(string message, Exception inner) : base(message, inner) { }
    }
}
