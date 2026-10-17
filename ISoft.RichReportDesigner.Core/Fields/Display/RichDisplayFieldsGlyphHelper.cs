using ISoft.RichReportDesigner.Core.Fields.Data;
using ISoft.Utils.Helpers;
using System;

namespace ISoft.RichReportDesigner.Core.Fields.Display
{
    public static class RichDisplayFieldsGlyphHelper
    {
        public static Uri GetFieldImageUri(this RichFieldDataTypeCategory category)
        {
            return category switch
            {
                RichFieldDataTypeCategory.String => DevexpressImagesHelper.GetDxImageUri(@"SvgImages/Spreadsheet/Text2.svg"),
                RichFieldDataTypeCategory.Numeric => DevexpressImagesHelper.GetDxImageUri(@"SvgImages/Spreadsheet/AccountingNumberFormat.svg"),
                RichFieldDataTypeCategory.DateTime => DevexpressImagesHelper.GetDxImageUri(@"SvgImages/Spreadsheet/ShortDate.svg"),
                RichFieldDataTypeCategory.Boolean => DevexpressImagesHelper.GetDxImageUri(@"SvgImages/Spreadsheet/3SymbolsCircled.svg"),
                RichFieldDataTypeCategory.Image => DevexpressImagesHelper.GetDxImageUri(@"SvgImages/Dashboards/Images.svg"),
                _ => DevexpressImagesHelper.GetDxImageUri(@"SvgImages/Icon Builder/Actions_Question.svg"),
            };
        }

        public static Uri GetGroupImageUri(this RichDisplayFieldGroupCategory category)
        {
            return category switch
            {
                RichDisplayFieldGroupCategory.SystemFields => DevexpressImagesHelper.GetDxImageUri(@"SvgImages/Business Objects/BO_Address.svg"),
                RichDisplayFieldGroupCategory.DocumentProperties => DevexpressImagesHelper.GetDxImageUri(@"SvgImages/Spreadsheet/AccountingNumberFormat.svg"),
                RichDisplayFieldGroupCategory.CustomProperties => DevexpressImagesHelper.GetDxImageUri(@"SvgImages/Spreadsheet/ShortDate.svg"),
                RichDisplayFieldGroupCategory.DataFields => DevexpressImagesHelper.GetDxImageUri(@"SvgImages/Icon Builder/Actions_Database.svg"),
                _ => DevexpressImagesHelper.GetDxImageUri(@"SvgImages/Icon Builder/Actions_Question.svg"),
            };
        }
    }
}
