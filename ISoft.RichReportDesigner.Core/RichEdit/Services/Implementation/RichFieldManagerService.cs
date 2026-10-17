using DevExpress.Mvvm.Native;
using DevExpress.XtraRichEdit.API.Native;
using ISoft.RichReportDesigner.Core.Fields.Codes;
using ISoft.RichReportDesigner.Core.Fields.Core;
using ISoft.RichReportDesigner.Core.Fields.Format;
using ISoft.RichReportDesigner.Core.Fields.Instructions;
using ISoft.RichReportDesigner.Core.Fields.Options;
using ISoft.RichReportDesigner.Core.Fields.Regions;
using ISoft.RichReportDesigner.Core.Fields.Search;
using ISoft.RichReportDesigner.Core.Fields.Switches;
using ISoft.RichReportDesigner.Core.Images;
using ISoft.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
namespace ISoft.RichReportDesigner.Core.RichEdit.Services.Implementation
{
    /// <summary>
    /// Comprehensive field management service for creating, scanning, updating, 
    /// and formatting fields in RichEdit documents.
    /// Provides a unified API for all field operations with proper error handling and performance optimization.
    /// </summary>
    /// <remarks>
    /// This service is the main entry point for field manipulation in documents.
    /// It coordinates between scanner, builder, and switch builder components.
    /// Thread-safety: Instance methods are not thread-safe. Create new instances per thread if needed.
    /// 
    /// Key responsibilities:
    /// - Field creation (merge fields, date/time, tables, conditional fields)
    /// - Field scanning and detection across document ranges
    /// - Field modification (paths, formats, arguments)
    /// - Field removal and cleanup
    /// - Format management and conversion
    /// </remarks>
    //public class RichFieldManagerService : IRichFieldManagerService
    //{
    //    #region Dependencies
    //    private readonly IRichFieldCodeScanner _scanner;
    //    private readonly IRichFieldCodeBuilder _codeBuilder;
    //    private readonly IRichFieldSwitchBuilder _switchBuilder;
    //    #endregion

    //    #region Constructor
    //    /// <summary>
    //    /// Initializes a new instance with custom component implementations.
    //    /// Allows dependency injection for testing and extensibility.
    //    /// </summary>
    //    /// <param name="scanner">Field code scanner for parsing field codes</param>
    //    /// <param name="builder">Field code builder for constructing field codes</param>
    //    /// <param name="switchBuilder">Switch builder for format switch management</param>
    //    public RichFieldManagerService(
    //        IRichFieldCodeScanner scanner = null,
    //        IRichFieldCodeBuilder builder = null,
    //        IRichFieldSwitchBuilder switchBuilder = null)
    //    {
    //        _scanner = scanner ?? new RichFieldCodeScanner();
    //        _switchBuilder = switchBuilder ?? new RichFieldSwitchBuilder();
    //        _codeBuilder = builder ?? new RichFieldCodeBuilder(new RichFieldInstructionsBuilder(_switchBuilder));
    //    }

    //    #endregion

    //    #region Field Creation

    //    /// <summary>
    //    /// Creates a merge field with optional format switch at the specified position.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="position">Insertion position</param>
    //    /// <param name="fieldPath">Field path/property name</param>
    //    /// <param name="format">Optional format string (e.g., "Upper", "MMMM d, yyyy")</param>
    //    /// <returns>The created field</returns>
    //    public Field CreateMergeField(Document document, DocumentPosition position, string fieldPath, RichFieldFormat? format = null)
    //    {
    //        var fieldCode = _codeBuilder.CreateMergeField(fieldPath, format);
    //        return CreateField(document, position, fieldCode);
    //    }

    //    /// <summary>
    //    /// Creates a merge field with a specific formatter for advanced formatting options.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="position">Insertion position</param>
    //    /// <param name="fieldPath">Field path/property name</param>
    //    /// <param name="fieldFormat">Formatter with field-specific formatting options</param>
    //    /// <returns>The created field</returns>
    //    public Field CreateMergeField(Document document, DocumentPosition position, RichMergeFieldOptions options)
    //    {
    //        var fieldCode = _codeBuilder.CreateMergeField(options);
    //        return CreateField(document, position, fieldCode);
    //    }

    //    /// <summary>
    //    /// Creates an INCLUDEPICTURE field with formatter options.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="position">Insertion position</param>
    //    /// <param name="uri">Image URI/path</param>
    //    /// <param name="fieldFormat">Picture-specific formatting options</param>
    //    /// <returns>The created field</returns>
    //    public Field CreatePictureField(Document document, DocumentPosition position, RichIncludePictureFieldOptions options)
    //    {
    //        var fieldCode = _codeBuilder.CreatePictureField(options);
    //        return CreateField(document, position, fieldCode);
    //    }

    //    /// <summary>
    //    /// Creates an INCLUDEPICTURE field with default formatting options.
    //    /// Default options: preserve formatting, suppress graphics data storage, 100x100 dimensions.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="position">Insertion position</param>
    //    /// <param name="uri">Image URI/path</param>
    //    /// <returns>The created field</returns>
    //    public Field CreatePictureField(Document document, DocumentPosition position, string uri)
    //    {
    //        var fieldCode = _codeBuilder.CreatePictureField(uri);
    //        return CreateField(document, position, fieldCode);
    //    }

    //    /// <summary>
    //    /// Creates an INCLUDEPICTURE field with default formatting options.
    //    /// Default options: preserve formatting, suppress graphics data storage, 100x100 dimensions.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="position">Insertion position</param>
    //    /// <param name="uri">Image URI/path</param>
    //    /// <returns>The created field</returns>
    //    public Field CreatePictureFieldFromImageUri(Document document, DocumentPosition position, ImageUri uri)
    //    {
    //       return CreatePictureField(document, position, uri);
    //    }

    //    ///// <summary>
    //    ///// Creates a nested INCLUDEPICTURE field containing a field for dynamic image paths.
    //    ///// This creates a composite field structure: INCLUDEPICTURE with embedded MERGEFIELD.
    //    ///// </summary>
    //    ///// <param name="document">Target document</param>
    //    ///// <param name="position">Insertion position</param>
    //    ///// <param name="fieldPath">field path for the image source</param>
    //    ///// <param name="fieldFormat">Picture formatting options (dimensions, switches)</param>
    //    ///// <returns>The outer INCLUDEPICTURE field</returns>
    //    ///// <remarks>
    //    ///// Creates field structure:
    //    ///// { INCLUDEPICTURE "{ Field fieldPath }" \w 100 \h 100 \* MERGEFORMAT }
    //    ///// 
    //    ///// This approach preserves the Field reference within the INCLUDEPICTURE code,
    //    ///// allowing dynamic image resolution at merge time.
    //    ///// </remarks>
    //    //public Field CreateNestedPictureField(
    //    //    Document document,
    //    //    DocumentPosition position,
    //    //    RichCalculatedFieldType nestedFieldType,
    //    //    string nestedFieldFirstArgument)
    //    //{
    //    //    // Initialize with sensible defaults for document embedding
    //    //    var pictureFieldOptions = RichIncludePictureFieldOptions.CreateDefault();
    //    //    return CreateNestedPictureField(document, position, nestedFieldType, nestedFieldFirstArgument, pictureFieldOptions);
    //    //}

    //    ///// <summary>
    //    ///// Creates a nested INCLUDEPICTURE field containing a field for dynamic image paths.
    //    ///// This creates a composite field structure: INCLUDEPICTURE with embedded MERGEFIELD.
    //    ///// </summary>
    //    ///// <param name="document">Target document</param>
    //    ///// <param name="position">Insertion position</param>
    //    ///// <param name="fieldPath">field path for the image source</param>
    //    ///// <param name="fieldFormat">Picture formatting options (dimensions, switches)</param>
    //    ///// <returns>The outer INCLUDEPICTURE field</returns>
    //    ///// <remarks>
    //    ///// Creates field structure:
    //    ///// { INCLUDEPICTURE "{ Field fieldPath }" \w 100 \h 100 \* MERGEFORMAT }
    //    ///// 
    //    ///// This approach preserves the Field reference within the INCLUDEPICTURE code,
    //    ///// allowing dynamic image resolution at merge time.
    //    ///// </remarks>
    //    //public Field CreateNestedPictureField(
    //    //    Document document,
    //    //    DocumentPosition position,
    //    //    RichCalculatedFieldType nestedFieldType,
    //    //    string nestedFieldFirstArgument,
    //    //    RichIncludePictureFieldOptions pictureFieldOptions)
    //    //{
    //    //    ArgumentGuardHelper.ArgumentNotNull(pictureFieldOptions, nameof(pictureFieldOptions));

    //    //    // skip nested field and create the actual pricure field
    //    //    if (nestedFieldType == RichCalculatedFieldType.IncludePictureField)
    //    //    {
    //    //        return CreatePictureField(document, position, pictureFieldOptions.WithUri(nestedFieldFirstArgument));
    //    //    }

    //    //    // Step 1: Create outer INCLUDEPICTURE field with placeholder
    //    //    // Using space as placeholder to establish field boundaries 
    //    //    // define empty options becouse the actual options is assigned after the inner field
    //    //    Field outerField = CreatePictureField(document, position, new RichIncludePictureFieldOptions()); 

    //    //    // append space 
    //    //    var spaceRange = document.InsertText(document.CreatePosition(outerField.CodeRange.End.ToInt()), " ");

    //    //    // Step 3: Create nested field for dynamic image path resolution
    //    //    CreateFieldWithCommonOptions(document, spaceRange.End, nestedFieldType, nestedFieldFirstArgument);

    //    //    // Step 4: Position cursor after inner field to append formatting switches
    //    //    DocumentPosition closingPosition = document.CreatePosition(outerField.CodeRange.End.ToInt());

    //    //    // Step 5: Append dimension and formatting switches after the inner field
    //    //    if (pictureFieldOptions != null)
    //    //    {
    //    //        var switchesStr = _codeBuilder.GetFieldSwitchesBuildString(pictureFieldOptions);
    //    //        if (!string.IsNullOrEmpty(switchesStr))
    //    //        {
    //    //            document.InsertText(closingPosition, switchesStr);
    //    //        }
    //    //    }

    //    //    return outerField;
    //    //}

