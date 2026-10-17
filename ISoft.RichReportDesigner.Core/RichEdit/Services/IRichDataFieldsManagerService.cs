using ISoft.RichReportDesigner.Core.Fields.Data;

namespace ISoft.RichReportDesigner.Core.RichEdit.Services
{
    public interface IRichDataFieldsManagerService
    {
        string GeFieldActualDataName(string fieldName);

        string GetFieldDisplayName(string fieldName);

        object GetFieldDisplayValue(string fieldName, object fieldValue);

        IRichFieldDataInfo GetFieldInfo(string fieldName);
    }
}
