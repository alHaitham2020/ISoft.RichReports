//using DevExpress.Office.Utils;
//using DevExpress.Utils.About;
//using DevExpress.XtraRichEdit;
//using DevExpress.XtraRichEdit.API.Native;
//using System;
//using System.IO;
//using System.Threading;
//using System.Threading.Tasks;

//namespace ISoft.RichReportDesigner.Core.RichEdit
//{
//    /// <summary>
//    /// Provides asynchronous extension methods for saving documents from an <see cref="IRichDocumentEditControl"/> instance.
//    /// These methods offload export operations to a background thread while supporting cancellation and proper resource cleanup.
//    /// </summary>
//    public static class RichEditControlEx
//    {
//        private static byte[] SaveDocumentToBytes(IRichDocumentEditControl editControl, DocumentFormat documentFormat)
//        {
//            using MemoryStream memoryStream = new();
//            editControl.InnerControl.SaveDocument(memoryStream, documentFormat);
//            return memoryStream.ToArray();
//        }

//        /// <summary>
//        /// Executes an action asynchronously with cancellation support and document model context.
//        /// </summary>
//        /// <param name="action">The action to execute.</param>
//        /// <param name="editControl">The rich document edit control providing the document model.</param>
//        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
//        /// <returns>A task representing the asynchronous operation.</returns>
//        private static Task RunActionAsync(Action action, IRichDocumentEditControl editControl, CancellationToken cancellationToken)
//        {
//            // Wrap the action in a Func returning true to reuse RunFuncAsync implementation
//            return RunFuncAsync(
//                () =>
//                {
//                    action();
//                    return true;
//                },
//                editControl,
//                cancellationToken);
//        }

//        /// <summary>
//        /// Executes a function asynchronously with cancellation support and document model context.
//        /// </summary>
//        /// <typeparam name="T">The return type of the function.</typeparam>
//        /// <param name="action">The function to execute.</param>
//        /// <param name="editControl">The rich document edit control providing the document model.</param>
//        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
//        /// <returns>A task representing the asynchronous operation with the function result.</returns>
//        private static Task<T> RunFuncAsync<T>(Func<T> action, IRichDocumentEditControl editControl, CancellationToken cancellationToken)
//        {
//            // Offload the operation to a thread pool thread
//            return Task.Run(() =>
//            {
//                // Use CancellationHelper to properly handle cancellation during document operations
//                using (new CancellationHelper(editControl.DocumentModel, cancellationToken))
//                {
//                    return action();
//                }
//            });
//        }

//        /// <summary>
//        /// Exports a document using the specified action, wrapping it with component-type tracking.
//        /// </summary>
//        /// <param name="editControl">The rich document edit control.</param>
//        /// <param name="export">The export action to execute.</param>
//        internal static void ExportWithComponentType(IRichDocumentEditControl editControl, Action export)
//        {
//            OfficeExportHelper.ExportWithComponentType(editControl.DocumentModel, typeof(RichEditControlEx), export);
//        }

//        /// <summary>
//        /// Exports a document using the specified function, wrapping it with component-type tracking.
//        /// </summary>
//        /// <typeparam name="T">The return type of the export function.</typeparam>
//        /// <param name="editControl">The rich document edit control.</param>
//        /// <param name="export">The export function to execute.</param>
//        /// <returns>The result of the export function.</returns>
//        internal static T ExportWithComponentType<T>(IRichDocumentEditControl editControl, Func<T> export)
//        {
//            return OfficeExportHelper.ExportWithComponentType(editControl.DocumentModel, typeof(RichEditControlEx), export);
//        }

//        #region SaveDocumentAsync Overloads - File Path

//        /// <summary>
//        /// Asynchronously saves the document to a file with the specified document format.
//        /// </summary>
//        /// <param name="editControl">The current <see cref="IRichDocumentEditControl"/> instance.</param>
//        /// <param name="fileName">The path to the file to which to save the document.</param>
//        /// <param name="documentFormat">The enumeration value that indicates the document format.</param>
//        /// <returns>An asynchronous export operation.</returns>
//        public static Task SaveDocumentAsync(this IRichDocumentEditControl editControl, string fileName, DocumentFormat documentFormat)
//        {
//            return RunActionAsync(
//                () => ExportWithComponentType(editControl, () => editControl.InnerControl.SaveDocument(fileName, documentFormat)),
//                editControl,
//                CancellationToken.None);
//        }

//        /// <summary>
//        /// Asynchronously saves the document to a file with the specified document format and cancellation support.
//        /// </summary>
//        /// <param name="editControl">The current <see cref="IRichDocumentEditControl"/> instance.</param>
//        /// <param name="fileName">The path to the file to which to save the document.</param>
//        /// <param name="documentFormat">The enumeration value that indicates the document format.</param>
//        /// <param name="cancellationToken">A CancellationToken object used to trace requests to cancel an operation.</param>
//        /// <returns>An asynchronous export operation.</returns>
//        public static Task SaveDocumentAsync(this IRichDocumentEditControl editControl, string fileName, DocumentFormat documentFormat, CancellationToken cancellationToken)
//        {
//            return RunActionAsync(
//                () => ExportWithComponentType(editControl, () => editControl.InnerControl.SaveDocument(fileName, documentFormat)),
//                editControl,
//                cancellationToken);
//        }

