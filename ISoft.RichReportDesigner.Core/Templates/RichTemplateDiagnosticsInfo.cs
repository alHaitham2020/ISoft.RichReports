using System.Collections.Generic;

namespace ISoft.RichReportDesigner.Core.Templates
{
    /// <summary>
    /// Diagnostic information about a template
    /// </summary>
    public class RichTemplateDiagnosticsInfo : IRichTemplateDiagnosticsInfo
    {
        public int TotalTablesCount { get; set; } = 0;

        public int CollectionTablesCount { get; set; } = 0;

        public int RegularTablesCount { get; set; } = 0;

        public int CollectionFieldsCount { get; set; } = 0;

        public int TotalFieldsCount { get; set; } = 0;

        public int TotalMergeFieldsCount { get; set; } = 0;

        public int NestedTablesCount { get; set; } = 0;

        public bool HasFormattingIssues { get; set; } = false;

        public List<string> FieldPaths { get; set; } = new List<string>();

        public List<string> CollectionPaths { get; set; } = new List<string>();

        public override string ToString()
        {
            return $"Tables: {TotalTablesCount} total, {CollectionTablesCount} collections, " +
                   $"{RegularTablesCount} regular, {NestedTablesCount} nested\n" +
                   $"Merge Fields: {TotalMergeFieldsCount}\n" +
                   $"Collections: {string.Join(", ", CollectionPaths)}";
        }
    }
}
