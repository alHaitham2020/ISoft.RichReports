using DevExpress.XtraRichEdit.API.Native;

namespace ISoft.RichReportDesigner.Core.Fields.Search
{
    public interface IRichDocField
    {
        SubDocumentType DocumentType { get; }

        Field Field { get; }
    }

    public static class RichFieldsDiscovery
    {
        private sealed class RichNativeFieldInfo : IRichDocField
        {
            public RichNativeFieldInfo(Field field, SubDocumentType documentType)
            {
                Field = field;
                DocumentType = documentType;
            }

            public Field Field { get; }

            public SubDocumentType DocumentType { get; }
        }

        /// <summary>
        /// Collects fields from the document according to the specified scope.
        /// </summary>
        /// <param name="document">The document to inspect.</param>
        /// <param name="scope">Which parts to include. Defaults to everything.</param>
        /// <returns>A list of all matched fields.</returns>
        public static IRichFieldCollection GetFields(Document document, RichFieldSearchScope scope = RichFieldSearchScope.All)
        {
            var results = new RichFieldCollection();
            if (document == null) return results;

            // 1. Body
            if (scope.HasFlag(RichFieldSearchScope.Body))
            {
                AddFields(document, results);
            }

            // 2. Headers & Footers across all sections
            if ((scope & RichFieldSearchScope.AllHeadersAndFooters) != 0)
            {
                foreach (Section section in document.Sections)
                {
                    if (scope.HasFlag(RichFieldSearchScope.HeaderFirst))
                        ProcessHeaderFooter(section, HeaderFooterType.First, isHeader: true, results);

                    if (scope.HasFlag(RichFieldSearchScope.HeaderOdd))
                        ProcessHeaderFooter(section, HeaderFooterType.Odd, isHeader: true, results);

                    if (scope.HasFlag(RichFieldSearchScope.HeaderEven))
                        ProcessHeaderFooter(section, HeaderFooterType.Even, isHeader: true, results);

                    if (scope.HasFlag(RichFieldSearchScope.FooterFirst))
                        ProcessHeaderFooter(section, HeaderFooterType.First, isHeader: false, results);

                    if (scope.HasFlag(RichFieldSearchScope.FooterOdd))
                        ProcessHeaderFooter(section, HeaderFooterType.Odd, isHeader: false, results);

                    if (scope.HasFlag(RichFieldSearchScope.FooterEven))
                        ProcessHeaderFooter(section, HeaderFooterType.Even, isHeader: false, results);
                }
            }

            return results;
        }

        private static void ProcessHeaderFooter(
            Section section,
            HeaderFooterType type,
            bool isHeader,
            RichFieldCollection results)
        {
            SubDocument subDoc = null;

            try
            {
                if (isHeader)
                {
                    if (!section.HasHeader(type)) return;
                    subDoc = section.BeginUpdateHeader(type);
                    AddFields(subDoc, results);
                }
                else
                {
                    if (!section.HasFooter(type)) return;
                    subDoc = section.BeginUpdateFooter(type);
                    AddFields(subDoc, results);
                }
            }
            finally
            {
                if (subDoc != null)
                {
                    if (isHeader) section.EndUpdateHeader(subDoc);
                    else section.EndUpdateFooter(subDoc);
                }
            }
        }

        private static void AddFields(SubDocument subDoc, RichFieldCollection results)
        {
            var documentType = subDoc.GetSubDocumentType();

            for (int i = 0; i < subDoc.Fields.Count; i++)
            {
                results.Add(new RichNativeFieldInfo(subDoc.Fields[i], documentType));
            }
        }
    }
}