    //    public Field CreateNestedField(
    //        Document document,
    //        DocumentPosition position,
    //        RichNestedFieldOptions options)
    //    {
    //        ArgumentGuardHelper.ArgumentNotNull(options, nameof(options));
    //        ArgumentGuardHelper.ArgumentNotNull(options.OuterFieldOptions, nameof(options.OuterFieldOptions));
    //        ArgumentGuardHelper.ArgumentNotNull(options.InnerFieldOptions, nameof(options.InnerFieldOptions));

    //        // Step 1: Create outer field with placeholder
    //        // Using space as placeholder to establish field boundaries 
    //        // define empty options becouse the actual options is assigned after the inner field
    //        Field outerField = CreateField(document, position, options.OuterFieldOptions.FieldType);

    //        // append space 
    //        var spaceRange = document.InsertText(document.CreatePosition(outerField.CodeRange.End.ToInt()), " ");

    //        // Step 3: Create nested field for dynamic data
    //        CreateFieldWithOptions(document, spaceRange.End, options.InnerFieldOptions);

    //        // Step 4: Position cursor after inner field to append formatting switches
    //        DocumentPosition closingPosition = document.CreatePosition(outerField.CodeRange.End.ToInt());

    //        // Step 5: Append switches and formatting switches after the inner field
    //        if (options.OuterFieldOptions != null)
    //        {
    //            var switchesStr = _codeBuilder.GetFieldSwitchesBuildString(options.OuterFieldOptions);
    //            if (!string.IsNullOrEmpty(switchesStr))
    //            {
    //                document.InsertText(closingPosition, switchesStr);
    //            }
    //        }

    //        return outerField;
    //    }

    //    /// <summary>
    //    /// Creates a nested INCLUDEPICTURE field containing a MERGEFIELD for dynamic image paths.
    //    /// This creates a composite field structure: INCLUDEPICTURE with embedded MERGEFIELD.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="position">Insertion position</param>
    //    /// <param name="mergeFieldPath">Merge field path for the image source</param>
    //    /// <param name="fieldFormat">Picture formatting options (dimensions, switches)</param>
    //    /// <returns>The outer INCLUDEPICTURE field</returns>
    //    /// <remarks>
    //    /// Creates field structure:
    //    /// { INCLUDEPICTURE "{ MERGEFIELD fieldPath }" \w 100 \h 100 \* MERGEFORMAT }
    //    /// 
    //    /// This approach preserves the MERGEFIELD reference within the INCLUDEPICTURE code,
    //    /// allowing dynamic image resolution at merge time.
    //    /// </remarks>
    //    public Field CreateMergePictureField(Document document, DocumentPosition position, RichMergeFieldOptions mergeFieldOptions, RichIncludePictureFieldOptions pictureFieldOptions)
    //    {
    //        var nestedFieldOptions = RichNestedFieldOptions.CreateIncludePictureWithInnerOptions(pictureFieldOptions, mergeFieldOptions);

    //        return CreateNestedField(document, position, nestedFieldOptions);
    //    }

    //    /// <summary>
    //    /// Creates a merge picture field with default formatting options.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="position">Insertion position</param>
    //    /// <param name="mergeFieldPath">Merge field path for the image source</param>
    //    /// <returns>The created INCLUDEPICTURE field</returns>
    //    public Field CreateMergePictureField(Document document, DocumentPosition position, string mergeFieldPath)
    //    {
    //        var mergeFieldOptions = RichMergeFieldOptions.Create(mergeFieldPath);
    //        var includePictureFieldOptions = RichIncludePictureFieldOptions.CreateDefault();

    //        return CreateMergePictureField(document, position, mergeFieldOptions, includePictureFieldOptions);
    //    }

    //    /// <summary>
    //    /// Creates a TableStart field to mark the beginning of a collection region.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="position">Insertion position</param>
    //    /// <param name="tableName">Collection path identifier</param>
    //    /// <returns>The created TableStart field</returns>
    //    public Field CreateTableStartField(Document document, DocumentPosition position, string tableName)
    //    {
    //        var fieldCode = _codeBuilder.CreateTableStart(tableName);
    //        return CreateField(document, position, fieldCode);
    //    }

    //    /// <summary>
    //    /// Creates a TableEnd field to mark the end of a collection region.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="position">Insertion position</param>
    //    /// <param name="tableName">Collection path identifier</param>
    //    /// <returns>The created TableEnd field</returns>
    //    public Field CreateTableEndField(Document document, DocumentPosition position, string tableName)
    //    {
    //        var fieldCode = _codeBuilder.CreateTableEnd(tableName);
    //        return CreateField(document, position, fieldCode);
    //    }

    //    /// <summary>
    //    /// Creates a DATE field with optional format switch.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="position">Insertion position</param>
    //    /// <param name="format">Optional date format (e.g., "MMMM d, yyyy")</param>
    //    /// <returns>The created DATE field</returns>
    //    public Field CreateDateField(Document document, DocumentPosition position, RichDateBasedFieldOptions options)
    //    {
    //        var fieldCode = _codeBuilder.CreateDateField(options);
    //        return CreateField(document, position, fieldCode);
    //    }

    //    /// <summary>
    //    /// Creates a DATE field with optional format switch.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="position">Insertion position</param>
    //    /// <param name="format">Optional date format (e.g., "MMMM d, yyyy")</param>
    //    /// <returns>The created DATE field</returns>
    //    public Field CreateDateField(Document document, DocumentPosition position, RichFieldFormat? format = null)
    //    {
    //        var fieldCode = _codeBuilder.CreateDateField(format);
    //        return CreateField(document, position, fieldCode);
    //    }

    //    /// <summary>
    //    /// Creates a TIME field with optional format switch.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="position">Insertion position</param>
    //    /// <param name="format">Optional time format (e.g., "HH:mm:ss")</param>
    //    /// <returns>The created TIME field</returns>
    //    public Field CreateTimeField(Document document, DocumentPosition position, RichFieldFormat? format = null)
    //    {
    //        var fieldCode = _codeBuilder.CreateTimeField(format);
    //        return CreateField(document, position, fieldCode);
    //    }

    //    /// <summary>
    //    /// Creates a PAGE field to display the current page number.
    //    /// </summary>
    //    public Field CreatePageField(Document document, DocumentPosition position)
    //    {
    //        var fieldCode = _codeBuilder.CreatePageField();
    //        return CreateField(document, position, fieldCode);
    //    }

    //    /// <summary>
    //    /// Creates a NUMPAGES field to display the total number of pages.
    //    /// </summary>
    //    public Field CreateNumPagesField(Document document, DocumentPosition position)
    //    {
    //        var fieldCode = _codeBuilder.CreateNumPagesField();
    //        return CreateField(document, position, fieldCode);
    //    }

    //    /// <summary>
    //    /// Creates a "Page X of Y" construct using PAGE and NUMPAGES fields at the current caret position.
    //    /// Inserts: { PAGE } of { NUMPAGES }
    //    /// </summary>
    //    public void CreatePageOfNumPagesField(Document document)
    //    {
    //        if (document == null) throw new ArgumentNullException(nameof(document));

    //        CreatePageField(document, document.CaretPosition);
    //        document.InsertText(document.CaretPosition, " of ");
    //        CreateNumPagesField(document, document.CaretPosition);
    //    }

    //    /// <summary>
    //    /// Creates an IF field for conditional content display.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="position">Insertion position</param>
    //    /// <param name="expression">Conditional expression</param>
    //    /// <param name="trueText">Text to display when condition is true</param>
    //    /// <param name="falseText">Text to display when condition is false</param>
    //    /// <returns>The created IF field</returns>
    //    public Field CreateIfField(Document document, DocumentPosition position, string expression1, string cmOperator, string expression2)
    //    {
    //        var fieldCode = _codeBuilder.CreateIfField(expression1, cmOperator, expression2);
    //        return CreateField(document, position, fieldCode);
    //    }

    //    /// <summary>
    //    /// Creates an IF field for conditional content display.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="position">Insertion position</param>
    //    /// <param name="expression">Conditional expression</param>
    //    /// <param name="trueText">Text to display when condition is true</param>
    //    /// <param name="falseText">Text to display when condition is false</param>
    //    /// <returns>The created IF field</returns>
    //    public Field CreateIfField(Document document, DocumentPosition position, RichIfFieldOptions options)
    //    {
    //        var fieldCode = _codeBuilder.CreateIfField(options);
    //        return CreateField(document, position, fieldCode);
    //    }

    //    /// <summary>
    //    /// Creates a field in a specific table cell at either the start or end position.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="cell">Target table cell</param>
    //    /// <param name="fieldCode">Field code to insert</param>
    //    /// <param name="atStart">True to insert at cell start, false for cell end</param>
    //    /// <returns>The created field</returns>
    //    public Field CreateFieldInCell(Document document, TableCell cell, IRichFieldCode fieldCode, bool atStart)
    //    {
    //        if (document == null) throw new ArgumentNullException(nameof(document));
    //        if (cell == null) throw new ArgumentNullException(nameof(cell));

    //        var position = atStart ? cell.ContentRange.Start : cell.ContentRange.End;
    //        return CreateField(document, position, fieldCode);
    //    }

    //    /// <summary>
    //    /// Creates a field with specified type, path, and optional format.
    //    /// Generic field creation method that handles formatter initialization based on field type.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="position">Insertion position</param>
    //    /// <param name="fieldType">Type of field to create</param>
    //    /// <param name="fieldPath">Field path/property name</param>
    //    /// <param name="format">Optional format string</param>
    //    /// <returns>The created field</returns>
    //    public Field CreateFieldWithCommonOptions(Document document, DocumentPosition position, RichFieldType fieldType, string firstArguemnt, RichFieldFormat? format = null)
    //    {
    //        var fieldCode = _codeBuilder.CreateFieldWithCommonOptions(fieldType, firstArguemnt, format);
    //        return CreateField(document, position, fieldCode);
    //    }

