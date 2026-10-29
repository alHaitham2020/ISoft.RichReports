using ISoft.RichReportDesigner.Core.Fields.Switches;
using System;
using System.Collections.Generic;

namespace ISoft.RichReportDesigner.Core.Fields.Format
{
    public interface IRichFieldFormatManager
    {
        List<string> GetAvailableFormatValues(Type dataType);

        List<RichFieldFormat> GetAvailableFormats(Type dataType);

        RichFieldFormat? GetDefaultFormat(Type dataType);

        string GetDefaultFormatValue(Type dataType);

        RichFieldFormat? GetFormat(string name);

        RichFieldFormatType? GetFormatTypeForValue(string formatValue);

        RichSwitchType GetSwitchType(string switchCode);

        IRichFieldFormatProvider GetProvider(RichFieldFormatType formatType);

        void RegisterProvider(IRichFieldFormatProvider provider);

        bool HasProvider(RichFieldFormatType formatType);

        bool HasDefaultFormatValue(Type dataType);
    }
}
