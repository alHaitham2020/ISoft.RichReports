using ISoft.Utils.DataAnnotations;
using System;

namespace ISoft.RichReportDesigner.Core.Fields.Instructions
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class RichFieldFormatSwitchAttribute : AttributeBase
    {
        public RichFieldFormatSwitchAttribute()
        {

        }

    }
}
