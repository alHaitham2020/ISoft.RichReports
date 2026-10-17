using ISoft.RichReportDesigner.Core.Fields.Core;

namespace ISoft.RichReportDesigner.Core.Fields.Data
{
    public class RichDataField : RichFieldNodeBase, IRichDataField
    {
        public new IRichDataFieldGroup ParentGroup
        {
            get => base.ParentGroup as IRichDataFieldGroup; 
            set => base.ParentGroup = value;
        }

        public bool AllowUseDefaultEmptyValue { get; set; }

        public string FieldDataName => IsDetailField ? Name : Path;

        public RichDataField()
        {

        }
    }
}
