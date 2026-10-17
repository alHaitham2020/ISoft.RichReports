using DevExpress.XtraRichEdit.Fields;
using ISoft.RichReportDesigner.Core.Fields.Argumentes;
using ISoft.RichReportDesigner.Core.Fields.Data;
using ISoft.RichReportDesigner.Core.Fields.Options;
using ISoft.RichReportDesigner.Core.Fields.Switches;
using ISoft.RichReportDesigner.Core.RichEdit.Fields;
using ISoft.Utils.Common;
using ISoft.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ISoft.RichReportDesigner.Core.Fields.Codes
{
    ///// <summary>
    ///// Parses field codes from document text into structured RichFieldCode objects
    ///// </summary>
    //public class RichFieldCodeScanner : IRichFieldCodeScanner
    //{
    //    private static readonly Regex FieldCodeRegex;

    //    private static readonly Regex SwitchRegex = new(
    //        @"(\\[@#\*a-zA-Z]+)(?:\s+([^\\]+))?",
    //        RegexOptions.Compiled);

    //    static RichFieldCodeScanner()
    //    {
    //        var pattern = FluentStringBuilder
    //                     .CreateNew()
    //                     .Append($"^({MergefieldField.FieldType}\\s+TableStart:|")
    //                     .Append($"{MergefieldField.FieldType}\\s+TableEnd:|")
    //                     .Append($"{MergefieldField.FieldType}|")
    //                     .Append($"{DateField.FieldType}|")
    //                     .Append($"{TimeField.FieldType}|")
    //                     .Append($"{PageField.FieldType}|")
    //                     .Append($"{NumPagesField.FieldType}|")
    //                     .Append($"{IfField.FieldType}|")
    //                     .Append($"{HyperlinkField.FieldType}|")
    //                     .Append($"{IncludepictureField.FieldType}|")
    //                     .Append($"{DocVariableField.FieldType}|")
    //                     .Append($"{DocumentPropertyField.FieldType}|")
    //                     .Append($"{RichCustomPropertyField.FieldType}|")
    //                     .Append($"{SymbolField.FieldType}|")
    //                     .Append($"{RefField.FieldType}|")
    //                     .Append($"{SequenceField.FieldType}|")
    //                     .Append($"{StyleRefField.FieldType}|")
    //                     .Append($"{TocField.FieldType}|")
    //                     .Append($"{AuthorField.FieldType}|")
    //                     .Append($"{CommentsField.FieldType}|")
    //                     .Append($"{CreatedateField.FieldType}|")
    //                     .Append($"{KeywordsField.FieldType}|")
    //                     .Append($"{LastSavedByField.FieldType}|")
    //                     .Append($"{PrintDateField.FieldType}|")
    //                     .Append($"{ReviewNumberField.FieldType}|")
    //                     .Append($"{SaveDateField.FieldType}|")
    //                     .Append($"{SectionPageField.FieldType}|")
    //                     .Append($"{SubjectField.FieldType}|")
    //                     .Append($"{TitleField.FieldType})")
    //                     .Append(@"\s*(.*)")
    //                     .ToString();

    //        FieldCodeRegex = new Regex(pattern, RegexOptions.IgnoreCase);
    //    }

    //    /// <summary>
    //    /// Parses a field code string into a structured RichFieldCode
    //    /// </summary>
    //    public IRichFieldCode Scan(string fieldCode)
    //    {
    //        if (string.IsNullOrWhiteSpace(fieldCode))
    //            return null;

    //        fieldCode = fieldCode.Trim();

    //        var match = FieldCodeRegex.Match(fieldCode);

    //        return ScanCore(match, fieldCode);
    //    }

    //    /// <summary>
    //    /// Parses multiple field codes from a text block
    //    /// </summary>
    //    public List<IRichFieldCode> ScanMultiple(string text)
    //    {
    //        var fields = new List<IRichFieldCode>();
    //        var matches = FieldCodeRegex.Matches(text);

    //        foreach (Match match in matches)
    //        {
    //            var fieldCode = ScanCore(match);
    //            if (fieldCode != null)
    //            {
    //                fields.Add(fieldCode);
    //            }
    //        }

    //        return fields;
    //    }

    //    private RichFieldCode ScanCore(Match match, string orginalCode = null)
    //    {
    //        var code = orginalCode ?? (match.Success ? match.Value : string.Empty);

    //        if (!match.Success)
    //        {
    //            return new RichFieldCode
    //            {
    //                FieldType = RichFieldType.Unknown,
    //                OriginalCode = code
    //            };
    //        }

    //        var fieldType = RichFieldCodesHelper.GetFieldType(match.Groups[1].Value);
    //        var parameters = match.Groups[2].Value.Trim();

    //        var fieldCode = new RichFieldCode
    //        {
    //            FieldType = fieldType,
    //            OriginalCode = code
    //        };

    //        // Parse property and switches
    //        ParsePropertyAndSwitches(fieldCode, parameters);

    //        // Handle special cases
    //        if (fieldType == RichFieldType.MergeField)
    //        {
    //            ParseMergeFieldProperty(fieldCode);
    //        }
    //        return fieldCode;
    //    }

    //    /// <summary>
    //    /// Parses property and switches from the parameters
    //    /// </summary>
    //    private void ParsePropertyAndSwitches(RichFieldCode fieldCode, string parameters)
    //    {
    //        if (string.IsNullOrWhiteSpace(parameters))
    //            return;

    //        // Find the first switch marker
    //        var switchMatch = Regex.Match(parameters, @"\\([@#\*a-zA-Z])");

    //        if (switchMatch.Success)
    //        {
    //            // Property is everything before the first switch
    //            fieldCode.Property = parameters.Substring(0, switchMatch.Index).Trim();

    //            // Parse switches
    //            var switchesStr = parameters.Substring(switchMatch.Index);
    //            ParseSwitches(fieldCode, switchesStr);
    //        }
    //        else
    //        {
    //            // No switches, entire string is property
    //            fieldCode.Property = parameters.Trim();
    //        }
    //    }

    //    /// <summary>
    //    /// Parses switches from a string
    //    /// </summary>
    //    private void ParseSwitches(RichFieldCode fieldCode, string switchesStr)
    //    {
    //        var matches = SwitchRegex.Matches(switchesStr);

    //        foreach (Match match in matches)
    //        {
    //            var sw = new RichFieldSwitch
    //            {
    //                SwitchCode = match.Groups[1].Success ? match.Groups[1].Value : null,
    //                FormatType = match.Groups[1].Success ? RichFieldSwitchsHelper.GetSwitchFormatType(match.Groups[1].Value) : RichFormatSwitchType.None,
    //                SwitchValue = match.Groups[2].Success ? match.Groups[2].Value.Trim() : null,
    //            };

    //            fieldCode.Switches.AddOrUpdate(sw);
    //        }
    //    }

    //    /// <summary>
    //    /// Parses arguments from a string
    //    /// </summary>
    //    private void ParseArgumentss(RichFieldCode fieldCode, string argumentsStr)
    //    {

    //    }

    //    /// <summary>
    //    /// Parses special merge field properties
    //    /// </summary>
    //    private void ParseMergeFieldProperty(RichFieldCode fieldCode)
    //    {
    //        if (string.IsNullOrEmpty(fieldCode.Property))
    //            return;

    //        fieldCode.EnsureCollection(fieldCode.Property);
    //    }

    //    #region IncludePicture Integration

    //    /// <summary>
    //    /// Parses INCLUDEPICTURE field code
    //    /// </summary>
    //    private IncludePictureFieldOptions ParseIncludePictureCode(string fieldCode)
    //    {
    //        if (string.IsNullOrWhiteSpace(fieldCode))
    //            return null;

    //        var info = new  IncludePictureFieldOptions();

    //        // Extract image path (between quotes)
    //        var pathMatch = Regex.Match(fieldCode,@"INCLUDEPICTURE\s+""([^""]+)""");
    //        if (pathMatch.Success)
    //        {
    //            info.ImagePath = pathMatch.Groups[1].Value;
    //        }

    //        // Extract width switch
    //        var widthMatch = Regex.Match(fieldCode, @"\\w\s+(\d+)");
    //        if (widthMatch.Success && int.TryParse(widthMatch.Groups[1].Value, out int width))
    //        {
    //            info.Width = width;
    //        }

    //        // Extract height switch
    //        var heightMatch = Regex.Match(fieldCode, @"\\h\s+(\d+)");
    //        if (heightMatch.Success && int.TryParse(heightMatch.Groups[1].Value, out int height))
    //        {
    //            info.Height = height;
    //        }

    //        // Check for MERGEFORMAT
    //        info.PreserveFormatting = fieldCode.Contains("\\* MERGEFORMAT");

    //        // Check for \d switch (link only)
    //        info.LinkOnly = fieldCode.Contains("\\d");

    //        // Check if contains MERGEFIELD
    //        info.HasMergeField = fieldCode.Contains("MERGEFIELD") || fieldCode.Contains("«");

    //        // Extract merge field name if present
    //        if (info.HasMergeField)
    //        {
    //            info.MergeFieldName = ExtractMergeFieldNameFromCode(fieldCode);
    //        }

    //        return info;
    //    }

    //    /// <summary>
    //    /// Extracts merge field name from INCLUDEPICTURE code
    //    /// </summary>
    //    private string ExtractMergeFieldNameFromCode(string fieldCode)
    //    {
    //        if (string.IsNullOrWhiteSpace(fieldCode))
    //            return null;

    //        // Check for MERGEFIELD keyword
    //        var mergeMatch = Regex.Match(fieldCode,@"MERGEFIELD\s+(\w+)", RegexOptions.IgnoreCase);
    //        if (mergeMatch.Success)
    //        {
    //            return mergeMatch.Groups[1].Value;
    //        }

    //        // Check for field placeholder «FieldName»
    //        var placeholderMatch = Regex.Match(fieldCode,@"«([^»]+)»");
    //        if (placeholderMatch.Success)
    //        {
    //            return placeholderMatch.Groups[1].Value;
    //        }

    //        return null;
    //    }

    //    #endregion

    //    #region Utility Methods for IncludePicture

    //    /// <summary>
    //    /// Replaces image path in INCLUDEPICTURE code
    //    /// </summary>
    //    public string ReplaceImagePath(string fieldCode, string newPath)
    //    {
    //        return Regex.Replace(fieldCode,@"""([^""]+)""", $"\"{newPath}\"");
    //    }

    //    /// <summary>
    //    /// Replaces static path with MERGEFIELD
    //    /// </summary>
    //    public string ReplacePathWithMergeField(string fieldCode, string mergeFieldName)
    //    {
    //        return Regex.Replace(fieldCode,@"""([^""]+)""", $"\"«{mergeFieldName}»\"");
    //    }

    //    /// <summary>
    //    /// Updates dimensions in INCLUDEPICTURE code
    //    /// </summary>
    //    public string UpdateDimensions(string fieldCode, int? width, int? height)
    //    {
    //        // Remove existing width/height switches
    //        fieldCode = Regex.Replace(fieldCode, @"\\w\s+\d+", "");
    //        fieldCode = Regex.Replace(fieldCode, @"\\h\s+\d+", "");

    //        // Add new dimensions
    //        if (width.HasValue)
    //        {
    //            fieldCode += $" \\w {width.Value}";
    //        }

    //        if (height.HasValue)
    //        {
    //            fieldCode += $" \\h {height.Value}";
    //        }

    //        return fieldCode.Trim();
    //    }

    //    #endregion
    //}


    /// <summary>
    /// Parses field codes from document text into structured RichFieldCode objects.
    /// Handles Microsoft Word field codes including MERGEFIELD, INCLUDEPICTURE, IF, HYPERLINK, etc.
    /// </summary>
    /// <remarks>
    /// Thread-safe implementation. All regex patterns are compiled once and reused.
    /// Field code format: {FIELD_TYPE property "argument1" "argument2" \switch1 value \switch2 value}
    /// </remarks>
    //public class RichFieldCodeScannerOld : IRichFieldCodeScanner
    //{
    //    #region Constants
    //    // Pattern groups: Group 1 = field type, Group 2 = remaining parameters
    //    private const int FIELD_TYPE_GROUP = 1;
    //    private const int PARAMETERS_GROUP = 2;

    //    // Switch pattern groups: Group 1 = switch code (e.g., \@, \#, \*), Group 2 = switch value
    //    private const string SWITCH_PATTERN = @"(\\[@#\*a-zA-Z]+)(?:\s+(""[^""]*""|[^\\]+))?";
    //    private const int SWITCH_CODE_GROUP = 1;
    //    private const int SWITCH_VALUE_GROUP = 2;

    //    // Argument pattern groups: Group 1 = argument content (inside quotes)
    //    private const string ARGUMENT_PATTERN = @"""([^""]*)""";
    //    private const int ARGUMENT_CONTENT_GROUP = 1;
    //    #endregion

    //    #region Static Fields
    //    /// <summary>
    //    /// Primary regex for extracting field type and parameters from field codes.
    //    /// Pattern: ^(FIELD_TYPE1|FIELD_TYPE2|...)\s*(.*)
    //    /// </summary>
    //    private static readonly Regex FieldCodeRegex;

    //    /// <summary>
    //    /// Regex for parsing switches like \@ "format", \# "format", \* MERGEFORMAT.
    //    /// Pattern: (\\[@#\*a-zA-Z]+)(?:\s+("[^"]*"|[^\\]+))?
    //    /// </summary>
    //    private static readonly Regex SwitchRegex;

    //    /// <summary>
    //    /// Regex for extracting quoted arguments from field parameters.
    //    /// Pattern: "([^"]*)"
    //    /// </summary>
    //    private static readonly Regex ArgumentRegex;

    //    /// <summary>
    //    /// Cached set of supported field types for quick lookup during validation.
    //    /// Optimizes field type checking from O(n) string comparison to O(1) HashSet lookup.
    //    /// </summary>
    //    private static readonly HashSet<string> SupportedFieldTypes;
    //    #endregion

    //    /// <summary>
    //    /// Static constructor to initialize compiled regex patterns and supported types cache.
    //    /// Patterns are compiled once for optimal performance across all instances.
    //    /// </summary>
    //    static RichFieldCodeScannerOld()
    //    {
    //        // Build field code pattern
    //        var pattern = FluentStringBuilder
    //                      .CreateNew()
    //                      .Append($"^({MergefieldField.FieldType}\\s+TableStart:|")
    //                      .Append($"{MergefieldField.FieldType}\\s+TableEnd:|")
    //                      .Append($"{MergefieldField.FieldType}|")
    //                      .Append($"{DateField.FieldType}|")
    //                      .Append($"{TimeField.FieldType}|")
    //                      .Append($"{PageField.FieldType}|")
    //                      .Append($"{NumPagesField.FieldType}|")
    //                      .Append($"{IfField.FieldType}|")
    //                      .Append($"{HyperlinkField.FieldType}|")
    //                      .Append($"{IncludepictureField.FieldType}|")
    //                      .Append($"{DocVariableField.FieldType}|")
    //                      .Append($"{DocumentPropertyField.FieldType}|")
    //                      .Append($"{RichCustomPropertyField.FieldType}|")
    //                      .Append($"{SymbolField.FieldType}|")
    //                      .Append($"{RefField.FieldType}|")
    //                      .Append($"{SequenceField.FieldType}|")
    //                      .Append($"{StyleRefField.FieldType}|")
    //                      .Append($"{TocField.FieldType}|")
    //                      .Append($"{AuthorField.FieldType}|")
    //                      .Append($"{CommentsField.FieldType}|")
    //                      .Append($"{CreatedateField.FieldType}|")
    //                      .Append($"{KeywordsField.FieldType}|")
    //                      .Append($"{LastSavedByField.FieldType}|")
    //                      .Append($"{PrintDateField.FieldType}|")
    //                      .Append($"{ReviewNumberField.FieldType}|")
    //                      .Append($"{SaveDateField.FieldType}|")
    //                      .Append($"{SectionPageField.FieldType}|")
    //                      .Append($"{SubjectField.FieldType}|")
    //                      .Append($"{TitleField.FieldType})")
    //                      .Append(@"\s*(.*)")
    //                      .ToString();

    //        FieldCodeRegex = new Regex(pattern, RegexOptions.IgnoreCase);

    //        //@"(\\[@#\*a-zA-Z]+)(?:\s+([^\\]+))?"
    //        // Switch pattern: \@ "format", \# "format", \* MERGEFORMAT
    //        SwitchRegex = new Regex(SWITCH_PATTERN, RegexOptions.Compiled);

    //        // Argument pattern: "value" (quoted strings)
    //        ArgumentRegex = new Regex(ARGUMENT_PATTERN, RegexOptions.Compiled);
    //    }

    //    #region Core Scanning Methods

    //    /// <summary>
    //    /// Parses a single field code string into a structured RichFieldCode object.
    //    /// </summary>
    //    /// <param name="fieldCode">The raw field code string (e.g., "MERGEFIELD FirstName \* MERGEFORMAT")</param>
    //    /// <returns>Parsed RichFieldCode or null if input is invalid/empty</returns>
    //    /// <remarks>
    //    /// Handles edge cases:
    //    /// - Null/whitespace input returns null
    //    /// - Unrecognized field types return RichFieldType.Unknown
    //    /// - Malformed field codes return best-effort parsing
    //    /// </remarks>
    //    public IRichFieldCode Scan(string fieldCode)
    //    {
    //        // Guard clause: return null for invalid input
    //        if (string.IsNullOrWhiteSpace(fieldCode))
    //            return null;

    //        // Trim to remove leading/trailing whitespace that could affect regex matching
    //        fieldCode = fieldCode.Trim();

    //        var match = FieldCodeRegex.Match(fieldCode);
    //        return ScanCore(match, fieldCode);
    //    }

    //    /// <summary>
    //    /// Parses multiple field codes from a text block containing one or more field codes.
    //    /// Uses regex matching to find all field codes in the text.
    //    /// </summary>
    //    /// <param name="text">Text block potentially containing multiple field codes</param>
    //    /// <returns>Collection of parsed field codes</returns>
    //    /// <remarks>
    //    /// Performance optimization: Uses Matches() instead of iterative Match() for single-pass collection.
    //    /// </remarks>
    //    public IRichFieldCodeCollection ScanMultiple(string text)
    //    {
    //        var fields = new RichFieldCodeCollection();

    //        // Guard clause: return empty collection for invalid input
    //        if (string.IsNullOrWhiteSpace(text))
    //            return fields;

    //        // Single-pass extraction of all field code matches
    //        var matches = FieldCodeRegex.Matches(text);

    //        // Pre-allocate capacity to avoid resizing during population
    //        fields.Capacity = matches.Count;

    //        foreach (Match match in matches)
    //        {
    //            var fieldCode = ScanCore(match);
    //            if (fieldCode != null)
    //            {
    //                fields.Add(fieldCode);
    //            }
    //        }

    //        return fields;
    //    }

    //    /// <summary>
    //    /// Core scanning logic shared between single and multiple field code parsing.
    //    /// Extracts field type, parameters, and delegates to specialized parsers.
    //    /// </summary>
    //    /// <param name="match">Regex match result from FieldCodeRegex</param>
    //    /// <param name="originalCode">Original field code string (optional, used when match fails)</param>
    //    /// <returns>Structured RichFieldCode object</returns>
    //    /// <remarks>
    //    /// Design decisions:
    //    /// - Returns Unknown field type for failed matches instead of null for graceful degradation
    //    /// - Separates field type extraction from parameter parsing for maintainability
    //    /// </remarks>
    //    private RichFieldCode ScanCore(Match match, string originalCode = null)
    //    {
    //        // Preserve original code for debugging and reconstruction
    //        var code = originalCode ?? (match.Success ? match.Value : string.Empty);

    //        // Failed match: return unknown field type with original code preserved
    //        if (!match.Success)
    //        {
    //            return new RichFieldCode
    //            {
    //                FieldType = RichFieldType.Unknown,
    //                OriginalCode = code
    //            };
    //        }

    //        // Extract field type from regex capture group
    //        var fieldTypeString = match.Groups[FIELD_TYPE_GROUP].Value;

    //        // Convert string field type to enum value (validated against supported types)
    //        var fieldType = RichFieldCodesHelper.GetFieldType(fieldTypeString);

    //        // Extract parameters (everything after field type name)
    //        var parameters = match.Groups[PARAMETERS_GROUP].Value.Trim();

    //        var fieldCode = new RichFieldCode
    //        {
    //            FieldType = fieldType,
    //            OriginalCode = code
    //        };

    //        // Parse property, arguments, and switches from parameters
    //        ParseParameters(fieldCode, parameters);

    //        // Handle field-type-specific parsing
    //        // Only MERGEFIELD currently requires special property handling
    //        if (fieldType == RichFieldType.MergeField)
    //        {
    //            ParseMergeFieldProperty(fieldCode);
    //        }

    //        return fieldCode;
    //    }

    //    #endregion

    //    #region Parameter Parsing

    //    /// <summary>
    //    /// Orchestrates the parsing of all parameter components: property, arguments, and switches.
    //    /// Parsing order is important: arguments first, then property and switches from cleaned text.
    //    /// </summary>
    //    /// <param name="fieldCode">Field code object to populate</param>
    //    /// <param name="parameters">Raw parameter string from field code</param>
    //    /// <remarks>
    //    /// Example: "FirstName "LastName" \* MERGEFORMAT \@ "MMMM d, yyyy""
    //    /// - Arguments: ["LastName"]
    //    /// - Property: "FirstName"
    //    /// - Switches: [\* MERGEFORMAT, \@ "MMMM d, yyyy"]
    //    /// </remarks>
    //    private void ParseParameters(RichFieldCode fieldCode, string parameters)
    //    {
    //        if (string.IsNullOrWhiteSpace(parameters))
    //            return;

    //        // Step 1: Extract quoted arguments first (they may contain spaces/switches that would confuse parsing)
    //        var arguments = ParseArguments(parameters);

    //        // Step 2: Remove arguments from parameters to get clean property+switches string
    //        var cleanedParameters = RemoveArgumentsFromParameters(parameters);

    //        // Step 3: Parse property (pre-switch text) and switches from cleaned string
    //        ParsePropertyAndSwitches(fieldCode, cleanedParameters);

    //        // Step 4: Add parsed arguments to field code
    //        foreach (var arg in arguments)
    //        {
    //            fieldCode.Arguments.Add(new RichFieldArgument(arg));
    //        }
    //    }

    //    /// <summary>
    //    /// Extracts all quoted string arguments from the parameter string.
    //    /// Arguments are always enclosed in double quotes in field codes.
    //    /// </summary>
    //    /// <param name="parameters">Raw parameter string</param>
    //    /// <returns>List of argument values (without quotes)</returns>
    //    /// <remarks>
    //    /// Example: '"Arg1" "Arg2"' returns ["Arg1", "Arg2"]
    //    /// Performance: Returns empty list instead of null to avoid null checks downstream
    //    /// </remarks>
    //    private List<string> ParseArguments(string parameters)
    //    {
    //        var arguments = new List<string>();

    //        // Guard clause: return empty list for null/whitespace
    //        if (string.IsNullOrWhiteSpace(parameters))
    //            return arguments;

    //        var matches = ArgumentRegex.Matches(parameters);

    //        foreach (Match match in matches)
    //        {
    //            if (match.Groups[ARGUMENT_CONTENT_GROUP].Success)
    //            {
    //                arguments.Add(match.Groups[ARGUMENT_CONTENT_GROUP].Value);
    //            }
    //        }

    //        return arguments;
    //    }

    //    /// <summary>
    //    /// Removes all quoted argument strings from the parameters to simplify switch/property parsing.
    //    /// Uses regex replacement for efficiency instead of iterative string manipulation.
    //    /// </summary>
    //    /// <param name="parameters">Original parameters string</param>
    //    /// <param name="arguments">List of arguments (not used directly, method operates on regex)</param>
    //    /// <returns>Parameters string with all quoted sections removed</returns>
    //    /// <remarks>
    //    /// Optimization: Uses ArgumentRegex.Replace() which internally performs a single pass
    //    /// rather than manual string operations which would require multiple passes.
    //    /// </remarks>
    //    private string RemoveArgumentsFromParameters(string parameters)
    //    {
    //        // Direct regex replacement: remove all quoted strings
    //        // Using Regex.Replace is more efficient than iterative substring operations
    //        return ArgumentRegex.Replace(parameters, string.Empty).Trim();
    //    }

    //    /// <summary>
    //    /// Parses field property (text before first switch) and switch components.
    //    /// Property is the field identifier/name, switches control formatting and behavior.
    //    /// </summary>
    //    /// <param name="fieldCode">Field code object to populate</param>
    //    /// <param name="parameters">Cleaned parameters string (arguments removed)</param>
    //    /// <remarks>
    //    /// Example: "FirstName \* MERGEFORMAT \@ "MMMM d, yyyy""
    //    /// - Property: "FirstName"
    //    /// - Switches: \* MERGEFORMAT, \@ "MMMM d, yyyy"
    //    /// 
    //    /// Switch detection: Looks for \ followed by @, #, *, or letter
    //    /// </remarks>
    //    private void ParsePropertyAndSwitches(RichFieldCode fieldCode, string parameters)
    //    {
    //        if (string.IsNullOrWhiteSpace(parameters))
    //            return;

    //        // Locate the first switch marker (backslash followed by special character or letter)
    //        var switchMatch = Regex.Match(parameters, @"\\([@#\*a-zA-Z])");

    //        if (switchMatch.Success)
    //        {
    //            // Property is everything before the first switch
    //            // Using Substring with known index for performance
    //            fieldCode.Property = parameters.Substring(0, switchMatch.Index).Trim();

    //            // Extract switch portion for dedicated parsing
    //            var switchesStr = parameters.Substring(switchMatch.Index);
    //            ScanSwitches(fieldCode.Switches, switchesStr);
    //        }
    //        else
    //        {
    //            // No switches present: entire string is the property
    //            fieldCode.Property = parameters.Trim();
    //        }
    //    }

    //    #endregion

    //    #region Switch Parsing

    //    /// <summary>
    //    /// Parses a single switch code string into a rich field switch object.
    //    /// Returns null if the input is not a valid switch.
    //    /// </summary>
    //    /// <param name="switcheCode">String containing a single switch definition (e.g., "\@", "\* MERGEFORMAT", "\# "$#,##0.00"")</param>
    //    /// <returns>Parsed switch object or null if invalid</returns>
    //    /// <remarks>
    //    /// Handles edge cases:
    //    /// - Null/whitespace input returns null
    //    /// - Invalid switch codes return null
    //    /// - Unrecognized switch types return RichFormatSwitchType.None
    //    /// </remarks>
    //    public IRichFieldSwitch ScanSwitch(string switcheCode)
    //    {
    //        // Guard clause: return null for invalid input
    //        if (string.IsNullOrWhiteSpace(switcheCode))
    //            return null;

    //        // Trim to remove leading/trailing whitespace that could affect regex matching
    //        switcheCode = switcheCode.Trim();

    //        var match = SwitchRegex.Match(switcheCode);
    //        return ScanSwitchCore(match, switcheCode);
    //    }


    //    // Option 1: Extract just the target switch portion and parse it
    //    public IRichFieldSwitch FindSpecificSwitch(string parameters, string targetSwitchCode)
    //    {
    //        if (string.IsNullOrWhiteSpace(parameters) || string.IsNullOrWhiteSpace(targetSwitchCode))
    //            return null;

    //        // Escape the switch code for regex (backslash is special)
    //        var escapedSwitchCode = Regex.Escape(targetSwitchCode);

    //        // Find the specific switch and capture everything until the next switch or end of string
    //        var pattern = $@"({escapedSwitchCode})(\s+[^\\]*)?(?=\\|$)";
    //        var match = Regex.Match(parameters, pattern);

    //        if (match.Success)
    //        {
    //            // Use ScanSwitch to parse just the matched portion
    //            return ScanSwitch(match.Value);
    //        }

    //        return null;
    //    }

    //    /// <summary>
    //    /// Public API for parsing switches from a parameter string.
    //    /// Returns a collection of switches without requiring a full field code object.
    //    /// </summary>
    //    /// <param name="parameters">String containing switch definitions</param>
    //    /// <returns>Collection of parsed switches</returns>
    //    /// <remarks>
    //    /// Useful for standalone switch parsing without full field code context.
    //    /// Example input: "\* MERGEFORMAT \@ "MMMM d, yyyy""
    //    /// </remarks>
    //    public IRichFieldSwitchReadOnlyCollection ScanSwitches(string parameters)
    //    {
    //        var switches = new RichFieldSwitchCollection();

    //        ScanSwitches(switches, parameters);

    //        return switches;
    //    }

    //    /// <summary>
    //    /// Public API for parsing switches from a parameter string.
    //    /// Returns a collection of switches without requiring a full field code object.
    //    /// </summary>
    //    /// <param name="parameters">String containing switch definitions</param>
    //    /// <returns>Collection of parsed switches</returns>
    //    /// <remarks>
    //    /// Useful for standalone switch parsing without full field code context.
    //    /// Example input: "\* MERGEFORMAT \@ "MMMM d, yyyy""
    //    /// </remarks>
    //    public void ScanSwitches(IRichFieldSwitchCollection switches, string parameters)
    //    {
    //        // Guard clause: return empty collection for invalid input
    //        if (string.IsNullOrWhiteSpace(parameters))
    //            return;

    //        // Locate the first switch marker to skip any non-switch text
    //        var switchMatch = Regex.Match(parameters, @"\\([@#\*a-zA-Z])");

    //        if (!switchMatch.Success)
    //            return;

    //        // Extract switch portion and parse
    //        var switchesStr = parameters.Substring(switchMatch.Index);

    //        // Single-pass extraction of all switch matches
    //        var matches = SwitchRegex.Matches(switchesStr);

    //        foreach (Match match in matches)
    //        {
    //            var sw = ScanSwitchCore(match);
    //            if (sw != null)
    //            {
    //                switches.AddOrUpdate(sw);
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// Core switch scanning logic shared between single and multiple switch parsing.
    //    /// Extracts switch code, value, and determines format type.
    //    /// </summary>
    //    /// <param name="match">Regex match result from SwitchRegex</param>
    //    /// <param name="originalCode">Original switch code string (optional, used when match fails)</param>
    //    /// <returns>Structured RichFieldSwitch object or null if parsing fails</returns>
    //    /// <remarks>
    //    /// Design decisions:
    //    /// - Returns null for failed matches to allow filtering in ScanSwitches
    //    /// - Centralizes switch creation logic for consistency
    //    /// - Uses same regex group extraction for both single and multiple parsing
    //    /// </remarks>
    //    private RichFieldSwitch ScanSwitchCore(Match match, string originalCode = null)
    //    {
    //        // Preserve original code for debugging and reconstruction
    //        var code = originalCode ?? (match.Success ? match.Value : string.Empty);

    //        // Failed match: return null (unlike field codes, we don't create Unknown switches)
    //        if (!match.Success)
    //            return null;

    //        // Parse switch components from regex groups
    //        var switchCode = match.Groups[SWITCH_CODE_GROUP].Success
    //            ? match.Groups[SWITCH_CODE_GROUP].Value
    //            : null;

    //        var switchValue = match.Groups[SWITCH_VALUE_GROUP].Success
    //            ? match.Groups[SWITCH_VALUE_GROUP].Value.Trim().Trim('"') // Remove quotes if present
    //            : null;

    //        // Determine format type from switch code
    //        var formatType = switchCode != null
    //            ? RichFieldSwitchsHelper.GetSwitchFormatType(switchCode)
    //            : RichFormatSwitchType.None;

    //        return new RichFieldSwitch
    //        {
    //            SwitchCode = switchCode,
    //            FormatType = formatType,
    //            SwitchValue = switchValue
    //        };
    //    }

    //    #endregion

    //    #region MergeField Parsing

    //    /// <summary>
    //    /// Parses special merge field property to handle merge field names that may contain
    //    /// spaces or special characters. Ensures proper collection initialization.
    //    /// </summary>
    //    /// <param name="fieldCode">Merge field code object</param>
    //    /// <remarks>
    //    /// MERGEFIELD names can be complex and require special handling.
    //    /// This method ensures the field code's internal collections are properly initialized.
    //    /// </remarks>
    //    private void ParseMergeFieldProperty(RichFieldCode fieldCode)
    //    {
    //        if (string.IsNullOrEmpty(fieldCode.Property))
    //            return;

    //        // Ensure collection is properly initialized with property value
    //        fieldCode.EnsureCollection(fieldCode.Property);
    //    }

    //    #endregion

    //    #region Specialized Field Type Parsers

    //    /// <summary>
    //    /// Parses INCLUDEPICTURE field with arguments and dimension switches.
    //    /// Handles both static image paths and MERGEFIELD-based dynamic paths.
    //    /// </summary>
    //    /// <param name="fieldCode">Raw INCLUDEPICTURE field code</param>
    //    /// <returns>Structured options object or null if parsing fails</returns>
    //    /// <remarks>
    //    /// Example: INCLUDEPICTURE "path" "QRCode" \w 100 \h 100
    //    /// - Arguments: ["path", "QRCode"]
    //    /// - Switches: \w 100, \h 100
    //    /// Supports barcode integration via ImageOption parameter.
    //    /// </remarks>
    //    public IncludePictureFieldOptions ParseIncludePictureCode(string fieldCode)
    //    {
    //        if (string.IsNullOrWhiteSpace(fieldCode))
    //            return null;

    //        // Use general scanner to parse field structure
    //        var scanned = Scan(fieldCode);

    //        // Validate: must be IncludePicture type
    //        if (scanned == null || scanned.FieldType != RichFieldType.IncludePictureField)
    //            return null;

    //        var options = new IncludePictureFieldOptions
    //        {
    //            // First argument is always the image path
    //            ImagePath = scanned.Arguments.GetFirstValue(),

    //            // Second argument may contain barcode type or other options
    //            ImageOption = scanned.Arguments.GetStringValue(1)
    //        };

    //        // Extract width from \w switch
    //        var widthSwitch = scanned.Switches.FirstOrDefault(s => s.SwitchCode == "\\w");
    //        if (widthSwitch != null && int.TryParse(widthSwitch.SwitchValue, out int width))
    //        {
    //            options.Width = width;
    //        }

    //        // Extract height from \h switch
    //        var heightSwitch = scanned.Switches.FirstOrDefault(s => s.SwitchCode == "\\h");
    //        if (heightSwitch != null && int.TryParse(heightSwitch.SwitchValue, out int height))
    //        {
    //            options.Height = height;
    //        }

    //        // Check for MERGEFORMAT switch (preserves formatting on field update)
    //        options.PreserveFormatting = scanned.Switches.Any(s =>
    //            s.SwitchCode == "\\*" &&
    //            s.SwitchValue?.IndexOf("MERGEFORMAT", StringComparison.OrdinalIgnoreCase) >= 0);

    //        // Check for \d switch (prevents image embedding, stores link only)
    //        options.LinkOnly = scanned.Switches.Any(s =>
    //            s.SwitchCode?.Equals("\\d", StringComparison.OrdinalIgnoreCase) == true);

    //        // Detect MERGEFIELD usage in path for dynamic image resolution
    //        if (options.ImagePath != null)
    //        {
    //            options.HasMergeField = options.ImagePath.IndexOf("MERGEFIELD", StringComparison.OrdinalIgnoreCase) >= 0 ||
    //                                   options.ImagePath.Contains('«'); // Check for field placeholder characters

    //            if (options.HasMergeField)
    //            {
    //                options.MergeFieldName = ExtractMergeFieldName(options.ImagePath);
    //            }
    //        }

    //        return options;
    //    }

    //    /// <summary>
    //    /// Parses IF field conditional statement.
    //    /// Extracts expression, true text, and false text from field code.
    //    /// </summary>
    //    /// <param name="fieldCode">Raw IF field code</param>
    //    /// <returns>Structured options or null if parsing fails</returns>
    //    /// <remarks>
    //    /// Example: IF "condition" "true text" "false text"
    //    /// - Property: condition expression
    //    /// - Argument 0: text to display if true
    //    /// - Argument 1: text to display if false
    //    /// </remarks>
    //    public IfFieldOptions ParseIfFieldCode(string fieldCode)
    //    {
    //        var scanned = Scan(fieldCode) as RichFieldCode;

    //        if (scanned == null || scanned.FieldType != RichFieldType.IfField)
    //            return null;

    //        return new IfFieldOptions
    //        {
    //            Expression = scanned.Property,      // Conditional expression
    //            TrueText = scanned.Arguments.GetStringValue(0),  // Display when condition is true
    //            FalseText = scanned.Arguments.GetStringValue(1)  // Display when condition is false
    //        };
    //    }

    //    /// <summary>
    //    /// Parses HYPERLINK field with URL and display text.
    //    /// </summary>
    //    /// <param name="fieldCode">Raw HYPERLINK field code</param>
    //    /// <returns>Structured options or null if parsing fails</returns>
    //    /// <remarks>
    //    /// Example: HYPERLINK "https://url.com" "Click Here"
    //    /// - Argument 0: URL target
    //    /// - Argument 1: Display text (optional)
    //    /// </remarks>
    //    public HyperlinkFieldOptions ParseHyperlinkFieldCode(string fieldCode)
    //    {
    //        var scanned = Scan(fieldCode) as RichFieldCode;

    //        if (scanned == null || scanned.FieldType != RichFieldType.HyperlinkField)
    //            return null;

    //        return new HyperlinkFieldOptions
    //        {
    //            Url = scanned.Arguments.GetFirstValue(),    // Link target URL
    //            DisplayText = scanned.Arguments.GetStringValue(1) // Optional display text
    //        };
    //    }

    //    /// <summary>
    //    /// Parses DOCVARIABLE field with variable name and default value.
    //    /// Handles both property-based and argument-based variable naming conventions.
    //    /// </summary>
    //    /// <param name="fieldCode">Raw DOCVARIABLE field code</param>
    //    /// <returns>Structured options or null if parsing fails</returns>
    //    /// <remarks>
    //    /// Flexible parsing: variable name can be in property or first argument position.
    //    /// Example 1: DOCVARIABLE "varName" "defaultValue"
    //    /// Example 2: DOCVARIABLE varName "defaultValue"
    //    /// </remarks>
    //    public DocVariableFieldOptions ParseDocVariableFieldCode(string fieldCode)
    //    {
    //        var scanned = Scan(fieldCode) as RichFieldCode;

    //        if (scanned == null || scanned.FieldType != RichFieldType.DocVariableField)
    //            return null;

    //        return new DocVariableFieldOptions
    //        {
    //            // Variable name: prefer property, fall back to first argument
    //            VariableName = scanned.Property ?? scanned.Arguments.GetFirstValue(),

    //            // Default value: position depends on where variable name was found
    //            DefaultValue = scanned.Arguments.GetStringValue(scanned.Property != null ? 0 : 1)
    //        };
    //    }

    //    /// <summary>
    //    /// Parses custom BARCODE field with barcode type, value, and dimensions.
    //    /// Typically implemented as a specialized MERGEFIELD with additional arguments.
    //    /// </summary>
    //    /// <param name="fieldCode">Raw barcode field code</param>
    //    /// <returns>Structured options or null if parsing fails</returns>
    //    /// <remarks>
    //    /// Example: MERGEFIELD Barcode "QRCode" "ProductCode" 100 50
    //    /// - Argument 0: Barcode type (QRCode, Code128, etc.)
    //    /// - Argument 1: Value to encode
    //    /// - Argument 2/3: Width/Height (optional numeric arguments)
    //    /// </remarks>
    //    public BarcodeFieldOptions ParseBarcodeFieldCode(string fieldCode)
    //    {
    //        var scanned = Scan(fieldCode) as RichFieldCode;

    //        if (scanned == null)
    //            return null;

    //        var options = new BarcodeFieldOptions
    //        {
    //            // Value to encode: from argument 1 or property as fallback
    //            Value = scanned.Arguments.GetStringValue(1) ?? scanned.Property,

    //            // Dimensions: parsed as integers from arguments 2 and 3
    //            Width = scanned.Arguments.GetValue<int>(2),
    //            Height = scanned.Arguments.GetValue<int>(3)
    //        };

    //        // Parse barcode type from first argument
    //        if (EnumerationHelper.TryParse<FieldBarcodeType>(scanned.Arguments.GetStringValue(0), out var barcodeType))
    //        {
    //            options.BarcodeType = barcodeType;
    //        }

    //        return options;
    //    }

    //    #endregion

    //    #region Utility Methods

    //    /// <summary>
    //    /// Extracts merge field name from a path string that may contain MERGEFIELD references.
    //    /// Handles both explicit MERGEFIELD syntax and field placeholder characters («»).
    //    /// </summary>
    //    /// <param name="path">Path string potentially containing MERGEFIELD reference</param>
    //    /// <returns>Extracted field name or null if not found</returns>
    //    /// <remarks>
    //    /// Two extraction strategies:
    //    /// 1. MERGEFIELD FieldName format (explicit keyword)
    //    /// 2. «FieldName» format (Word field placeholder characters)
    //    /// 
    //    /// Performance: Uses OrdinalIgnoreCase for case-insensitive matching.
    //    /// </remarks>
    //    public string ExtractMergeFieldName(string path)
    //    {
    //        if (string.IsNullOrWhiteSpace(path))
    //            return null;

    //        // Strategy 1: Check for explicit MERGEFIELD keyword pattern
    //        var mergeMatch = Regex.Match(
    //            path,
    //            @"MERGEFIELD\s+(\w+)",
    //            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

    //        if (mergeMatch.Success)
    //        {
    //            return mergeMatch.Groups[1].Value;
    //        }

    //        // Strategy 2: Check for Word field placeholder characters «FieldName»
    //        var placeholderMatch = Regex.Match(path, @"«([^»]+)»");
    //        if (placeholderMatch.Success)
    //        {
    //            return placeholderMatch.Groups[1].Value;
    //        }

    //        return null;
    //    }

    //    /// <summary>
    //    /// Replaces the image path (first quoted string) in an INCLUDEPICTURE field code.
    //    /// </summary>
    //    /// <param name="fieldCode">Original INCLUDEPICTURE field code</param>
    //    /// <param name="newPath">New image path to insert</param>
    //    /// <returns>Modified field code with replaced path</returns>
    //    /// <remarks>
    //    /// Only replaces the first quoted argument (image path), preserves all other arguments and switches.
    //    /// Uses regex replacement for robustness against various formatting.
    //    /// </remarks>
    //    public string ReplaceImagePath(string fieldCode, string newPath)
    //    {
    //        if (string.IsNullOrWhiteSpace(fieldCode) || string.IsNullOrWhiteSpace(newPath))
    //            return fieldCode;

    //        // Replace only the first quoted string to avoid affecting switch values or other arguments
    //        var regex = new Regex(@"""([^""]+)""");
    //        return regex.Replace(fieldCode, $"\"{newPath}\"", 1); // Count=1 limits to first match
    //    }

    //    /// <summary>
    //    /// Replaces static image path with a MERGEFIELD reference for dynamic images.
    //    /// Converts INCLUDEPICTURE "staticPath" to INCLUDEPICTURE "«MergeFieldName»"
    //    /// </summary>
    //    /// <param name="fieldCode">Original INCLUDEPICTURE field code</param>
    //    /// <param name="mergeFieldName">Name of merge field to use</param>
    //    /// <returns>Modified field code with MERGEFIELD placeholder</returns>
    //    /// <remarks>
    //    /// Uses Word field placeholder characters («») for proper Word rendering.
    //    /// </remarks>
    //    public string ReplacePathWithMergeField(string fieldCode, string mergeFieldName)
    //    {
    //        if (string.IsNullOrWhiteSpace(fieldCode) || string.IsNullOrWhiteSpace(mergeFieldName))
    //            return fieldCode;

    //        // Replace first quoted path with MERGEFIELD placeholder
    //        var regex = new Regex(@"""([^""]+)""");
    //        return regex.Replace(fieldCode, $"\"«{mergeFieldName}»\"", 1);
    //    }

    //    /// <summary>
    //    /// Updates or adds dimension switches (\w and \h) in an INCLUDEPICTURE field code.
    //    /// Removes existing dimension switches and adds new ones if values are provided.
    //    /// </summary>
    //    /// <param name="fieldCode">Original INCLUDEPICTURE field code</param>
    //    /// <param name="width">New width value (null to remove width switch)</param>
    //    /// <param name="height">New height value (null to remove height switch)</param>
    //    /// <returns>Modified field code with updated dimensions</returns>
    //    /// <remarks>
    //    /// Handles edge cases:
    //    /// - Removes existing dimension switches before adding new ones
    //    /// - Only adds switches for non-null values
    //    /// - Preserves all other switches and arguments
    //    /// 
    //    /// Example: "INCLUDEPICTURE "img.jpg" \w 100" → "INCLUDEPICTURE "img.jpg" \w 200 \h 150"
    //    /// </remarks>
    //    public string UpdateImageDimensions(string fieldCode, int? width, int? height)
    //    {
    //        if (string.IsNullOrWhiteSpace(fieldCode))
    //            return fieldCode;

    //        // Remove existing dimension switches to prevent duplicates
    //        // Use pattern that matches switch + optional whitespace + digits
    //        fieldCode = Regex.Replace(fieldCode, @"\\w\s+\d+", "", RegexOptions.IgnoreCase);
    //        fieldCode = Regex.Replace(fieldCode, @"\\h\s+\d+", "", RegexOptions.IgnoreCase);

    //        // Append new dimensions if values are provided
    //        if (width.HasValue)
    //            fieldCode += $" \\w {width.Value}";

    //        if (height.HasValue)
    //            fieldCode += $" \\h {height.Value}";

    //        // Clean up any double spaces from replacement operations
    //        return Regex.Replace(fieldCode.Trim(), @"\s+", " ");
    //    }

    //    /// <summary>
    //    /// Validates if a field type string is in the supported types collection.
    //    /// Uses cached HashSet for O(1) lookup instead of O(n) string comparison.
    //    /// </summary>
    //    /// <param name="fieldType">Field type string to validate</param>
    //    /// <returns>True if field type is supported</returns>
    //    /// <remarks>
    //    /// Performance: Uses StringComparer.OrdinalIgnoreCase for case-insensitive comparison.
    //    /// This method can be used for pre-validation before attempting to scan field codes.
    //    /// </remarks>
    //    public bool IsSupportedFieldType(string fieldType)
    //    {
    //        return !string.IsNullOrEmpty(fieldType) &&
    //               SupportedFieldTypes.Contains(fieldType);
    //    }

    //    public RichFieldSwitchInfo GetSwitchInfo(string rawCodeText, string switchCode)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    #endregion
    //}

    ///// <summary>
    ///// Parses field codes from document text into structured RichFieldCode objects
    ///// </summary>
    //public class RichFieldCodeScanner : IRichFieldCodeScanner
    //{
    //    private static readonly Regex FieldCodeRegex;
    //    private static readonly Regex SwitchRegex;
    //    private static readonly Regex ArgumentRegex;

    //    static RichFieldCodeScanner()
    //    {
    //        // Build field code pattern
    //        var pattern = FluentStringBuilder
    //                      .CreateNew()
    //                      .Append($"^({MergefieldField.FieldType}\\s+TableStart:|")
    //                      .Append($"{MergefieldField.FieldType}\\s+TableEnd:|")
    //                      .Append($"{MergefieldField.FieldType}|")
    //                      .Append($"{DateField.FieldType}|")
    //                      .Append($"{TimeField.FieldType}|")
    //                      .Append($"{PageField.FieldType}|")
    //                      .Append($"{NumPagesField.FieldType}|")
    //                      .Append($"{IfField.FieldType}|")
    //                      .Append($"{HyperlinkField.FieldType}|")
    //                      .Append($"{IncludepictureField.FieldType}|")
    //                      .Append($"{DocVariableField.FieldType}|")
    //                      .Append($"{DocumentPropertyField.FieldType}|")
    //                      .Append($"{RichCustomPropertyField.FieldType}|")
    //                      .Append($"{SymbolField.FieldType}|")
    //                      .Append($"{RefField.FieldType}|")
    //                      .Append($"{SequenceField.FieldType}|")
    //                      .Append($"{StyleRefField.FieldType}|")
    //                      .Append($"{TocField.FieldType}|")
    //                      .Append($"{AuthorField.FieldType}|")
    //                      .Append($"{CommentsField.FieldType}|")
    //                      .Append($"{CreatedateField.FieldType}|")
    //                      .Append($"{KeywordsField.FieldType}|")
    //                      .Append($"{LastSavedByField.FieldType}|")
    //                      .Append($"{PrintDateField.FieldType}|")
    //                      .Append($"{ReviewNumberField.FieldType}|")
    //                      .Append($"{SaveDateField.FieldType}|")
    //                      .Append($"{SectionPageField.FieldType}|")
    //                      .Append($"{SubjectField.FieldType}|")
    //                      .Append($"{TitleField.FieldType})")
    //                      .Append(@"\s*(.*)")
    //                      .ToString();

    //        FieldCodeRegex = new Regex(pattern, RegexOptions.IgnoreCase);

    //        //@"(\\[@#\*a-zA-Z]+)(?:\s+([^\\]+))?"
    //        // Switch pattern: \@ "format", \# "format", \* MERGEFORMAT
    //        SwitchRegex = new Regex(
    //            @"(\\[@#\*a-zA-Z]+)(?:\s+(""[^""]*""|[^\\]+))?",
    //            RegexOptions.Compiled);

    //        // Argument pattern: "value" (quoted strings)
    //        ArgumentRegex = new Regex(
    //            @"""([^""]*)""",
    //            RegexOptions.Compiled);
    //    }

    //    #region Scan
    //    /// <summary>
    //    /// Parses a field code string into a structured RichFieldCode
    //    /// </summary>
    //    public IRichFieldCode Scan(string fieldCode)
    //    {
    //        if (string.IsNullOrWhiteSpace(fieldCode))
    //            return null;

    //        fieldCode = fieldCode.Trim();

    //        var match = FieldCodeRegex.Match(fieldCode);
    //        return ScanCore(match, fieldCode);
    //    }

    //    /// <summary>
    //    /// Parses multiple field codes from a text block
    //    /// </summary>
    //    public IRichFieldCodeCollection ScanMultiple(string text)
    //    {
    //        var fields = new RichFieldCodeCollection();
    //        var matches = FieldCodeRegex.Matches(text);

    //        foreach (Match match in matches)
    //        {
    //            var fieldCode = ScanCore(match);
    //            if (fieldCode != null)
    //            {
    //                fields.Add(fieldCode);
    //            }
    //        }

    //        return fields;
    //    }

    //    /// <summary>
    //    /// Core scanning logic
    //    /// </summary>
    //    private RichFieldCode ScanCore(Match match, string originalCode = null)
    //    {
    //        var code = originalCode ?? (match.Success ? match.Value : string.Empty);

    //        if (!match.Success)
    //        {
    //            return new RichFieldCode
    //            {
    //                FieldType = RichFieldType.Unknown,
    //                OriginalCode = code
    //            };
    //        }

    //        var fieldType = RichFieldCodesHelper.GetFieldType(match.Groups[1].Value);
    //        var parameters = match.Groups[2].Value.Trim();

    //        var fieldCode = new RichFieldCode
    //        {
    //            FieldType = fieldType,
    //            OriginalCode = code
    //        };

    //        // Parse property, arguments, and switches
    //        ParseParameters(fieldCode, parameters);

    //        // Handle special cases
    //        if (fieldType == RichFieldType.MergeField)
    //        {
    //            ParseMergeFieldProperty(fieldCode);
    //        }

    //        return fieldCode;
    //    }

    //    /// <summary>
    //    /// Parses all parameters: property, arguments, and switches
    //    /// </summary>
    //    private void ParseParameters(RichFieldCode fieldCode, string parameters)
    //    {
    //        if (string.IsNullOrWhiteSpace(parameters))
    //            return;

    //        // Step 1: Extract quoted arguments
    //        var arguments = ParseArguments(parameters);

    //        // Remove arguments from parameters for easier parsing
    //        var cleanedParameters = RemoveArgumentsFromParameters(parameters, arguments);

    //        // Step 2: Parse property (everything before first switch)
    //        ParsePropertyAndSwitches(fieldCode, cleanedParameters);

    //        // Step 3: Add parsed arguments
    //        foreach (var arg in arguments)
    //        {
    //            fieldCode.Arguments.Add(new RichFieldArgument(arg));
    //        }
    //    }

    //    /// <summary>
    //    /// Parses quoted arguments from parameters
    //    /// </summary>
    //    private List<string> ParseArguments(string parameters)
    //    {
    //        var arguments = new List<string>();

    //        var matches = ArgumentRegex.Matches(parameters);

    //        foreach (Match match in matches)
    //        {
    //            if (match.Groups[1].Success)
    //            {
    //                arguments.Add(match.Groups[1].Value);
    //            }
    //        }

    //        return arguments;
    //    }

    //    /// <summary>
    //    /// Removes quoted arguments from parameters string
    //    /// </summary>
    //    private string RemoveArgumentsFromParameters(string parameters, List<string> arguments)
    //    {
    //        // Remove all quoted strings
    //        return ArgumentRegex.Replace(parameters, string.Empty).Trim();
    //    }

    //    /// <summary>
    //    /// Parses property and switches from the cleaned parameters
    //    /// </summary>
    //    private void ParsePropertyAndSwitches(RichFieldCode fieldCode, string parameters)
    //    {
    //        if (string.IsNullOrWhiteSpace(parameters))
    //            return;

    //        // Find the first switch marker
    //        var switchMatch = Regex.Match(parameters, @"\\([@#\*a-zA-Z])");

    //        if (switchMatch.Success)
    //        {
    //            // Property is everything before the first switch
    //            fieldCode.Property = parameters.Substring(0, switchMatch.Index).Trim();

    //            // Parse switches
    //            var switchesStr = parameters.Substring(switchMatch.Index);
    //            ParseSwitches(fieldCode, switchesStr);
    //        }
    //        else
    //        {
    //            // No switches, entire string is property
    //            fieldCode.Property = parameters.Trim();
    //        }
    //    }

    //    /// <summary>
    //    /// Parses switches Only and Return Result from parameters
    //    /// </summary>
    //    public IRichFieldSwitchCollection ParseSwitches(string parameters)
    //    {
    //        RichFieldSwitchCollection switches = new();
            
    //        if (string.IsNullOrWhiteSpace(parameters))
    //            return switches;

    //        // Find the first switch marker
    //        var switchMatch = Regex.Match(parameters, @"\\([@#\*a-zA-Z])");

    //        if (switchMatch.Success)
    //        {
    //            // Parse switches
    //            var switchesStr = parameters.Substring(switchMatch.Index);
    //            ParseSwitches(switches, switchesStr);
    //        }

    //        return switches;
    //    }

    //    /// <summary>
    //    /// Parses switches from a string
    //    /// </summary>
    //    private void ParseSwitches(RichFieldCode fieldCode, string switchesStr)
    //    {
    //        ParseSwitches(fieldCode.Switches, switchesStr);
    //    }

    //    /// <summary>
    //    /// Parses switches from a string
    //    /// </summary>
    //    private void ParseSwitches(IRichFieldSwitchCollection switches, string switchesStr)
    //    {
    //        var matches = SwitchRegex.Matches(switchesStr);

    //        foreach (Match match in matches)
    //        {
    //            var sw = new RichFieldSwitch
    //            {
    //                SwitchCode = match.Groups[1].Success ? match.Groups[1].Value : null,
    //                FormatType = match.Groups[1].Success
    //                    ? RichFieldSwitchsHelper.GetSwitchFormatType(match.Groups[1].Value)
    //                    : RichFormatSwitchType.None,
    //                SwitchValue = match.Groups[2].Success
    //                    ? match.Groups[2].Value.Trim().Trim('"')
    //                    : null,
    //            };

    //            switches.AddOrUpdate(sw);
    //        }
    //    }

    //    /// <summary>
    //    /// Parses special merge field properties
    //    /// </summary>
    //    private void ParseMergeFieldProperty(RichFieldCode fieldCode)
    //    {
    //        if (string.IsNullOrEmpty(fieldCode.Property))
    //            return;

    //        fieldCode.EnsureCollection(fieldCode.Property);
    //    }
    //    #endregion

    //    #region Specialized Parsers for Specific Field Types

    //    /// <summary>
    //    /// Parses INCLUDEPICTURE field with arguments
    //    /// Example: INCLUDEPICTURE "path" "QRCode" \w 100 \h 100
    //    /// </summary>
    //    public IncludePictureFieldOptions ParseIncludePictureCode(string fieldCode)
    //    {
    //        if (string.IsNullOrWhiteSpace(fieldCode))
    //            return null;

    //        var scanned = Scan(fieldCode);
    //        if (scanned == null || scanned.FieldType != RichFieldType.IncludePictureField)
    //            return null;

    //        var options = new IncludePictureFieldOptions
    //        {
    //            // First argument is the image path
    //            ImagePath = scanned.GetFirstArgument(),

    //            // Second argument could be barcode type or other options
    //            ImageOption = scanned.GetArgument(1)
    //        };

    //        // Extract dimensions from switches
    //        var widthSwitch = scanned.Switches.FirstOrDefault(s => s.SwitchCode == "\\w");
    //        if (widthSwitch != null && int.TryParse(widthSwitch.SwitchValue, out int width))
    //        {
    //            options.Width = width;
    //        }

    //        var heightSwitch = scanned.Switches.FirstOrDefault(s => s.SwitchCode == "\\h");
    //        if (heightSwitch != null && int.TryParse(heightSwitch.SwitchValue, out int height))
    //        {
    //            options.Height = height;
    //        }

    //        // Check for MERGEFORMAT
    //        options.PreserveFormatting = scanned.Switches.Any(s =>
    //            s.SwitchCode == "\\*" && s.SwitchValue?.Contains("MERGEFORMAT") == true);

    //        // Check for \d switch
    //        options.LinkOnly = scanned.Switches.Any(s => s.SwitchCode == "\\d");

    //        // Check if contains MERGEFIELD
    //        if (options.ImagePath != null)
    //        {
    //            options.HasMergeField = options.ImagePath.Contains("MERGEFIELD") ||
    //                                   options.ImagePath.Contains("«");

    //            if (options.HasMergeField)
    //            {
    //                options.MergeFieldName = ExtractMergeFieldName(options.ImagePath);
    //            }
    //        }

    //        return options;
    //    }

    //    /// <summary>
    //    /// Parses IF field with arguments
    //    /// Example: IF "condition" "true text" "false text"
    //    /// </summary>
    //    public IfFieldOptions ParseIfFieldCode(string fieldCode)
    //    {
    //        var scanned = Scan(fieldCode) as RichFieldCode;
    //        if (scanned == null || scanned.FieldType != RichFieldType.IfField)
    //            return null;

    //        return new IfFieldOptions
    //        {
    //            Expression = scanned.Property,
    //            TrueText = scanned.GetArgument(0),
    //            FalseText = scanned.GetArgument(1)
    //        };
    //    }

    //    /// <summary>
    //    /// Parses HYPERLINK field with arguments
    //    /// Example: HYPERLINK "url" "display text"
    //    /// </summary>
    //    public HyperlinkFieldOptions ParseHyperlinkFieldCode(string fieldCode)
    //    {
    //        var scanned = Scan(fieldCode) as RichFieldCode;
    //        if (scanned == null || scanned.FieldType != RichFieldType.HyperlinkField)
    //            return null;

    //        return new HyperlinkFieldOptions
    //        {
    //            Url = scanned.GetFirstArgument(),
    //            DisplayText = scanned.GetArgument(1)
    //        };
    //    }

    //    /// <summary>
    //    /// Parses DOCVARIABLE field with arguments
    //    /// Example: DOCVARIABLE "variableName" "defaultValue"
    //    /// </summary>
    //    public DocVariableFieldOptions ParseDocVariableFieldCode(string fieldCode)
    //    {
    //        var scanned = Scan(fieldCode) as RichFieldCode;
    //        if (scanned == null || scanned.FieldType != RichFieldType.DocVariableField)
    //            return null;

    //        return new DocVariableFieldOptions
    //        {
    //            VariableName = scanned.Property ?? scanned.GetFirstArgument(),
    //            DefaultValue = scanned.GetArgument(scanned.Property != null ? 0 : 1)
    //        };
    //    }

    //    /// <summary>
    //    /// Parses BARCODE field (custom field) with arguments
    //    /// Example: MERGEFIELD Barcode "QRCode" "ProductCode"
    //    /// </summary>
    //    public BarcodeFieldOptions ParseBarcodeFieldCode(string fieldCode)
    //    {
    //        var scanned = Scan(fieldCode) as RichFieldCode;
    //        if (scanned == null) return null;

    //        var options = new BarcodeFieldOptions
    //        {
    //            Value = scanned.GetArgument(1) ?? scanned.Property,
    //            Width = scanned.GetArgument<int>(2),
    //            Height = scanned.GetArgument<int>(3)
    //        };

    //        if (EnumerationHelper.TryParse<FieldBarcodeType>(scanned.GetArgument(0), out var barcodeType))
    //        {
    //            options.BarcodeType = barcodeType;
    //        }

    //        return options;
    //    }

    //    #endregion

    //    #region Utility Methods

    //    /// <summary>
    //    /// Extracts merge field name from a path
    //    /// </summary>
    //    public string ExtractMergeFieldName(string path)
    //    {
    //        if (string.IsNullOrWhiteSpace(path))
    //            return null;

    //        // Check for MERGEFIELD keyword
    //        var mergeMatch = Regex.Match(path, @"MERGEFIELD\s+(\w+)", RegexOptions.IgnoreCase);
    //        if (mergeMatch.Success)
    //        {
    //            return mergeMatch.Groups[1].Value;
    //        }

    //        // Check for field placeholder «FieldName»
    //        var placeholderMatch = Regex.Match(path, @"«([^»]+)»");
    //        if (placeholderMatch.Success)
    //        {
    //            return placeholderMatch.Groups[1].Value;
    //        }

    //        return null;
    //    }

    //    /// <summary>
    //    /// Replaces image path in INCLUDEPICTURE code
    //    /// </summary>
    //    public string ReplaceImagePath(string fieldCode, string newPath)
    //    {
    //        return Regex.Replace(fieldCode, @"""([^""]+)""", $"\"{newPath}\"");
    //    }

    //    /// <summary>
    //    /// Replaces static path with MERGEFIELD
    //    /// </summary>
    //    public string ReplacePathWithMergeField(string fieldCode, string mergeFieldName)
    //    {
    //        return Regex.Replace(fieldCode, @"""([^""]+)""", $"\"«{mergeFieldName}»\"");
    //    }

    //    /// <summary>
    //    /// Updates dimensions in INCLUDEPICTURE code
    //    /// </summary>
    //    public string UpdateImageDimensions(string fieldCode, int? width, int? height)
    //    {
    //        fieldCode = Regex.Replace(fieldCode, @"\\w\s+\d+", "");
    //        fieldCode = Regex.Replace(fieldCode, @"\\h\s+\d+", "");

    //        if (width.HasValue)
    //            fieldCode += $" \\w {width.Value}";

    //        if (height.HasValue)
    //            fieldCode += $" \\h {height.Value}";

    //        return fieldCode.Trim();
    //    }

    //    #endregion
    //}
}