    //    /// <summary>
    //    /// Creates a field with specified type, path, and optional format.
    //    /// Generic field creation method that handles formatter initialization based on field type.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="position">Insertion position</param>
    //    /// <param name="fieldType">Type of field to create</param>
    //    /// <param name="fieldPath">Field path/property name</param>
    //    /// <param name="format">Optional format string</param>
    //    /// <returns>The created field</returns>
    //    public Field CreateFieldWithOptions(Document document, DocumentPosition position, object options)
    //    {
    //        if (options is RichNestedFieldOptions nestedFieldOptions)
    //        {
    //            return CreateNestedField(document, position, nestedFieldOptions);
    //        }
    //        else
    //        {
    //            var fieldCode = _codeBuilder.CreateFieldWithOptions(options);
    //            return CreateField(document, position, fieldCode);
    //        }
    //    }

    //    protected Field CreateField(Document document, DocumentPosition position, RichFieldType fieldType)
    //    {
    //        var fieldCode = _codeBuilder.CreateField(fieldType);
    //        return CreateField(document, position, fieldCode);
    //    }

    //    /// <summary>
    //    /// Core method for field creation. Applies formatter, builds field code, and creates the field in the document.
    //    /// This is the single point of field insertion ensuring consistent behavior.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="position">Insertion position</param>
    //    /// <param name="fieldCode">Structured field code to insert</param>
    //    /// <returns>The created field</returns>
    //    /// <exception cref="ArgumentNullException">Thrown if document, position, or fieldCode is null</exception>
    //    protected Field CreateField(Document document, DocumentPosition position, IRichFieldCode fieldCode)
    //    {
    //        // Guard clauses
    //        ArgumentGuardHelper.ArgumentNotNull(document, nameof(document));
    //        ArgumentGuardHelper.ArgumentNotNull(position, nameof(position));
    //        ArgumentGuardHelper.ArgumentNotNull(fieldCode, nameof(fieldCode));

    //        // Build the final field code string
    //        string code = fieldCode.Build();

    //        // Begin editing the document that owns the position.
    //        // This resolves ambiguity when the position might be in the body, header, or footer.
    //        SubDocument subDocument = position.BeginUpdateDocument();

    //        try
    //        {
    //            // Verify the position belongs to this subdocument (defensive check)
    //            // toInt() gives the integer index; ensure it's within valid bounds.
    //            int posIndex = position.ToInt();
    //            if (posIndex < 0 || posIndex > subDocument.Range.End.ToInt())
    //            {
    //                throw new InvalidOperationException($"Position index {posIndex} is outside the valid range of the subdocument.");
    //            }

    //            // Create the field using the resolved subdocument's FieldCollection
    //            Field field = subDocument.Fields.Create(position, code);

    //            return field;
    //        }
    //        finally
    //        {
    //            // Always end the update to release the document lock.
    //            position.EndUpdateDocument(subDocument);
    //        }
    //    }

    //    #endregion

    //    #region Field Scanning and Detection

    //    /// <summary>
    //    /// Finds all merge fields in the document.
    //    /// Uses LINQ filter on full document scan for consistent field detection.
    //    /// </summary>
    //    /// <returns>Collection of merge field scan results</returns>
    //    public IRichFieldFindResultCollection FindMergeFields(Document document)
    //    {
    //        return FindDocumentFields(document)
    //               .GetFiltredResult(f => f.FieldCode.FieldType == RichFieldType.MergeField && !f.FieldCode.IsRegion);
    //    }

    //    /// <summary>
    //    /// Finds all collection merge fields (fields with dotted paths indicating collection membership).
    //    /// </summary>
    //    /// <returns>Collection of collection merge field scan results</returns>
    //    public IRichFieldFindResultCollection FindDataTableMergeFields(Document document)
    //    {
    //        return FindMergeFields(document)
    //               .GetFiltredResult(f => f.FieldCode.IsDataTableField);
    //    }

    //    /// <summary>
    //    /// Finds all TableStart fields in the document.
    //    /// </summary>
    //    public IRichFieldFindResultCollection FindTableStartFields(Document document)
    //    {
    //        return FindDocumentFields(document)
    //               .GetFiltredResult(f => f.FieldCode.IsTableStart);
    //    }

    //    /// <summary>
    //    /// Finds all TableEnd fields in the document.
    //    /// </summary>
    //    public IRichFieldFindResultCollection FindTableEndFields(Document document)
    //    {
    //        return FindDocumentFields(document)
    //               .GetFiltredResult(f => f.FieldCode.IsTableEnd);
    //    }

    //    /// <summary>
    //    /// Scans table start/end fields within a specific table row.
    //    /// </summary>
    //    public IRichFieldFindResultCollection FindRowStartEndFields(Document document, TableRow row)
    //    {
    //        return FindRowFields(document, row)
    //               .GetFiltredResult(f => f.FieldCode.IsRegion);
    //    }

    //    /// <summary>
    //    /// Scans the entire document and returns all fields as structured IRichFieldCode objects.
    //    /// This is the primary scanning method used by other detection methods.
    //    /// </summary>
    //    /// <returns>Collection of all field scan results in the document</returns>
    //    /// <remarks>
    //    /// Performance consideration: This method iterates all document fields.
    //    /// For performance-critical scenarios targeting specific ranges, use ranged scanning methods.
    //    /// </remarks>
    //    public IRichFieldFindResultCollection FindDocumentFields(Document document)
    //    {
    //        return FindDocumentFields(document, RichFieldSearchScope.All);
    //    }

    //    /// <summary>
    //    /// Scans the entire document and returns all fields as structured IRichFieldCode objects.
    //    /// This is the primary scanning method used by other detection methods.
    //    /// </summary>
    //    /// <returns>Collection of all field scan results in the document</returns>
    //    /// <remarks>
    //    /// Performance consideration: This method iterates all document fields.
    //    /// For performance-critical scenarios targeting specific ranges, use ranged scanning methods.
    //    /// </remarks>
    //    public IRichFieldFindResultCollection FindDocumentFields(Document document, RichFieldSearchScope scope)
    //    {
    //        if (document == null) throw new ArgumentNullException(nameof(document));

    //        var scanFieldResults = new RichFieldFindResultCollection();

    //        foreach (Field field in document.GetFields(scope))
    //        {
    //            var fieldCode = ScanFieldCode(document, field);
    //            if (fieldCode != null)
    //            {
    //                scanFieldResults.Add(new RichFieldFindResult(fieldCode, field));
    //            }
    //        }

    //        return scanFieldResults;
    //    }

    //    /// <summary>
    //    /// Scans the entire document and returns all fields as structured IRichFieldCode objects.
    //    /// This is the primary scanning method used by other detection methods.
    //    /// </summary>
    //    /// <returns>Collection of all field scan results in the document</returns>
    //    /// <remarks>
    //    /// Performance consideration: This method iterates all document fields.
    //    /// For performance-critical scenarios targeting specific ranges, use ranged scanning methods.
    //    /// </remarks>
    //    public IRichFieldFindResultCollection FindDocumentFields(Document document, Func<IRichFieldCode, bool> filter, RichFieldSearchScope scope = RichFieldSearchScope.All)
    //    {
    //        ArgumentGuardHelper.ArgumentNotNull(document, nameof(document));
    //        ArgumentGuardHelper.ArgumentNotNull(filter, nameof(filter));

    //        var scanFieldResults = new RichFieldFindResultCollection();

    //        foreach (Field field in document.GetFields(scope))
    //        {
    //            var fieldCode = ScanFieldCode(document, field);
    //            if (fieldCode != null && filter(fieldCode))
    //            {
    //                scanFieldResults.Add(new RichFieldFindResult(fieldCode, field));
    //            }
    //        }

    //        return scanFieldResults;
    //    }

    //    /// <summary>
    //    /// Scans fields within a specific table range.
    //    /// Uses range boundary checks for efficient field filtering.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="table">Table to scan within</param>
    //    /// <returns>Collection of field scan results within the table</returns>
    //    public IRichFieldFindResultCollection FindTableFields(Document document, Table table)
    //    {
    //        if (document == null) throw new ArgumentNullException(nameof(document));
    //        if (table == null) throw new ArgumentNullException(nameof(table));

    //        var scanFieldResults = new RichFieldFindResultCollection();
    //        var tableStart = table.Range.Start.ToInt();
    //        var tableEnd = table.Range.End.ToInt();

    //        foreach (Field field in document.Fields)
    //        {
    //            // Efficient range check: only process fields within table boundaries
    //            if (field.Range.Start.ToInt() >= tableStart && field.Range.End.ToInt() <= tableEnd)
    //            {
    //                var fieldCode = ScanFieldCode(document, field);
    //                if (fieldCode != null)
    //                {
    //                    scanFieldResults.Add(new RichFieldFindResult(fieldCode, field));
    //                }
    //            }
    //        }

    //        return scanFieldResults;
    //    }

    //    /// <summary>
    //    /// Scans fields within a specific table row.
    //    /// Iterates through row cells to capture all fields in the row.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="row">Table row to scan within</param>
    //    /// <returns>Collection of field scan results within the row</returns>
    //    public IRichFieldFindResultCollection FindRowFields(Document document, TableRow row)
    //    {
    //        if (document == null) throw new ArgumentNullException(nameof(document));
    //        if (row == null) throw new ArgumentNullException(nameof(row));

    //        var scanFieldResults = new RichFieldFindResultCollection();

    //        foreach (TableCell cell in row.Cells)
    //        {
    //            var cellFields = FindCellFields(document, cell);
    //            foreach (var field in cellFields)
    //            {
    //                scanFieldResults.Add(field);
    //            }
    //        }

    //        return scanFieldResults;
    //    }

    //    /// <summary>
    //    /// Scans fields within a specific table cell.
    //    /// Uses document.Fields.Get() for efficient cell-range field retrieval.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="cell">Table cell to scan within</param>
    //    /// <returns>Collection of field scan results within the cell</returns>
    //    public IRichFieldFindResultCollection FindCellFields(Document document, TableCell cell)
    //    {
    //        if (document == null) throw new ArgumentNullException(nameof(document));
    //        if (cell == null) throw new ArgumentNullException(nameof(cell));

    //        return FindFieldsAtRange(document, cell.Range);
    //    }

    //    /// <summary>
    //    /// Scans fields within a specific range.
    //    /// Uses document.Fields.Get() for efficient cell-range field retrieval.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="range">rang to scan within</param>
    //    /// <returns>Collection of field scan results within the cell</returns>
    //    public IRichFieldFindResultCollection FindFieldsAtRange(Document document, DocumentRange range)
    //    {
    //        if (document == null) throw new ArgumentNullException(nameof(document));
    //        if (range == null) throw new ArgumentNullException(nameof(range));

