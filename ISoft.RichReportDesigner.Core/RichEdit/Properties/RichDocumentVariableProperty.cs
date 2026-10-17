using ISoft.RichReportDesigner.Core.Fields.Format;
using ISoft.RichReportDesigner.Core.Images;
using System;

namespace ISoft.RichReportDesigner.Core.RichEdit.Properties
{
    public interface IRichDocumentVariableProperty : IRichPropertyWithValue
    {

    }

    public class RichDocumentVariableProperty : RichPropertyWithValueBase, IRichDocumentVariableProperty
    {
        public RichDocumentVariableProperty(
             string name,
             string displayName,
             object value,
             Type propertyType,
             object defaultValue = null,
             RichFieldFormat? format = null,
             bool? visible = null)
             : base(name, displayName, propertyType, value, defaultValue, format, visible)
        {

        }
    }
}