//        /// <summary>
//        /// Asynchronously saves the document to a file with the specified document format and encryption options.
//        /// </summary>
//        /// <param name="editControl">The current <see cref="IRichDocumentEditControl"/> instance.</param>
//        /// <param name="fileName">The path to the file to which to save the document.</param>
//        /// <param name="documentFormat">The enumeration value that indicates the document format.</param>
//        /// <param name="encryptionSettings">An object that contains document encryption options.</param>
//        /// <returns>An asynchronous export operation.</returns>
//        public static Task SaveDocumentAsync(this IRichDocumentEditControl editControl, string fileName, DocumentFormat documentFormat, EncryptionSettings encryptionSettings)
//        {
//            return RunActionAsync(
//                () => ExportWithComponentType(editControl, () => editControl.InnerControl.SaveDocument(fileName, documentFormat, encryptionSettings)),
//                editControl,
//                CancellationToken.None);
//        }

//        /// <summary>
//        /// Asynchronously saves the document to a file with the specified document format, encryption options, and cancellation support.
//        /// </summary>
//        /// <param name="editControl">The current <see cref="IRichDocumentEditControl"/> instance.</param>
//        /// <param name="fileName">The path to the file to which to save the document.</param>
//        /// <param name="documentFormat">The enumeration value that indicates the document format.</param>
//        /// <param name="encryptionSettings">An object that contains document encryption options.</param>
//        /// <param name="cancellationToken">A CancellationToken object used to trace requests to cancel an operation.</param>
//        /// <returns>An asynchronous export operation.</returns>
//        public static Task SaveDocumentAsync(this IRichDocumentEditControl editControl, string fileName, DocumentFormat documentFormat, EncryptionSettings encryptionSettings, CancellationToken cancellationToken)
//        {
//            return RunActionAsync(
//                () => ExportWithComponentType(editControl, () => editControl.InnerControl.SaveDocument(fileName, documentFormat, encryptionSettings)),
//                editControl,
//                cancellationToken);
//        }

//        #endregion

//        #region SaveDocumentAsync Overloads - Stream

//        /// <summary>
//        /// Asynchronously saves the document to a stream with the specified document format.
//        /// </summary>
//        /// <param name="editControl">The current <see cref="IRichDocumentEditControl"/> instance.</param>
//        /// <param name="stream">The stream to which to output the document.</param>
//        /// <param name="documentFormat">The enumeration value that indicates the document format.</param>
//        /// <returns>An asynchronous export operation.</returns>
//        public static Task SaveDocumentAsync(this IRichDocumentEditControl editControl, Stream stream, DocumentFormat documentFormat)
//        {
//            return RunActionAsync(
//                () => ExportWithComponentType(editControl, () => editControl.InnerControl.SaveDocument(stream, documentFormat)),
//                editControl,
//                CancellationToken.None);
//        }

//        /// <summary>
//        /// Asynchronously saves the document to a stream with the specified document format and cancellation support.
//        /// </summary>
//        /// <param name="editControl">The current <see cref="IRichDocumentEditControl"/> instance.</param>
//        /// <param name="stream">The stream to which to output the document.</param>
//        /// <param name="documentFormat">The enumeration value that indicates the document format.</param>
//        /// <param name="cancellationToken">A CancellationToken object used to trace requests to cancel an operation.</param>
//        /// <returns>An asynchronous export operation.</returns>
//        public static Task SaveDocumentAsync(this IRichDocumentEditControl editControl, Stream stream, DocumentFormat documentFormat, CancellationToken cancellationToken)
//        {
//            return RunActionAsync(
//                () => ExportWithComponentType(editControl, () => editControl.InnerControl.SaveDocument(stream, documentFormat)),
//                editControl,
//                cancellationToken);
//        }

//        /// <summary>
//        /// Asynchronously saves the document to a stream with the specified document format and encryption options.
//        /// </summary>
//        /// <param name="editControl">The current <see cref="IRichDocumentEditControl"/> instance.</param>
//        /// <param name="stream">The stream used to output the document.</param>
//        /// <param name="documentFormat">Indicates the document format.</param>
//        /// <param name="encryptionSettings">An object that contains document encryption options.</param>
//        /// <returns>A Task object that represents an asynchronous export operation.</returns>
//        public static Task SaveDocumentAsync(this IRichDocumentEditControl editControl, Stream stream, DocumentFormat documentFormat, EncryptionSettings encryptionSettings)
//        {
//            return RunActionAsync(
//                () => ExportWithComponentType(editControl, () => editControl.InnerControl.SaveDocument(stream, documentFormat, encryptionSettings)),
//                editControl,
//                CancellationToken.None);
//        }