    //        var scanFieldResults = new RichFieldFindResultCollection();

    //        // Use efficient range-based field retrieval
    //        var fields = document.Fields.Get(range);

    //        foreach (Field field in fields)
    //        {
    //            var fieldCode = ScanFieldCode(document, field);
    //            if (fieldCode != null)
    //            {
    //                scanFieldResults.Add(new RichFieldFindResult(fieldCode, field));
    //            }
    //        }

    //        return scanFieldResults;
    //    }

    //    public bool IsDocumentContainsField(Document document, Func<IRichFieldCode, bool> filter, RichFieldSearchScope scope = RichFieldSearchScope.All)
    //    {
    //        ArgumentGuardHelper.ArgumentNotNull(document, nameof(document));
    //        ArgumentGuardHelper.ArgumentNotNull(filter, nameof(filter));

    //        foreach (Field field in document.GetFields(scope))
    //        {
    //            var fieldCode = ScanFieldCode(document, field);
    //            if (fieldCode != null && filter(fieldCode))
    //            {
    //                return true;
    //            }
    //        }

    //        return false;
    //    }

    //    /// <summary>
    //    /// Parses a field code string into a structured field code object.
    //    /// Returns null for invalid or unknown field types.
    //    /// </summary>
    //    /// <param name="rawCode">Raw field code string</param>
    //    /// <returns>Parsed field code or null if invalid/unknown</returns>
    //    public IRichFieldCode GetFieldCode(Document document, Field field)
    //    {
    //        return ScanFieldCode(document, field);
    //    }

    //    /// <summary>
    //    /// Parses a field code string into a structured field code object.
    //    /// Returns null for invalid or unknown field types.
    //    /// </summary>
    //    /// <param name="rawCode">Raw field code string</param>
    //    /// <returns>Parsed field code or null if invalid/unknown</returns>
    //    public IRichFieldCode GetFieldCode(string rawCode)
    //    {
    //        return ScanFieldCode(rawCode);
    //    }

    //    /// <summary>
    //    /// Checks if a table contains collection fields from a different data table.
    //    /// Useful for detecting potential collection conflicts.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="table">Table to check</param>
    //    /// <param name="tableName">Source table name to compare against</param>
    //    /// <returns>True if any other data table exists in the table</returns>
    //    public bool TableHasDifferentDataTables(Document document, Table table, string tableName)
    //    {
    //        if (document == null || table == null || string.IsNullOrEmpty(tableName))
    //            return false;

    //        var scanResults = FindTableFields(document, table);

    //        return scanResults.Any(result =>
    //            result.FieldCode != null &&
    //            result.FieldCode.IsDataTableField &&
    //            !result.FieldCode.GetDataTableName().EqualsTo(tableName));
    //    }

    //    /// <summary>
    //    /// Checks if a row contains collection fields from a different data table.
    //    /// Useful for detecting potential collection conflicts.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="row">row to check</param>
    //    /// <param name="tableName">Source table name to compare against</param>
    //    /// <returns>True if any other data table exists in the table</returns>
    //    public bool RowHasDifferentDataTables(Document document, TableRow row, string tableName)
    //    {
    //        if (document == null || row == null || string.IsNullOrEmpty(tableName))
    //            return false;

    //        var scanResults = FindRowFields(document, row);

    //        return scanResults.Any(result =>
    //            result.FieldCode != null &&
    //            result.FieldCode.IsDataTableField &&
    //            !result.FieldCode.GetDataTableName().EqualsTo(tableName));
    //    }

    //    /// <summary>
    //    /// Detects if a row contains any collection fields.
    //    /// </summary>
    //    public bool RowHasDataTableFields(Document document, TableRow row)
    //    {
    //        var fields = FindRowFields(document, row);
    //        return fields.Any(f => f.FieldCode.IsDataTableField);
    //    }

    //    /// <summary>
    //    /// Extracts the collection path from a row's collection fields.
    //    /// Returns the first found collection path or null if none exists.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="row">Table row to extract from</param>
    //    /// <returns>Collection path string or null</returns>
    //    public string ExtractDataTableName(Document document, TableRow row)
    //    {
    //        if (document == null || row == null) return null;

    //        var scanResults = FindRowFields(document, row);
    //        var result = scanResults.FirstOrDefault(f => f.FieldCode.IsDataTableField);

    //        if(result == null) return null;

    //        return result.FieldCode.GetDataTableName();
    //    }

    //    /// <summary>
    //    /// Checks if a row already has collection tags (TableStart/TableEnd) for a specific collection path.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="row">Table row to check</param>
    //    /// <param name="collectionPath">Collection path to look for</param>
    //    /// <returns>True if collection tags already exist</returns>
    //    public bool RowHasTableRegions(Document document, TableRow row, string tableName)
    //    {
    //        if (document == null || row == null || string.IsNullOrEmpty(tableName))
    //            return false;

    //        using var regionScanner = document.CreateRegionScanner();
    //        return regionScanner.IsRowHasRegion(row, tableName);
    //    }

    //    #endregion

    //    #region Field Update and Modification

    //    /// <summary>
    //    /// Updates a specific argument in a field by index.
    //    /// Preserves other arguments and field structure.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="field">Field to modify</param>
    //    /// <param name="index">Zero-based argument index</param>
    //    /// <param name="newArg">New argument value</param>
    //    public void UpdateFieldArgument(Document document, Field field, int index, string newArg)
    //    {
    //        if (document == null || field == null) return;

    //        var fieldCode = ScanFieldCode(document, field);

    //        if (fieldCode == null) return;

    //        fieldCode.Arguments.AddOrUpdateValue(index, newArg);

    //        string newCode = fieldCode.Build();
    //        document.ReplaceFieldCode(field, newCode);
    //    }

    //    /// <summary>
    //    /// Updates a field's property path and optional format in the document.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="field">Field to modify</param>
    //    /// <param name="newPath">New property path</param>
    //    /// <param name="newFormat">Optional new format string</param>
    //    public void UpdateMergeFieldPath(Document document, Field field, string newPath)
    //    {
    //        if (document == null || field == null) return;

    //        var fieldCode = ScanFieldCode(document, field);

    //        if (fieldCode == null) return;

    //        fieldCode.Arguments.AddOrUpdateValue(0, newPath);

    //        string newCode = fieldCode.Build();
    //        document.ReplaceFieldCode(field, newCode);
    //    }

    //    /// <summary>
    //    /// Updates only the format of a field, preserving the field path and arguments.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="field">Field to modify</param>
    //    /// <param name="format">New format string</param>
    //    public void UpdateFieldFormat(Document document, Field field, RichFieldFormat format)
    //    {
    //        if (document == null || field == null || RichFieldFormat.IsNullOrEmpty(format))
    //            return;

    //        var fieldCode = ScanFieldCode(document, field);

    //        if (fieldCode == null) return;

    //        TryAddFieldFormatSwitch(fieldCode, format);

    //        string newCode = fieldCode.Build();
    //        document.ReplaceFieldCode(field, newCode);
    //    }

    //    /// <summary>
    //    /// Removes all format switches from a field, keeping only the field path and arguments.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="field">Field to clean</param>
    //    public void RemoveFieldFormat(Document document, Field field)
    //    {
    //        if (document == null || field == null) return;

    //        var fieldCode = ScanFieldCode(document,field);

    //        if (fieldCode == null) return;

    //        var cleanedField = RemoveAllFormats(fieldCode);

    //        string newCode = cleanedField.Build();
    //        document.ReplaceFieldCode(field, newCode);
    //    }

    //    /// <summary>
    //    /// Adds a format switch of the specified type to a field.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="field">Field to modify</param>
    //    /// <param name="formatValue">Format string to apply</param>
    //    /// <param name="formatType">Type of format switch to create</param>
    //    public void UpdateFieldFormat(Document document, Field field, RichFieldFormatType formatType, string formatValue)
    //    {
    //        if (document == null || field == null || string.IsNullOrWhiteSpace(formatValue) || formatType == RichFieldFormatType.None)
    //            return;

    //        var fieldCode = ScanFieldCode(document,field);

    //        if (fieldCode == null) return;

    //        var fieldSwitch = _switchBuilder.CreateFormatSwitch(formatType, formatValue);
    //        if (fieldSwitch != null)
    //        {
    //            fieldCode.Switches.AddOrUpdate(fieldSwitch);
    //        }

    //        string newCode = fieldCode.Build();
    //        document.ReplaceFieldCode(field, newCode);
    //    }

    //    #endregion

    //    #region Field Removal

    //    /// <summary>
    //    /// Removes the currently selected field in the document.
    //    /// </summary>
    //    public void RemoveSelectedField(Document document)
    //    {
    //        if (document == null) return;

    //        Field selectedField = GetSelectedField(document);

    //        if (selectedField != null)
    //        {
    //            RemoveField(document, selectedField);
    //        }
    //    }

    //    /// <summary>
    //    /// Removes a specific field from the document.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="field">Field to remove</param>
    //    public void RemoveField(Document document, Field field)
    //    {
    //        if (document == null || field == null) return;

    //        document.Delete(field.Range);
    //    }

    //    /// <summary>
    //    /// Removes all fields of a specific type from the document.
    //    /// Uses deferred execution with ToList() to avoid collection modification during enumeration.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="fieldType">Type of fields to remove</param>
    //    public void RemoveFieldsByType(Document document, RichFieldType fieldType)
    //    {
    //        if (document == null) return;

    //        // Collect fields to remove first to avoid modifying collection during iteration
    //        var fieldsToRemove = document.Fields
    //            .Where(f =>
    //            {
    //                var fieldCode = ScanFieldCode(document,f);
    //                return fieldCode?.FieldType == fieldType;
    //            })
    //            .ToList(); // Materialize before deletion

    //        foreach (var field in fieldsToRemove)
    //        {
    //            document.Delete(field.Range);
    //        }
    //    }

    //    /// <summary>
    //    /// Removes all merge fields from the document.
    //    /// </summary>
    //    public void RemoveAllMergeFields(Document document)
    //    {
    //        RemoveFieldsByType(document, RichFieldType.MergeField);
    //    }

    //    #endregion

