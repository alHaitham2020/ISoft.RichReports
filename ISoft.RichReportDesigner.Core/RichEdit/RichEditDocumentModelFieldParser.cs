using DevExpress.XtraRichEdit.Fields;
using DevExpress.XtraRichEdit.Model;
using ISoft.RichReportDesigner.Core.RichEdit.Fields;
using ISoft.RichReportDesigner.Core.RichEdit.Services.Implementation;
using ISoft.Utils.Extentions;

namespace ISoft.RichReportDesigner.Core.RichEdit
{
    public class RichEditDocumentModelFieldParser
    {
        private readonly RichEditFieldCalculatorService _calculatorService;

        public RichEditDocumentModelFieldParser() : this(new RichEditFieldCalculatorService())
        {

        }

        public RichEditDocumentModelFieldParser(RichEditFieldCalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        public RichMergeField GetMergeField(Field field)
        {
            return ParseField<RichMergeField>(field);
        }

        public T ParseField<T>(Field field) where T : CalculatedFieldBase
        {
            CalculatedFieldBase calculatedFieldBase = _calculatorService.ParseField(field.PieceTable, field);
            return calculatedFieldBase.CastTo<T>();
        }

        public bool TryParseField<T>(Field field,out T calcField) where T : CalculatedFieldBase
        {
            CalculatedFieldBase calculatedFieldBase = _calculatorService.ParseField(field.PieceTable, field);
            return calculatedFieldBase.TryCast(out calcField);
        }
    }
}