//        /// <summary>
//        /// Asynchronously saves the document to a stream with the specified document format, encryption options, and cancellation support.
//        /// </summary>
//        /// <param name="editControl">The current <see cref="IRichDocumentEditControl"/> instance.</param>
//        /// <param name="stream">The stream to which to output the document.</param>
//        /// <param name="documentFormat">The enumeration value that indicates the document format.</param>
//        /// <param name="encryptionSettings">An object that contains document encryption options.</param>
//        /// <param name="cancellationToken">A CancellationToken object used to trace requests to cancel an operation.</param>
//        /// <returns>An asynchronous export operation.</returns>
//        public static Task SaveDocumentAsync(this IRichDocumentEditControl editControl, Stream stream, DocumentFormat documentFormat, EncryptionSettings encryptionSettings, CancellationToken cancellationToken)
//        {
//            return RunActionAsync(
//                () => ExportWithComponentType(editControl, () => editControl.InnerControl.SaveDocument(stream, documentFormat, encryptionSettings)),
//                editControl,
//                cancellationToken);
//        }

//        #endregion

//        #region SaveDocumentAsync Overloads - Byte Array

//        /// <summary>
//        /// Asynchronously saves the document to a byte array in the specified file format.
//        /// </summary>
//        /// <param name="editControl">The current <see cref="IRichDocumentEditControl"/> instance.</param>
//        /// <param name="documentFormat">An enumeration member that specifies the output document format.</param>
//        /// <returns>Returns a byte array that contains data in the specified format.</returns>
//        public static Task<byte[]> SaveDocumentAsync(this IRichDocumentEditControl editControl, DocumentFormat documentFormat)
//        {
//            return RunFuncAsync(
//                () => ExportWithComponentType(editControl, () => SaveDocumentToBytes(editControl, documentFormat)),
//                editControl,
//                CancellationToken.None);
//        }

//        /// <summary>
//        /// Asynchronously saves the document to a byte array in the specified file format with cancellation support.
//        /// </summary>
//        /// <param name="editControl">The current <see cref="IRichDocumentEditControl"/> instance.</param>
//        /// <param name="documentFormat">An enumeration member that specifies the output document format.</param>
//        /// <param name="cancellationToken">A cancellation token that indicates whether to cancel the operation.</param>
//        /// <returns>Returns a byte array that contains data in the specified format.</returns>
//        public static Task<byte[]> SaveDocumentAsync(this IRichDocumentEditControl editControl, DocumentFormat documentFormat, CancellationToken cancellationToken)
//        {
//            return RunFuncAsync(
//                () => ExportWithComponentType(editControl, () => SaveDocumentToBytes(editControl, documentFormat)),
//                editControl,
//                cancellationToken);
//        }

//        /// <summary>
//        /// Asynchronously saves the document to a byte array in the specified format and encrypts it with a password.
//        /// </summary>
//        /// <param name="editControl">The current <see cref="IRichDocumentEditControl"/> instance.</param>
//        /// <param name="documentFormat">An enumeration member that specifies the output document format.</param>
//        /// <param name="encryptionSettings">Specifies encryption settings.</param>
//        /// <returns>Returns a byte array that contains encrypted data in the specified format.</returns>
//        public static Task<byte[]> SaveDocumentAsync(this IRichDocumentEditControl editControl, DocumentFormat documentFormat, EncryptionSettings encryptionSettings)
//        {
//            return RunFuncAsync(
//                () => ExportWithComponentType(editControl, () => editControl.InnerControl.SaveDocument(documentFormat, encryptionSettings)),
//                editControl,
//                CancellationToken.None);
//        }

//        /// <summary>
//        /// Asynchronously saves the document to a byte array in the specified format, encrypts it with a password, and supports cancellation.
//        /// </summary>
//        /// <param name="editControl">The current <see cref="IRichDocumentEditControl"/> instance.</param>
//        /// <param name="documentFormat">An enumeration member that specifies the output document format.</param>
//        /// <param name="encryptionSettings">Specifies encryption settings.</param>
//        /// <param name="cancellationToken">A cancellation token that indicates whether to cancel the operation.</param>
//        /// <returns>Returns a byte array that contains encrypted data in the specified format.</returns>
//        public static Task<byte[]> SaveDocumentAsync(this IRichDocumentEditControl editControl, DocumentFormat documentFormat, EncryptionSettings encryptionSettings, CancellationToken cancellationToken)
//        {
//            return RunFuncAsync(
//                () => ExportWithComponentType(editControl, () => editControl.InnerControl.SaveDocument(documentFormat, encryptionSettings)),
//                editControl,
//                cancellationToken);
//        }

//        #endregion
//    }
//}
