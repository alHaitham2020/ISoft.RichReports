using DevExpress.XtraRichEdit.API.Native;
using ISoft.RichReportDesigner.Core.Fields.Codes;
namespace ISoft.RichReportDesigner.Core.Fields.Search
{
    public class RichFieldFindResult : IRichFieldFindResult
    {
        public RichFieldFindResult(IRichFieldCode fieldCode, Field field)
        {
            FieldCode = fieldCode;
            Field = field;
        }

        public IRichFieldCode FieldCode { get; }

        public Field Field { get; }
    }
}
