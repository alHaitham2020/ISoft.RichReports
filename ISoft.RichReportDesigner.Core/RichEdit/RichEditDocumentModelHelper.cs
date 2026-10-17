using DevExpress.XtraRichEdit.Fields;
using DevExpress.XtraRichEdit.Layout;
using DevExpress.XtraRichEdit.Model;
using DevExpress.XtraRichEdit.Utils;
using System;
using System.Text;

namespace ISoft.RichReportDesigner.Core.RichEdit
{
    public static class RichEditDocumentModelHelper
    {
        public static DocumentModelPosition GetCodeStartPosition(this Field field, PieceTable table)
        {
           return DocumentModelPosition.FromRunEnd(table, field.Code.Start);
        }

        public static DocumentModelPosition GetCodeEndPosition(this Field field, PieceTable table)
        {
            return DocumentModelPosition.FromRunStart(table, field.Code.End);
        }

        /// <summary>
        /// returns clean code without start and end {}
        /// </summary>
        /// <param name="field"></param>
        /// <param name="pieceTable"></param>
        /// <returns></returns>
        public static string GetCleanCodeText(this Field field, PieceTable pieceTable)
        {
            StringBuilder stringBuilder = new();
            var start = field.Code.Start + 1;
            var end = field.Code.End - 1;
            for (RunIndex runIndex = start; runIndex <= end; ++runIndex)
            {
                stringBuilder.Append(pieceTable.GetRunPlainText(runIndex));
            }

            return stringBuilder.ToString();
        }

        public static DocumentLogInterval GetFullCodeInterval(this Field field, PieceTable table)
        {
            DocumentModelPosition start = DocumentModelPosition.FromRunStart(table, field.FirstRunIndex);
            DocumentModelPosition end = DocumentModelPosition.FromRunStart(table, field.LastRunIndex + 1);
            int leng = Math.Abs(end.LogPosition - start.LogPosition);
            return ToLogInterval(start.LogPosition, leng);
        }

        public static DocumentLogInterval GetCodeInterval(this Field field, PieceTable table)
        {
            var start = DocumentModelPosition.FromRunEnd(table, field.Code.Start);
            var end = DocumentModelPosition.FromRunStart(table, field.Code.End);
            var leng = end.LogPosition - start.LogPosition;
            return ToLogInterval(start.LogPosition, leng);
        }

        public static DocumentLogInterval GetCleanCodeInterval(this Field field, PieceTable table)
        {
            var start = DocumentModelPosition.FromRunEnd(table, field.Code.Start + 1);
            var end = DocumentModelPosition.FromRunStart(table, field.Code.End - 1);
            var leng = end.LogPosition - start.LogPosition;
            return ToLogInterval(start.LogPosition, leng);
        }

        public static DocumentLogInterval GetResultInterval(this Field field, PieceTable table)
        {
            var start = DocumentModelPosition.FromRunStart(table, field.Result.Start);
            var end = DocumentModelPosition.FromRunStart(table, field.Result.End);
            var leng = end.LogPosition - start.LogPosition;
            return ToLogInterval(start.LogPosition, leng);
        }

        public static DocumentLogInterval ToLogInterval(this DocumentLogPosition position, int length)
        {
            return new DocumentLogInterval(position, length);
        }

        public static DocumentModelPosition ToDocumentModelPosition(this DocumentLogPosition logPosition, PieceTable table)
        {
            if (table == null) throw new ArgumentNullException(nameof(table));

            // ToDocumentModelPosition requires an *inclusive* position within
            // [DocumentStartLogPosition, DocumentEndLogPosition]. Clamp so a
            // stale / boundary position never blows up the command's UI-state pass.
            var clamped = ClampToDocument(logPosition, table);
            return PositionConverter.ToDocumentModelPosition(table, clamped);
        }

        public static RunInterval GetRunInterval(this Selection selection, PieceTable table)
        {
            if (selection == null || table == null) return null;

            // Selection.NormalizedStart is inclusive.
            // Selection.NormalizedEnd is *exclusive* (== NormalizedVirtualEnd + 1
            // when Length > 0). Convert to inclusive before hitting the converter
            // — same rule Selection.NormalizedVirtualEnd uses internally.
            DocumentLogPosition startLog = selection.NormalizedStart;
            DocumentLogPosition endLog = selection.Length > 0
                ? selection.NormalizedEnd - 1
                : selection.NormalizedEnd;

            var startPos = startLog.ToDocumentModelPosition(table);
            var endPos = endLog.ToDocumentModelPosition(table);

            return new RunInterval(startPos.RunIndex, endPos.RunIndex);
        }

        private static DocumentLogPosition ClampToDocument(DocumentLogPosition pos, PieceTable table)
        {
            if (pos < table.DocumentStartLogPosition) return table.DocumentStartLogPosition;
            if (pos > table.DocumentEndLogPosition) return table.DocumentEndLogPosition;
            return pos;
        }

        public static T Parse<T>(this Field field) where T : CalculatedFieldBase
        {
            RichEditDocumentModelFieldParser parser = new();
            return parser.ParseField<T>(field);
        }

        public static bool TryParse<T>(this Field field,out T result) where T : CalculatedFieldBase
        {
            RichEditDocumentModelFieldParser parser = new();
            return parser.TryParseField(field,out result);
        }

        public static Field FindFieldBySelection(Selection selection)
        {
            FieldController fieldController = new FieldController();
           return fieldController.FindFieldBySelection(selection);
        }

        public static Field FindTopmostFieldBySelection(Selection selection)
        {
            FieldController fieldController = new FieldController();
            return fieldController.FindTopmostFieldBySelection(selection);
        }
    }
}
