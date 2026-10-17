using System.Collections.Generic;

namespace ISoft.RichReportDesigner.Core.RichEdit.Properties
{
    public class RichDocumentVariablesProperties : RichValuePropertiesBase<IRichDocumentVariableProperty>, IRichDocumentVariablesProperties
    {
        public RichDocumentVariablesProperties()
        {

        }

        public RichDocumentVariablesProperties(IEnumerable<IRichDocumentVariableProperty> collection) : base(collection)
        {

        }
    }
}