using ISoft.RichReportDesigner.Core.Fields.Core;
using ISoft.RichReportDesigner.Core.Fields.Display;
using System;

namespace ISoft.RichReportDesigner.Core.Fields.Data
{
    public class RichDataFieldGroup : RichFieldGroupBase, IRichDataFieldGroup
    {
        #region Fields
        private readonly RichDataFieldCollection _fields;
        #endregion

        #region Proprites
        public IRichDataFieldCollection Fields => _fields;

        public new IRichDataFieldGroup ParentGroup
        {
            get
            {
                return base.ParentGroup as IRichDataFieldGroup;
            }
            set
            {
                base.ParentGroup = value;
            }
        }

        #endregion

        public RichDataFieldGroup(
            string name,
            string displayName,
            string path,
            Type dataType = null,
            IRichDataFieldGroup parentGroup = null)
            : base(name, displayName, path, RichDisplayFieldGroupCategory.DataFields, dataType, parentGroup)
        {
            _fields = new ();
        }
    }

}
