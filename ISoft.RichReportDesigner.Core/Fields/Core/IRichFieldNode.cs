using ISoft.Office.Images;
using ISoft.RichReportDesigner.Core.Fields.Data;
using ISoft.RichReportDesigner.Core.Fields.Format;
using ISoft.Utils.Enumrations;

namespace ISoft.RichReportDesigner.Core.Fields.Core
{
    public interface IRichFieldNode : IRichNode
    {
        IRichFieldGroup ParentGroup { get;}

        string FieldProperty { get;}

        string DataMember { get; }

        RichFieldFormat? FieldFormat { get; }

        object NullValue { get;}

        RichFieldDataTypeCategory DataTypeCategory { get; }

        DataTypeKind DataTypeKind { get; }

        ImageUriSchemeType ImageSchemeType { get; }

        bool IsDetailField { get; }

        bool HasFormat { get; }

        bool IsImageField { get; }

    }
}