    //    #region Field Formatting Helpers

    //    /// <summary>
    //    /// Adds a numeric format switch (e.g., \# "#,##0.00") to a field code.
    //    /// </summary>
    //    protected void AddNumericFormatSwitch(IRichFieldCode fieldCode, string formatValue)
    //    {
    //        if (fieldCode == null || string.IsNullOrWhiteSpace(formatValue)) return;
    //        fieldCode.Switches.AddOrUpdate(_switchBuilder.CreateNumericFormatSwitch(formatValue));
    //    }

    //    /// <summary>
    //    /// Adds a date/time format switch (e.g., \@ "MMMM d, yyyy") to a field code.
    //    /// </summary>
    //    protected void AddDateTimeFormatSwitch(IRichFieldCode fieldCode, string formatValue)
    //    {
    //        if (fieldCode == null || string.IsNullOrWhiteSpace(formatValue)) return;
    //        fieldCode.Switches.AddOrUpdate(_switchBuilder.CreateDateTimeFormatSwitch(formatValue));
    //    }

    //    /// <summary>
    //    /// Adds a general format switch (e.g., \* Upper) to a field code.
    //    /// </summary>
    //    protected void AddGeneralFormatSwitch(IRichFieldCode fieldCode, string formatValue)
    //    {
    //        if (fieldCode == null || string.IsNullOrWhiteSpace(formatValue)) return;
    //        fieldCode.Switches.AddOrUpdate(_switchBuilder.CreateGeneralFormatSwitch(formatValue));
    //    }

    //    /// <summary>
    //    /// Adds an appropriate format switch based on the format string.
    //    /// Uses switch builder to auto-detect and create the correct switch type.
    //    /// </summary>
    //    /// <param name="fieldCode">Field code to modify</param>
    //    /// <param name="format">Format string (will be parsed to determine switch type)</param>
    //    protected void TryAddFieldFormatSwitch(IRichFieldCode fieldCode, RichFieldFormat? format)
    //    {
    //        if (fieldCode == null || RichFieldFormat.IsNullOrEmpty(format))
    //            return;

    //        var fieldSwitch = _switchBuilder.CreateFormatSwitch(format.Value);
    //        if (fieldSwitch != null)
    //        {
    //            fieldCode.Switches.AddOrUpdate(fieldSwitch);
    //        }
    //    }

    //    /// <summary>
    //    /// Removes all switches of a specific format type from a field.
    //    /// </summary>
    //    /// <param name="field">Field to modify</param>
    //    /// <param name="switchType">Format switch type to remove</param>
    //    protected void RemoveExistingSwitch(IRichFieldCode field, RichSwitchType switchType)
    //    {
    //        if (field?.Switches == null)
    //            return;

    //        var existing = field.Switches.FirstOrDefault(s => s.SwitchType == switchType);
    //        if (existing != null)
    //        {
    //            field.Switches.Remove(existing);
    //        }
    //    }

    //    /// <summary>
    //    /// Extracts the format string from a field if any format switch is present.
    //    /// Returns the value of the first found format switch (general, numeric, or date/time).
    //    /// </summary>
    //    /// <param name="field">Field to extract format from</param>
    //    /// <returns>Format string or null if no format switch exists</returns>
    //    protected string GetFormat(IRichFieldCode field)
    //    {
    //        if (field?.Switches == null)
    //            return null;

    //        var formatSwitch = field.Switches.FirstOrDefault(s => s.IsFormatSwitch);

    //        return formatSwitch?.SwitchValue;
    //    }

    //    /// <summary>
    //    /// Creates a copy of the field with all format switches removed.
    //    /// The original field is not modified (immutable pattern).
    //    /// </summary>
    //    /// <param name="field">Original field code</param>
    //    /// <returns>New field code without format switches</returns>
    //    protected IRichFieldCode RemoveAllFormats(IRichFieldCode field)
    //    {
    //        if (field == null) return null;

    //        var newField = field.Copy();

    //        // Remove all format-type switches
    //        var formatSwitches = newField.Switches.Where(s => s.IsFormatSwitch).ToList(); // Materialize to avoid collection modification during iteration

    //        foreach (var sw in formatSwitches)
    //        {
    //            newField.Switches.Remove(sw);
    //        }

    //        return newField;
    //    }

    //    /// <summary>
    //    /// Gets the format string from a document field.
    //    /// </summary>
    //    public string GetFieldFormat(Document document, Field field)
    //    {
    //        if (document == null || field == null) return null;

    //        var parsed = ScanFieldCode(document, field);
    //        return GetFormat(parsed);
    //    }

    //    /// <summary>
    //    /// Checks if a document field has a specific format applied.
    //    /// Performs case-insensitive comparison.
    //    /// </summary>
    //    public bool HasFormat(Document document, Field field, string format)
    //    {
    //        if (string.IsNullOrEmpty(format)) return false;

    //        var currentFormat = GetFieldFormat(document, field);
    //        return string.Equals(currentFormat, format, StringComparison.OrdinalIgnoreCase);
    //    }

    //    #endregion

    //    #region Switches
    //    /// <summary>
    //    /// Updates only the format of a field, preserving the field path and arguments.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="field">Field to modify</param>
    //    /// <param name="format">New format string</param>
    //    public void UpdateFieldSwitch(Document document, Field field, string switchKey, string switchValue)
    //    {
    //        if (document == null || field == null || string.IsNullOrWhiteSpace(switchKey))
    //            return;

    //        var fieldCode = ScanFieldCode(document,field);

    //        if (fieldCode == null) return;

    //        fieldCode.Switches.AddOrUpdateSwitch(switchKey, switchValue);

    //        string newCode = fieldCode.Build();

    //        document.ReplaceFieldCode(field, newCode);
    //    }

    //    /// <summary>
    //    /// Updates a specific switch value in a field. If the switch doesn't exist, it can optionally be added.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="field">Field to modify</param>
    //    /// <param name="switchKey">Switch code to update (e.g., "\@", "\#", "\*")</param>
    //    /// <param name="newValue">New switch value</param>
    //    /// <param name="addIfMissing">Whether to add the switch if it doesn't exist</param>
    //    public void UpdateSwitchValue(Document document, Field field, string switchKey, string newValue, bool addIfMissing = true)
    //    {
    //        if (document == null || field == null || string.IsNullOrEmpty(switchKey))
    //            return;

    //        // Get current field code
    //        var fieldCode = ScanFieldCode(document,field);

    //        if (fieldCode == null) return;

    //        // Update switch value
    //        UpdateSwitchValue(fieldCode, switchKey, newValue, addIfMissing);

    //        string newCode = fieldCode.Build();
    //        document.ReplaceFieldCode(field, newCode);
    //    }

    //    /// <summary>
    //    /// Updates a specific switch value in a field code object.
    //    /// </summary>
    //    /// <param name="fieldCode">Field code to modify</param>
    //    /// <param name="switchKey">Switch code to update</param>
    //    /// <param name="newValue">New switch value</param>
    //    /// <param name="addIfMissing">Whether to add the switch if it doesn't exist</param>
    //    public void UpdateSwitchValue(IRichFieldCode fieldCode, string switchKey, string newValue, bool addIfMissing = true)
    //    {
    //        if (fieldCode == null || string.IsNullOrEmpty(switchKey))
    //            return;

    //        // Find existing switch
    //        var containsKey = fieldCode.Switches.ContainsKey(switchKey);

    //        if (containsKey)
    //        {
    //            // Update existing switch value
    //            //existingSwitch.SwitchValue = newValue;
    //            fieldCode.Switches.TryUpdateSwitch(switchKey, newValue);
    //        }
    //        else if (addIfMissing)
    //        {
    //            // Add new switch with value
    //            var newSwitch = _switchBuilder.CreateFieldSwitch(switchKey, newValue);
    //            if (newSwitch != null)
    //            {
    //                fieldCode.Switches.AddOrUpdate(newSwitch);
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// Updates a switch value and creates a new field code (immutable approach).
    //    /// Returns a new instance while preserving the original.
    //    /// </summary>
    //    /// <param name="fieldCode">Original field code</param>
    //    /// <param name="switchKey">Switch code to update</param>
    //    /// <param name="newValue">New switch value</param>
    //    /// <returns>New field code with updated switch</returns>
    //    public IRichFieldCode UpdateSwitchValueImmutable(IRichFieldCode fieldCode, string switchKey, string newValue)
    //    {
    //        if (fieldCode == null || string.IsNullOrEmpty(switchKey))
    //            return fieldCode;

    //        // Create a copy to avoid modifying the original
    //        var newField = fieldCode.Copy();
    //        UpdateSwitchValue(newField, switchKey, newValue, true);
    //        return newField;
    //    }

    //    /// <summary>
    //    /// Updates multiple switch values in a single operation.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="field">Field to modify</param>
    //    /// <param name="switchUpdates">Dictionary of switch codes and their new values</param>
    //    public void UpdateSwitches(Document document, Field field, Dictionary<string, string> switchUpdates)
    //    {
    //        if (document == null || field == null || switchUpdates == null || switchUpdates.Count == 0)
    //            return;

    //        var fieldCode = ScanFieldCode(document,field);

    //        if (fieldCode == null) return;

    //        // Apply all updates
    //        foreach (var update in switchUpdates)
    //        {
    //            UpdateSwitchValue(fieldCode, update.Key, update.Value, true);
    //        }

    //        // Single update back to document
    //        string newCode = fieldCode.Build();
    //        document.ReplaceFieldCode(field, newCode);
    //    }

    //    /// <summary>
    //    /// Updates image dimension switches (\w and \h) in an INCLUDEPICTURE field.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="field">INCLUDEPICTURE field to update</param>
    //    /// <param name="width">New width (null to remove width switch)</param>
    //    /// <param name="height">New height (null to remove height switch)</param>
    //    public void UpdateImageDimensions(Document document, Field field, int? width, int? height)
    //    {
    //        if (document == null || field == null) return;

    //        var fieldCode = ScanFieldCode(document,field);

    //        if (fieldCode == null) return;

