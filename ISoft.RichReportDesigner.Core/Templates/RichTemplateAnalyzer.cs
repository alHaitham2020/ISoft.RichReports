//using DevExpress.XtraRichEdit;
//using ISoft.RichReportDesigner.Core.Fields;
//using ISoft.RichReportDesigner.Core.Fields.Services;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;

//namespace ISoft.RichReportDesigner.Core.Templates
//{
//    /// <summary>
//    /// Field scanning and analysis
//    /// </summary>
//    public class RichTemplateAnalyzer
//    {
//        private readonly IRichFieldManagerService _fieldService;

//        public RichTemplateAnalyzer(IRichFieldManagerService fieldManagementService)
//        {
//            _fieldService = fieldManagementService;
//        }

//        public RichTemplateDiagnosticsInfo AnalyzeTemplate(byte[] templateBytes)
//        {
//            using var server = new RichEditDocumentServer();
//            server.LoadDocument(new MemoryStream(templateBytes), DocumentFormat.OpenXml);
//            var doc = server.Document;

//            var allFields = _fieldService.FindDocumentFields(doc);
//            var mergeFields = allFields.Where(f => f.FieldCode.FieldType == RichFieldType.MergeField).ToList();
//            var tableStarts = allFields.Where(f => f.FieldCode.IsTableStart).ToList();
//            var tableEnds = allFields.Where(f => f.FieldCode.IsTableEnd).ToList();

//            return new RichTemplateDiagnosticsInfo
//            {
//                TotalFieldsCount = allFields.Count,
//                TotalMergeFieldsCount = mergeFields.Count,
//                CollectionFieldsCount = mergeFields.Count(f => f.FieldCode.IsCollectionField),
//                //TableStartCount = tableStarts.Count,
//                //TableEndCount = tableEnds.Count,
//                FieldPaths = mergeFields.Select(f => f.FieldCode.Property).Distinct().ToList(),
//                CollectionPaths = mergeFields
//                    .Where(f => f.FieldCode.IsCollectionField)
//                    .Select(f => f.FieldCode.CollectionPath)
//                    .Distinct()
//                    .ToList(),
//                HasFormattingIssues = !ValidateFormatting(allFields)
//            };
//        }

//        private bool ValidateFormatting(IRichFieldFindResultCollection fields)
//        {
//            // Check for common formatting issues
//            return true;
//        }

//        ///// <summary>
//        ///// Get diagnostic information about the template
//        ///// </summary>
//        //public RichTemplateDiagnostics AnalyzeTemplate(byte[] template)
//        //{
//        //    if (template == null) return new RichTemplateDiagnostics();

//        //    _inputServer.LoadDocumentContent(template, DocumentFormat.OpenXml);

//        //    var document = _inputServer.Document;

//        //    var diagnostics = new RichTemplateDiagnostics
//        //    {
//        //        TotalTablesCount = document.Tables.Count,
//        //        TotalMergeFieldsCount = document.Fields.Count(f =>
//        //        {
//        //            var code = document.GetText(f.CodeRange);
//        //            return code.StartsWith("MERGEFIELD", StringComparison.OrdinalIgnoreCase);
//        //        }),
//        //        CollectionTablesCount = 0,
//        //        RegularTablesCount = 0,
//        //        NestedTablesCount = 0
//        //    };

//        //    // Analyze each table
//        //    foreach (var table in document.Tables)
//        //    {
//        //        bool hasCollections = false;
//        //        bool hasNestedTables = false;

//        //        for (int r = 1; r < table.Rows.Count; r++)
//        //        {
//        //            var row = table.Rows[r];
//        //            foreach (TableCell cell in row.Cells)
//        //            {
//        //                var fieldsInCell = document.Fields.Get(cell.Range);
//        //                foreach (Field field in fieldsInCell)
//        //                {
//        //                    var code = document.GetText(field.CodeRange);
//        //                    if (code.StartsWith("MERGEFIELD", StringComparison.OrdinalIgnoreCase))
//        //                    {
//        //                        var cleanPath = ExtractFieldName(code);
//        //                        if (cleanPath.Contains("."))
//        //                        {
//        //                            hasCollections = true;
//        //                        }
//        //                    }
//        //                }
//        //            }

//        //            // Check for nested tables in cells
//        //            //if (row.Cells.Any(c => c.ContentRange.Paragraphs.Any(p => p.Range.Tables.Any())))
//        //            //{
//        //            //    hasNestedTables = true;
//        //            //}
//        //        }

//        //        if (hasCollections)
//        //            diagnostics.CollectionTablesCount++;
//        //        else
//        //            diagnostics.RegularTablesCount++;

//        //        if (hasNestedTables)
//        //            diagnostics.NestedTablesCount++;
//        //    }

//        //    return diagnostics;
//        //}
//    }
//}
