using ISoft.RichReportDesigner.Core.Fields.Core;
using System;
using System.Collections.Generic;

namespace ISoft.RichReportDesigner.Core.RichEdit.Services
{
    public interface IRichEditFieldsManagerService
    {
        IReadOnlyDictionary<RichFieldType, RichFieldInfo> GetAllFieldInfos();

        IEnumerable<string> GetAllFieldNames();

        string GetFieldDisplayName(RichFieldType fieldType);

        RichFieldInfo? GetFieldInfo(RichFieldType fieldType);

        RichFieldInfo? GetFieldInfoByName(string fieldName);

        IEnumerable<RichFieldInfo> GetFieldsByType(Type type);

        RichFieldType? GetFieldTypeByName(string fieldName);

        bool HasFieldName(string fieldName);

        bool HasFieldType(RichFieldType fieldType);

        bool TryGetFieldInfo(RichFieldType fieldType, out RichFieldInfo info);

        bool TryGetFieldInfoByName(string fieldName, out RichFieldInfo info);
    }
}
