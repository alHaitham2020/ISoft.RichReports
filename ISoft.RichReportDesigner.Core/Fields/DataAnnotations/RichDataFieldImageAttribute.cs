using ISoft.Office.Images;
using ISoft.Utils.DataAnnotations;
using System;

namespace ISoft.RichReportDesigner.Core.Fields.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class RichDataFieldImageAttribute : AttributeBase
    {
        private readonly ImageUriSchemeType _schemeType;

        public ImageUriSchemeType SchemeType => _schemeType;

        public RichDataFieldImageAttribute(ImageUriSchemeType schemeType)
        {
            _schemeType = schemeType;
        }
    }
}
