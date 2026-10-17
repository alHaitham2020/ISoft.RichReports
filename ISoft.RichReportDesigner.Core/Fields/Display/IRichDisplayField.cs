using ISoft.RichReportDesigner.Core.Fields.Core;

namespace ISoft.RichReportDesigner.Core.Fields.Display
{
    public interface IRichDisplayField : IRichFieldNode, IRichFieldHighlightInfo
    {
        new IRichDisplayFieldGroup ParentGroup { get; }

        RichFieldType FieldType { get; }

        RichFieldKey FieldKey { get; }

        bool IsNestedPictureField { get; }

        bool PreventMultipleInsertion { get; }

        object CreateFieldOptions();
    }
}