    //        // Update width
    //        if (width.HasValue)
    //        {
    //            UpdateSwitchValue(fieldCode, RichFieldSwitchKeys.ImageBasedField.ImageWidth, width.Value.ToString(), true);
    //        }
    //        else
    //        {
    //            // Remove width switch
    //            var widthSwitch = fieldCode.Switches.GetItemByKey(RichFieldSwitchKeys.ImageBasedField.ImageWidth);
    //            if (widthSwitch != null)
    //            {
    //                fieldCode.Switches.Remove(widthSwitch);
    //            }
    //        }

    //        // Update height
    //        if (height.HasValue)
    //        {
    //            UpdateSwitchValue(fieldCode, RichFieldSwitchKeys.ImageBasedField.ImageHeight, height.Value.ToString(), true);
    //        }
    //        else
    //        {
    //            // Remove height switch
    //            var heightSwitch = fieldCode.Switches.GetItemByKey(RichFieldSwitchKeys.ImageBasedField.ImageHeight);
    //            if (heightSwitch != null)
    //            {
    //                fieldCode.Switches.Remove(heightSwitch);
    //            }
    //        }

    //        // Apply changes
    //        string newCode = fieldCode.Build();
    //        document.ReplaceFieldCode(field, newCode);
    //    }

    //    /// <summary>
    //    /// Updates a specific switch value across all fields of a given type.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="fieldType">Type of fields to update</param>
    //    /// <param name="switchKey">Switch code to update</param>
    //    /// <param name="newValue">New switch value</param>
    //    public void UpdateSwitchInAllFields(Document document, RichFieldType fieldType, string switchKey, string newValue)
    //    {
    //        if (document == null) return;

    //        // Find all fields of the specified type
    //        var scanResults = FindDocumentFields(document)
    //            .Where(f => f.FieldCode?.FieldType == fieldType)
    //            .ToList();

    //        foreach (var result in scanResults)
    //        {
    //            if (result.Field != null)
    //            {
    //                UpdateSwitchValue(document, result.Field, switchKey, newValue, true);
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// Updates format switches for all merge fields in a document.
    //    /// </summary>
    //    /// <param name="document">Target document</param>
    //    /// <param name="formatType">Type of format to update</param>
    //    /// <param name="newFormat">New format string</param>
    //    public void UpdateMergeFieldsFormat(Document document, RichFieldFormatType formatType, string newFormat)
    //    {
    //        if (document == null) return;

    //        var mergeFields = FindMergeFields(document);

    //        foreach (var mergeField in mergeFields)
    //        {
    //            if (mergeField.Field != null)
    //            {
    //                UpdateFieldFormat(document, mergeField.Field, formatType, newFormat);
    //            }
    //        }
    //    }
    //    #endregion

    //    #region Utility Methods

    //    /// <summary>
    //    /// Gets the field at the current selection position.
    //    /// Returns the first field that contains the selection start point.
    //    /// </summary>
    //    public Field GetSelectedField(Document document)
    //    {
    //        if (document?.Selection == null) return null;

    //        DocumentRange selection = document.Selection;
    //        return document.FindFieldContainsPosition(selection.Start);
    //    }

    //    /// <summary>
    //    /// Validates if a field code string represents a valid, recognized field type.
    //    /// </summary>
    //    /// <param name="rawCode">Field code string to validate</param>
    //    /// <returns>True if field code is valid and recognized</returns>
    //    public bool IsValidFieldCode(string rawCode)
    //    {
    //        if (string.IsNullOrWhiteSpace(rawCode)) return false;

    //        var code = GetFieldCode(rawCode);
    //        return code != null;
    //    }

    //    /// <summary>
    //    /// Extracts the field type from a field code string.
    //    /// </summary>
    //    /// <param name="rawCode">Field code string to analyze</param>
    //    /// <returns>Detected field type or Unknown if invalid</returns>
    //    public RichFieldType GetFieldType(string rawCode)
    //    {
    //        var code = GetFieldCode(rawCode);
    //        return code?.FieldType ?? RichFieldType.Unknown;
    //    }

    //    /// <summary>
    //    /// Gets all unique merge field paths in the document.
    //    /// </summary>
    //    /// <returns>Distinct list of merge field paths</returns>
    //    public List<string> GetAllMergeDataFieldPaths(Document document)
    //    {
    //        return FindMergeFields(document)
    //                .Select(f => f.FieldCode?.FieldProperty)
    //                .Where(p => !string.IsNullOrEmpty(p))
    //                .Distinct()
    //                .ToList();
    //    }

    //    /// <summary>
    //    /// Gets all unique collection paths referenced in the document.
    //    /// </summary>
    //    /// <returns>Distinct list of collection paths</returns>
    //    public List<string> GetAllDataTablesName(Document document)
    //    {
    //        return FindDataTableMergeFields(document)
    //                .Select(f => f.FieldCode?.GetDataTableName())
    //                .Where(p => !string.IsNullOrEmpty(p))
    //                .Distinct()
    //                .ToList();
    //    }

    //    /// <summary>
    //    /// Updates all fields in the document to refresh their display values.
    //    /// </summary>
    //    public void UpdateAllFields(Document document)
    //    {
    //        if (document == null) throw new ArgumentNullException(nameof(document));
    //        document.Fields.Update();
    //    }

    //    #endregion

    //    #region Private Helper Methods

    //    /// <summary>
    //    /// Helper method to scan a single field and extract its field code.
    //    /// Centralizes the scanning logic used by multiple scanning methods.
    //    /// </summary>
    //    /// <param name="document">Document containing the field</param>
    //    /// <param name="field">Field to scan</param>
    //    /// <returns>Parsed field code or null if scanning fails</returns>
    //    private IRichFieldCode ScanFieldCode(Document document, Field field)
    //    {
    //        if (document == null || field == null) return null;

    //        var subDocument = field.CodeRange.BeginUpdateDocument();
    //        try
    //        {
    //            string code = subDocument.GetText(field.CodeRange);
    //            return ScanFieldCode(code);
    //        }
    //        finally
    //        {
    //            field.CodeRange.EndUpdateDocument(subDocument);
    //        }
    //    }

    //    /// <summary>
    //    /// Helper method to scan a single field and extract its field code.
    //    /// Centralizes the scanning logic used by multiple scanning methods.
    //    /// </summary>
    //    /// <param name="document">Document containing the field</param>
    //    /// <param name="field">Field to scan</param>
    //    /// <returns>Parsed field code or null if scanning fails</returns>
    //    private IRichFieldCode ScanFieldCode(Field field)
    //    {
    //        if (field == null) return null;

    //        return ScanFieldCode(field.CodeRange);
    //    }

    //    /// <summary>
    //    /// Helper method to scan a single field and extract its field code.
    //    /// Centralizes the scanning logic used by multiple scanning methods.
    //    /// </summary>
    //    /// <param name="document">Document containing the field</param>
    //    /// <param name="field">Field to scan</param>
    //    /// <returns>Parsed field code or null if scanning fails</returns>
    //    private IRichFieldCode ScanFieldCode(DocumentRange range)
    //    {
    //        if (range == null) return null;

    //        var subDocument = range.BeginUpdateDocument();
    //        try
    //        {
    //            string code = subDocument.GetText(range);
    //            return ScanFieldCode(code);
    //        }
    //        finally
    //        {
    //            range.EndUpdateDocument(subDocument);
    //        }
    //    }

    //    private IRichFieldCode ScanFieldCode(string rawCodeText)
    //    {
    //        if (string.IsNullOrWhiteSpace(rawCodeText))
    //            return null;

    //        var fieldCode = _scanner.Scan(rawCodeText);

    //        if (fieldCode == null || !fieldCode.IsValid()) return null;

    //        return fieldCode;
    //    }

    //    #endregion
    //}

    /// <summary>
    /// Comprehensive field management service for creating, scanning, updating,
    /// and formatting fields in RichEdit documents.
    /// </summary>
    /// <remarks>
    /// Thread-safety: Instance methods are not thread-safe. Create new instances per thread if needed.
    /// </remarks>
    public class RichFieldManagerService : IRichFieldManagerService
    {
        #region Dependencies
        private readonly IRichFieldCodeScanner _scanner;
        private readonly IRichFieldCodeBuilder _codeBuilder;
        private readonly IRichFieldSwitchBuilder _switchBuilder;
        #endregion

        #region Constructor
        public RichFieldManagerService(
            IRichFieldCodeScanner scanner = null,
            IRichFieldCodeBuilder builder = null,
            IRichFieldSwitchBuilder switchBuilder = null)
        {
            _scanner = scanner ?? new RichFieldCodeScanner();
            _switchBuilder = switchBuilder ?? new RichFieldSwitchBuilder();
            _codeBuilder = builder ?? new RichFieldCodeBuilder(new RichFieldInstructionsBuilder(_switchBuilder));
        }
        #endregion

        #region Field Creation

        public Field CreateMergeField(DocumentPosition position, string fieldPath, RichFieldFormat? format = null)
            => CreateField(position, _codeBuilder.CreateMergeField(fieldPath, format));

        public Field CreateMergeField(DocumentPosition position, RichMergeFieldOptions options)
            => CreateField(position, _codeBuilder.CreateMergeField(options));

        public Field CreatePictureField(DocumentPosition position, RichIncludePictureFieldOptions options)
            => CreateField(position, _codeBuilder.CreatePictureField(options));

        public Field CreatePictureField(DocumentPosition position, string uri)
            => CreateField(position, _codeBuilder.CreatePictureField(uri));

        public Field CreatePictureFieldFromImageUri(DocumentPosition position, ImageUri uri)
            => CreatePictureField(position, uri);

        public Field CreateNestedField(DocumentPosition position, RichNestedFieldOptions options)
        {
            ArgumentGuardHelper.ArgumentNotNull(position, nameof(position));
            ArgumentGuardHelper.ArgumentNotNull(options, nameof(options));
            ArgumentGuardHelper.ArgumentNotNull(options.OuterFieldOptions, nameof(options.OuterFieldOptions));
            ArgumentGuardHelper.ArgumentNotNull(options.InnerFieldOptions, nameof(options.InnerFieldOptions));

            // Resolve the owning subdocument ONCE so every piece (outer field, space,
            // inner field, switches) lands in the same piece table.
            SubDocument subDoc = position.BeginUpdateDocument();
            try
            {
                // 1. Outer shell field.
                Field outerField = CreateFieldCore(subDoc, position, options.OuterFieldOptions.FieldType);

                // 2. Space placeholder establishes the inner field boundary inside the outer code.
                var spaceRange = subDoc.InsertText(
                    subDoc.CreatePosition(outerField.CodeRange.End.ToInt()), " ");

                // 3. Inner field, inserted after the placeholder.
                CreateFieldWithOptions(spaceRange.End, options.InnerFieldOptions);

                // 4. Outer switches appended after the inner field.
                var switchesStr = _codeBuilder.GetFieldSwitchesBuildString(options.OuterFieldOptions);
                if (!string.IsNullOrEmpty(switchesStr))
                {
                    subDoc.InsertText(
                        subDoc.CreatePosition(outerField.CodeRange.End.ToInt()), switchesStr);
                }

                return outerField;
            }
            finally
            {
                position.EndUpdateDocument(subDoc);
            }
        }

