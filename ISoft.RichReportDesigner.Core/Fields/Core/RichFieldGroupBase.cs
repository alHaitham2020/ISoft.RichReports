using ISoft.RichReportDesigner.Core.Fields.Display;
using System;

namespace ISoft.RichReportDesigner.Core.Fields.Core
{
    public abstract class RichFieldGroupBase : RichNodeBase, IRichFieldGroup
    {
        public override bool IsGroup => true;

        public RichDisplayFieldGroupCategory Category { get; set; }

        public RichFieldGroupBase(
            string name,
            string displayName,
            string path,
            RichDisplayFieldGroupCategory category,
            Type dataType = null,
            IRichFieldGroup parentGroup = null,
            bool? isVisible = null)
        {
            Name = name;
            DisplayName = displayName;
            Path = path;
            Category = category;
            DataType = dataType;
            ParentGroup = parentGroup;
            IsVisible = isVisible ?? true;
        }
    }
}
