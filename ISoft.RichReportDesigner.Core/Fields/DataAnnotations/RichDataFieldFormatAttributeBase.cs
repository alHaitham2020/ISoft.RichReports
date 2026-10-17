using ISoft.RichReportDesigner.Core.Fields.Format;
using ISoft.Utils.DataAnnotations;
using System;

namespace ISoft.RichReportDesigner.Core.Fields.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public abstract class RichDataFieldFormatAttributeBase : AttributeBase
    {
        public abstract RichFieldFormat? GetFormat();
    }
}
