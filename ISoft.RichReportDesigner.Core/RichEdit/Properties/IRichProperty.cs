using ISoft.RichReportDesigner.Core.Fields.Format;
using System;

namespace ISoft.RichReportDesigner.Core.RichEdit.Properties
{
    public interface IRichProperty : ICloneable
    {
        string Name { get; }

        string DisplayName { get; }

        object DefaultValue { get; }

        bool AllowUseDefaultEmptyValue {  get; }

        bool IsVisible { get; }

        Type PropertyType { get; }

        RichFieldFormat? Format { get; }

        bool HasFormat { get; }
    }
}
