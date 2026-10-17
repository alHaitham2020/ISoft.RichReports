using System.Collections.Generic;

namespace ISoft.RichReportDesigner.Core.RichEdit.Properties
{
    public class RichDocumentCustomProperties : RichValuePropertiesBase<IRichDocumentCustomProperty>, IRichDocumentCustomProperties
    {
        public RichDocumentCustomProperties()
        {

        }

        public RichDocumentCustomProperties(IEnumerable<IRichDocumentCustomProperty> collection) : base(collection)
        {

        }
    }
}