        public Field CreateMergePictureField(DocumentPosition position,
            RichMergeFieldOptions mergeFieldOptions, RichIncludePictureFieldOptions pictureFieldOptions)
            => CreateNestedField(position,
                RichNestedFieldOptions.CreateIncludePictureWithInnerOptions(pictureFieldOptions, mergeFieldOptions));

        public Field CreateMergePictureField(DocumentPosition position, string mergeFieldPath)
            => CreateMergePictureField(
                position,
                RichMergeFieldOptions.Create(mergeFieldPath),
                RichIncludePictureFieldOptions.CreateDefault());

        public Field CreateTableStartField(DocumentPosition position, string tableName)
            => CreateField(position, _codeBuilder.CreateTableStart(tableName));

        public Field CreateTableEndField(DocumentPosition position, string tableName)
            => CreateField(position, _codeBuilder.CreateTableEnd(tableName));

        public Field CreateDateField(DocumentPosition position, RichDateBasedFieldOptions options)
            => CreateField(position, _codeBuilder.CreateDateField(options));

        public Field CreateDateField(DocumentPosition position, RichFieldFormat? format = null)
            => CreateField(position, _codeBuilder.CreateDateField(format));

        public Field CreateTimeField(DocumentPosition position, RichFieldFormat? format = null)
            => CreateField(position, _codeBuilder.CreateTimeField(format));

        public Field CreatePageField(DocumentPosition position)
            => CreateField(position, _codeBuilder.CreatePageField());

        public Field CreateNumPagesField(DocumentPosition position)
            => CreateField(position, _codeBuilder.CreateNumPagesField());

        public Field CreateIfField(DocumentPosition position,
            string expression1, string cmOperator, string expression2)
            => CreateField(position, _codeBuilder.CreateIfField(expression1, cmOperator, expression2));

        public Field CreateIfField(DocumentPosition position, RichIfFieldOptions options)
            => CreateField(position, _codeBuilder.CreateIfField(options));

        public Field CreateFieldInCell(TableCell cell, IRichFieldCode fieldCode, bool atStart)
        {
            ArgumentGuardHelper.ArgumentNotNull(cell, nameof(cell));

            var position = atStart ? cell.ContentRange.Start : cell.ContentRange.End;
            return CreateField(position, fieldCode);
        }

        public Field CreateFieldWithCommonOptions(DocumentPosition position,
            RichFieldType fieldType, string firstArgument, RichFieldFormat? format = null)
            => CreateField(position, _codeBuilder.CreateFieldWithCommonOptions(fieldType, firstArgument, format));

        public Field CreateFieldWithOptions(DocumentPosition position, object options)
            => options is RichNestedFieldOptions nested
                ? CreateNestedField(position, nested)
                : CreateField(position, _codeBuilder.CreateFieldWithOptions(options));

        protected Field CreateField(DocumentPosition position, RichFieldType fieldType)
            => CreateField(position, _codeBuilder.CreateField(fieldType));

        /// <summary>
        /// Core field insertion. Resolves the owning subdocument (body/header/footer/textbox)
        /// from the position, then creates the field in that context.
        /// </summary>
        protected Field CreateField(DocumentPosition position, IRichFieldCode fieldCode)
        {
            ArgumentGuardHelper.ArgumentNotNull(position, nameof(position));
            ArgumentGuardHelper.ArgumentNotNull(fieldCode, nameof(fieldCode));

            // Resolve the subdocument that actually owns this position.
            SubDocument subDoc = position.BeginUpdateDocument();
            try
            {
                return CreateFieldCore(subDoc, position, fieldCode);
            }
            finally
            {
                position.EndUpdateDocument(subDoc);
            }
        }

        private static Field CreateFieldCore(SubDocument subDoc, DocumentPosition position, IRichFieldCode fieldCode)
        {
            ArgumentGuardHelper.ArgumentNotNull(subDoc, nameof(subDoc));
            ArgumentGuardHelper.ArgumentNotNull(position, nameof(position));
            ArgumentGuardHelper.ArgumentNotNull(fieldCode, nameof(fieldCode));

            int posIndex = position.ToInt();
            if (posIndex < 0 || posIndex > subDoc.Range.End.ToInt())
                throw new InvalidOperationException(
                    $"Position index {posIndex} is outside the valid range of the subdocument.");

            return subDoc.Fields.Create(position, fieldCode.Build());
        }

        private Field CreateFieldCore(SubDocument subDoc, DocumentPosition position, RichFieldType fieldType)
            => CreateFieldCore(subDoc, position, _codeBuilder.CreateField(fieldType));

        #endregion

        #region Field Scanning and Detection

        public IRichFieldFindResultCollection FindMergeFields(Document document)
            => FindDocumentFields(document,
                code => code.FieldType == RichFieldType.MergeField && !code.IsRegion);

        public IRichFieldFindResultCollection FindDataTableMergeFields(Document document)
            => FindMergeFields(document).GetFiltredResult(f => f.FieldCode.IsDataTableField);

        public IRichFieldFindResultCollection FindTableStartFields(Document document)
            => FindDocumentFields(document, code => code.IsTableStart);

        public IRichFieldFindResultCollection FindTableEndFields(Document document)
            => FindDocumentFields(document, code => code.IsTableEnd);

        public IRichFieldFindResultCollection FindRowStartEndFields(TableRow row)
            => FindRowFields(row).GetFiltredResult(f => f.FieldCode.IsRegion);

        public IRichFieldFindResultCollection FindAllDocumentFields(Document document)
            => FindDocumentFields(document, RichFieldSearchScope.All);

        public IRichFieldFindResultCollection FindDocumentFields(Document document, RichFieldSearchScope scope)
            => FindDocumentFields(document, filter: null, scope);

        public IRichFieldFindResultCollection FindDocumentFields(
            Document document,
            Func<IRichFieldCode, bool> filter,
            RichFieldSearchScope scope = RichFieldSearchScope.All)
        {
            ArgumentGuardHelper.ArgumentNotNull(document, nameof(document));

            var results = new RichFieldFindResultCollection();

            foreach (var field in document.GetFields(scope))
            {
                var code = ScanFieldCode(field.Field.CodeRange);
                if (code == null) continue;
                if (filter != null && !filter(code)) continue;

                results.Add(new RichFieldFindResult(code, field.Field));
            }

            return results;
        }

        public bool IsDocumentContainsField(
            Document document,
            Func<IRichFieldCode, bool> filter,
            RichFieldSearchScope scope = RichFieldSearchScope.All)
        {
            ArgumentGuardHelper.ArgumentNotNull(document, nameof(document));
            ArgumentGuardHelper.ArgumentNotNull(filter, nameof(filter));

            foreach (var field in document.GetFields(scope))
            {
                var code = ScanFieldCode(field.Field.CodeRange);
                if (code != null && filter(code)) return true;
            }
            return false;
        }

        /// <summary>Scans fields whose range falls inside the given table.</summary>
        public IRichFieldFindResultCollection FindTableFields(Table table)
        {
            ArgumentGuardHelper.ArgumentNotNull(table, nameof(table));

            return FindFieldsAtRange(table.Range);
        }

        public IRichFieldFindResultCollection FindRowFields(TableRow row)
        {
            ArgumentGuardHelper.ArgumentNotNull(row, nameof(row));

            var results = new RichFieldFindResultCollection();

            foreach (TableCell cell in row.Cells)
            {
                foreach (var result in FindCellFields(cell))
                    results.Add(result);
            }

            return results;
        }

        public IRichFieldFindResultCollection FindCellFields(TableCell cell)
        {
            ArgumentGuardHelper.ArgumentNotNull(cell, nameof(cell));

            return FindFieldsAtRange(cell.Range);
        }

        public IRichFieldFindResultCollection FindFieldsAtRange(DocumentRange range)
        {
            ArgumentGuardHelper.ArgumentNotNull(range, nameof(range));

            var results = new RichFieldFindResultCollection();

            // Resolve the owning subdocument to safely enumerate fields in the range.
            SubDocument subDoc = range.BeginUpdateDocument();
            try
            {
                foreach (Field field in subDoc.Fields.Get(range))
                {
                    var code = ScanFieldCode(field.CodeRange);
                    if (code != null)
                        results.Add(new RichFieldFindResult(code, field));
                }
            }
            finally
            {
                range.EndUpdateDocument(subDoc);
            }

            return results;
        }

        public IRichFieldCode GetFieldCode(Field field)
        {
            return ScanFieldCode(field?.CodeRange);
        }

        public IRichFieldCode GetFieldCode(string rawCode) => ScanFieldCode(rawCode);

        public bool TableHasDifferentDataTables(Table table, string tableName)
        {
            if (table == null || string.IsNullOrEmpty(tableName)) return false;

            return FindTableFields(table).Any(r =>
                r.FieldCode != null
                && r.FieldCode.IsDataTableField
                && !r.FieldCode.GetDataTableName().EqualsTo(tableName));
        }

        public bool RowHasDifferentDataTables(TableRow row, string tableName)
        {
            if (row == null || string.IsNullOrEmpty(tableName)) return false;

            return FindRowFields(row).Any(r =>
                r.FieldCode != null
                && r.FieldCode.IsDataTableField
                && !r.FieldCode.GetDataTableName().EqualsTo(tableName));
        }

        public bool RowHasDataTableFields(TableRow row)
            => FindRowFields(row).Any(f => f.FieldCode.IsDataTableField);

