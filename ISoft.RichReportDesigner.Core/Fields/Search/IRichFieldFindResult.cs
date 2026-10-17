using DevExpress.XtraRichEdit.API.Native;
using ISoft.RichReportDesigner.Core.Fields.Codes;
namespace ISoft.RichReportDesigner.Core.Fields.Search
{
    public interface IRichFieldFindResult
    {
        IRichFieldCode FieldCode { get; }

        Field Field { get; }
    }
}
