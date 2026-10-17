using ISoft.RichReportDesigner.Core.Images;
using System;

namespace ISoft.RichReportDesigner.Core.Fields.Data
{
    public class RichFieldDataInfo : IRichFieldDataInfo
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Path { get; set; }

        public Type DataType {  get; set; }

        public object NullValue { get; set; }

        public bool AllowUseDefaultEmptyValue { get; set; }

        public bool IsDetailField { get; set; }

        public ImageUriSchemeType ImageSchemeType {  get; set; }

        public bool IsImageField { get; set; }
    }
}