        public string ExtractDataTableName(TableRow row)
        {
            if (row == null) return null;

            return FindRowFields(row)
                .FirstOrDefault(f => f.FieldCode.IsDataTableField)
                ?.FieldCode.GetDataTableName();
        }

        public bool RowHasTableRegions(Document document, TableRow row, string tableName)
        {
            if (document == null || row == null || string.IsNullOrEmpty(tableName)) return false;

            using var regionScanner = document.CreateRegionScanner();
            return regionScanner.IsRowHasRegion(row, tableName);
        }

        #endregion

        #region Field Update and Modification

        public void UpdateFieldArgument(Document document, Field field, int index, string newArg)
            => UpdateFieldCode(document, field, code => code.Arguments.AddOrUpdateValue(index, newArg));

        public void UpdateMergeFieldPath(Document document, Field field, string newPath)
            => UpdateFieldArgument(document, field, 0, newPath);

        public void UpdateFieldFormat(Document document, Field field, RichFieldFormat format)
        {
            if (RichFieldFormat.IsNullOrEmpty(format)) return;
            UpdateFieldCode(document, field, code => TryAddFieldFormatSwitch(code, format));
        }

        public void UpdateFieldFormat(Document document, Field field, RichFieldFormatType formatType, string formatValue)
        {
            if (string.IsNullOrWhiteSpace(formatValue) || formatType == RichFieldFormatType.None) return;

            UpdateFieldCode(document, field, code =>
            {
                var sw = _switchBuilder.CreateFormatSwitch(formatType, formatValue);
                if (sw != null) code.Switches.AddOrUpdate(sw);
            });
        }

        public void RemoveFieldFormat(Document document, Field field)
            => UpdateFieldCode(document, field, code =>
            {
                var toRemove = code.Switches.Where(s => s.IsFormatSwitch).ToList();
                foreach (var sw in toRemove) code.Switches.Remove(sw);
            });

        #endregion

        #region Field Removal

        public void RemoveSelectedField(Document document)
        {
            if (document == null) return;

            var selected = GetSelectedField(document);
            if (selected != null) RemoveField(document, selected);
        }

        public void RemoveField(Document document, Field field)
        {
            if (document == null || field == null) return;
            document.Delete(field.Range);
        }

        public void RemoveFieldsByType(Document document, RichFieldType fieldType)
        {
            if (document == null) return;

            // Materialize before deletion to avoid collection modification during iteration.
            var toRemove = FindDocumentFields(document, code => code.FieldType == fieldType)
                .Select(r => r.Field)
                .Where(f => f != null)
                .ToList();

            foreach (var field in toRemove)
                document.Delete(field.Range);
        }

        public void RemoveAllMergeFields(Document document)
            => RemoveFieldsByType(document, RichFieldType.MergeField);

        #endregion

        #region Formatting Helpers

        protected void AddNumericFormatSwitch(IRichFieldCode code, string value)
        {
            if (code == null || string.IsNullOrWhiteSpace(value)) return;
            code.Switches.AddOrUpdate(_switchBuilder.CreateNumericFormatSwitch(value));
        }

        protected void AddDateTimeFormatSwitch(IRichFieldCode code, string value)
        {
            if (code == null || string.IsNullOrWhiteSpace(value)) return;
            code.Switches.AddOrUpdate(_switchBuilder.CreateDateTimeFormatSwitch(value));
        }

        protected void AddGeneralFormatSwitch(IRichFieldCode code, string value)
        {
            if (code == null || string.IsNullOrWhiteSpace(value)) return;
            code.Switches.AddOrUpdate(_switchBuilder.CreateGeneralFormatSwitch(value));
        }

        protected void TryAddFieldFormatSwitch(IRichFieldCode code, RichFieldFormat? format)
        {
            if (code == null || RichFieldFormat.IsNullOrEmpty(format)) return;

            var sw = _switchBuilder.CreateFormatSwitch(format.Value);
            if (sw != null) code.Switches.AddOrUpdate(sw);
        }

        protected void RemoveExistingSwitch(IRichFieldCode code, RichSwitchType switchType)
        {
            if (code?.Switches == null) return;

            var existing = code.Switches.FirstOrDefault(s => s.SwitchType == switchType);
            if (existing != null) code.Switches.Remove(existing);
        }

        protected string GetFormat(IRichFieldCode code)
            => code?.Switches?.FirstOrDefault(s => s.IsFormatSwitch)?.SwitchValue;

        protected IRichFieldCode RemoveAllFormats(IRichFieldCode code)
        {
            if (code == null) return null;

            var copy = code.Copy();
            var formatSwitches = copy.Switches.Where(s => s.IsFormatSwitch).ToList();
            foreach (var sw in formatSwitches) copy.Switches.Remove(sw);
            return copy;
        }

        public string GetFieldFormat(Document document, Field field)
        {
            ArgumentGuardHelper.ArgumentNotNull(document, nameof(document));
            return GetFormat(ScanFieldCode(field?.CodeRange));
        }

        public bool HasFormat(Document document, Field field, string format)
            => !string.IsNullOrEmpty(format)
               && string.Equals(GetFieldFormat(document, field), format, StringComparison.OrdinalIgnoreCase);

        #endregion

        #region Switches

        public void UpdateFieldSwitch(Document document, Field field, string switchKey, string switchValue)
        {
            if (string.IsNullOrWhiteSpace(switchKey)) return;

            UpdateFieldCode(document, field, code => code.Switches.AddOrUpdateSwitch(switchKey, switchValue));
        }

        public void UpdateSwitchValue(Document document, Field field, string switchKey, string newValue, bool addIfMissing = true)
            => UpdateFieldCode(document, field, code => UpdateSwitchValue(code, switchKey, newValue, addIfMissing));

        public void UpdateSwitchValue(IRichFieldCode fieldCode, string switchKey, string newValue, bool addIfMissing = true)
        {
            if (fieldCode == null || string.IsNullOrEmpty(switchKey)) return;

            if (fieldCode.Switches.ContainsKey(switchKey))
            {
                fieldCode.Switches.TryUpdateSwitch(switchKey, newValue);
            }
            else if (addIfMissing)
            {
                var sw = _switchBuilder.CreateFieldSwitch(switchKey, newValue);
                if (sw != null) fieldCode.Switches.AddOrUpdate(sw);
            }
        }

        public IRichFieldCode UpdateSwitchValueImmutable(IRichFieldCode fieldCode, string switchKey, string newValue)
        {
            if (fieldCode == null || string.IsNullOrEmpty(switchKey)) return fieldCode;

            var copy = fieldCode.Copy();
            UpdateSwitchValue(copy, switchKey, newValue, addIfMissing: true);
            return copy;
        }

        public void UpdateSwitches(Document document, Field field, Dictionary<string, string> switchUpdates)
        {
            if (switchUpdates == null || switchUpdates.Count == 0) return;

            UpdateFieldCode(document, field, code =>
            {
                foreach (var kv in switchUpdates)
                    UpdateSwitchValue(code, kv.Key, kv.Value, addIfMissing: true);
            });
        }

        public void UpdateSwitchInAllFields(Document document, RichFieldType fieldType, string switchKey, string newValue)
        {
            if (document == null) return;

            var targets = FindDocumentFields(document, code => code.FieldType == fieldType)
                .Select(r => r.Field)
                .Where(f => f != null)
                .ToList();

            foreach (var field in targets)
                UpdateSwitchValue(document, field, switchKey, newValue, addIfMissing: true);
        }

        public void UpdateMergeFieldsFormat(Document document, RichFieldFormatType formatType, string newFormat)
        {
            if (document == null) return;

            foreach (var result in FindMergeFields(document))
            {
                if (result.Field != null)
                    UpdateFieldFormat(document, result.Field, formatType, newFormat);
            }
        }

        #endregion

        #region Utility Methods

        public Field GetSelectedField(Document document)
        {
            if (document == null) return null;
            return document.GetSelectedField();
        }

        public bool IsValidFieldCode(string rawCode)
            => !string.IsNullOrWhiteSpace(rawCode) && GetFieldCode(rawCode) != null;

        public RichFieldType GetFieldType(string rawCode)
            => GetFieldCode(rawCode)?.FieldType ?? RichFieldType.Unknown;

        public RichFieldType GetFieldType(Field field)
           => GetFieldCode(field)?.FieldType ?? RichFieldType.Unknown;

        public List<string> GetAllMergeDataFieldPaths(Document document)
            => FindMergeFields(document)
                .Select(f => f.FieldCode?.FieldProperty)
                .Where(p => !string.IsNullOrEmpty(p))
                .Distinct()
                .ToList();

        public List<string> GetAllDataTablesName(Document document)
            => FindDataTableMergeFields(document)
                .Select(f => f.FieldCode?.GetDataTableName())
                .Where(p => !string.IsNullOrEmpty(p))
                .Distinct()
                .ToList();

        public void UpdateAllFields(Document document)
        {
            ArgumentGuardHelper.ArgumentNotNull(document, nameof(document));
            document.Fields.Update();
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Single source of truth for scanning a field code from a range.
        /// Resolves the correct subdocument before reading text, preventing
        /// cross-document range errors.
        /// </summary>
        private IRichFieldCode ScanFieldCode(DocumentRange range)
        {
            if (range == null) return null;

            SubDocument subDoc = range.BeginUpdateDocument();
            try
            {
                string text = subDoc.GetText(range);
                return ScanFieldCode(text);
            }
            finally
            {
                range.EndUpdateDocument(subDoc);
            }
        }

        private IRichFieldCode ScanFieldCode(string rawCodeText)
        {
            if (string.IsNullOrWhiteSpace(rawCodeText)) return null;

            var code = _scanner.Scan(rawCodeText);
            return code != null && code.IsValid() ? code : null;
        }

        /// <summary>
        /// Common flow for any mutation of an existing field:
        ///   scan → mutate code → rebuild → replace in document.
        /// Wrapped in a single subdocument update so range operations stay consistent.
        /// </summary>
        private void UpdateFieldCode(Document document, Field field, Action<IRichFieldCode> mutate)
        {
            if (document == null || field == null || mutate == null) return;

            var code = ScanFieldCode(field.CodeRange);
            if (code == null) return;

            mutate(code);

            string newCode = code.Build();
            document.ReplaceFieldCode(field, newCode);
        }

        #endregion
    }
}
