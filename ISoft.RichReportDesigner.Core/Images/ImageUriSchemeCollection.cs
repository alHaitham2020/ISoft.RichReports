using System;
using System.Collections.Generic;

namespace ISoft.RichReportDesigner.Core.Images
{
    public class ImageUriSchemeCollection : Dictionary<string, ImageUriScheme>
    {
        public ImageUriSchemeCollection() : base(StringComparer.OrdinalIgnoreCase)
        {

        }
    }
}
