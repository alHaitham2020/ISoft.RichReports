using ISoft.Utils.DataAnnotations;
using System;

namespace ISoft.RichReportDesigner.Core.Fields.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class RichDataFieldIgnoreAttribute : AttributeBase
    {

    }
}
