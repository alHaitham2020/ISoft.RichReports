using System.Collections.Generic;

namespace ISoft.RichReportDesigner.Core.Templates
{
    public interface IRichTemplateDiagnosticsInfo
    {
        int CollectionTablesCount { get; }

        List<string> CollectionPaths { get; }

        int NestedTablesCount { get; }

        int RegularTablesCount { get; }

        int TotalMergeFieldsCount { get; }

        int TotalTablesCount { get; }

        string ToString();
    }
}
