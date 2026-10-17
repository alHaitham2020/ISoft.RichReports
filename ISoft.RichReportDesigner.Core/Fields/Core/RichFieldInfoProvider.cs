using DevExpress.XtraRichEdit.Fields;
using ISoft.RichReportDesigner.Core.RichEdit.Fields;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;

namespace ISoft.RichReportDesigner.Core.Fields.Core
{
    //public static class RichFieldInfoProvider
    //{
    //    // convert to ConcurrentDictionary<string, RichFieldInfo> where the key is [field].FieldType
    //    private static readonly ConcurrentDictionary<RichCalculatedFieldType, RichFieldInfo> FieldsCache =
    //       new()
    //       {
    //           [RichCalculatedFieldType.MergeField] = RichFieldInfo.StringField(RichCalculatedFieldType.MergeField, MergefieldField.FieldType, RichFieldCategory.DataFields),

    //           [RichCalculatedFieldType.IfField] = RichFieldInfo.BoolField(RichCalculatedFieldType.IfField, IfField.FieldType, RichFieldCategory.EquationAndFormula),
    //           [RichCalculatedFieldType.SymbolField] = RichFieldInfo.StringField(RichCalculatedFieldType.SymbolField, SymbolField.FieldType, RichFieldCategory.EquationAndFormula),
    //           [RichCalculatedFieldType.CheckboxField] = RichFieldInfo.BoolField(RichCalculatedFieldType.CheckboxField, CheckBoxField.FieldType, RichFieldCategory.EquationAndFormula),

    //           [RichCalculatedFieldType.IndexEntryField] = RichFieldInfo.StringField(RichCalculatedFieldType.IndexEntryField, IndexEntryField.FieldType, RichFieldCategory.IndexAndTables),
    //           [RichCalculatedFieldType.TocField] = RichFieldInfo.StringField(RichCalculatedFieldType.TocField, TocField.FieldType, RichFieldCategory.IndexAndTables),
    //           [RichCalculatedFieldType.TocEntryField] = RichFieldInfo.StringField(RichCalculatedFieldType.TocEntryField, TocEntryField.FieldType, RichFieldCategory.IndexAndTables),

    //           [RichCalculatedFieldType.HyperlinkField] = RichFieldInfo.StringField(RichCalculatedFieldType.HyperlinkField, HyperlinkField.FieldType, RichFieldCategory.LinksAndReferences),
    //           [RichCalculatedFieldType.IncludePictureField] = RichFieldInfo.StringField(RichCalculatedFieldType.IncludePictureField, IncludepictureField.FieldType, RichFieldCategory.LinksAndReferences),
    //           [RichCalculatedFieldType.RefField] = RichFieldInfo.StringField(RichCalculatedFieldType.RefField, RefField.FieldType, RichFieldCategory.LinksAndReferences),
    //           [RichCalculatedFieldType.PageRefField] = RichFieldInfo.StringField(RichCalculatedFieldType.PageRefField, PageRefField.FieldType, RichFieldCategory.LinksAndReferences),
    //           [RichCalculatedFieldType.StyleRefField] = RichFieldInfo.StringField(RichCalculatedFieldType.StyleRefField, StyleRefField.FieldType, RichFieldCategory.LinksAndReferences),

    //           [RichCalculatedFieldType.PageField] = RichFieldInfo.IntField(RichCalculatedFieldType.PageField, PageField.FieldType, RichFieldCategory.Numbering),
    //           [RichCalculatedFieldType.NumPagesField] = RichFieldInfo.IntField(RichCalculatedFieldType.NumPagesField, NumPagesField.FieldType, RichFieldCategory.Numbering),
    //           [RichCalculatedFieldType.SeqField] = RichFieldInfo.IntField(RichCalculatedFieldType.SeqField, SequenceField.FieldType, RichFieldCategory.Numbering),
    //           [RichCalculatedFieldType.SectionPagesField] = RichFieldInfo.IntField(RichCalculatedFieldType.SectionPagesField, SectionPageField.FieldType, RichFieldCategory.DocumentProperties),

    //           [RichCalculatedFieldType.DateField] = RichFieldInfo.DateOnlyField(RichCalculatedFieldType.DateField, DateField.FieldType, RichFieldCategory.DateAndTime),
    //           [RichCalculatedFieldType.TimeField] = RichFieldInfo.TimeOnlyField(RichCalculatedFieldType.TimeField, TimeField.FieldType, RichFieldCategory.DateAndTime),

    //           [RichCalculatedFieldType.DocVariableField] = RichFieldInfo.StringField(RichCalculatedFieldType.DocVariableField, DocVariableField.FieldType, RichFieldCategory.DocumentVariables),
    //           [RichCalculatedFieldType.CustomPropertyField] = RichFieldInfo.StringField(RichCalculatedFieldType.CustomPropertyField, RichCustomPropertyField.FieldType, RichFieldCategory.CustomProperties),

    //           [RichCalculatedFieldType.DocPropertyField] = RichFieldInfo.StringField(RichCalculatedFieldType.DocPropertyField, DocumentPropertyField.FieldType, RichFieldCategory.DocumentProperties),
    //           [RichCalculatedFieldType.InfoField] = RichFieldInfo.StringField(RichCalculatedFieldType.InfoField, InfoField.FieldType, RichFieldCategory.DocumentProperties),
    //           [RichCalculatedFieldType.AuthorField] = RichFieldInfo.StringField(RichCalculatedFieldType.AuthorField, AuthorField.FieldType, RichFieldCategory.DocumentProperties),
    //           [RichCalculatedFieldType.CommentsField] = RichFieldInfo.StringField(RichCalculatedFieldType.CommentsField, CommentsField.FieldType, RichFieldCategory.DocumentProperties),
    //           [RichCalculatedFieldType.CreateDateField] = RichFieldInfo.DateTimeField(RichCalculatedFieldType.CreateDateField, CreatedateField.FieldType, RichFieldCategory.DocumentProperties),
    //           [RichCalculatedFieldType.SaveDateField] = RichFieldInfo.DateTimeField(RichCalculatedFieldType.SaveDateField, SaveDateField.FieldType, RichFieldCategory.DocumentProperties),
    //           [RichCalculatedFieldType.PrintDateField] = RichFieldInfo.DateTimeField(RichCalculatedFieldType.PrintDateField, PrintDateField.FieldType, RichFieldCategory.DocumentProperties),
    //           [RichCalculatedFieldType.KeywordsField] = RichFieldInfo.StringField(RichCalculatedFieldType.KeywordsField, KeywordsField.FieldType, RichFieldCategory.DocumentProperties),
    //           [RichCalculatedFieldType.LastSavedByField] = RichFieldInfo.StringField(RichCalculatedFieldType.LastSavedByField, LastSavedByField.FieldType, RichFieldCategory.DocumentProperties),
    //           [RichCalculatedFieldType.RevNumField] = RichFieldInfo.IntField(RichCalculatedFieldType.RevNumField, ReviewNumberField.FieldType, RichFieldCategory.DocumentProperties),
    //           [RichCalculatedFieldType.SubjectField] = RichFieldInfo.StringField(RichCalculatedFieldType.SubjectField, SubjectField.FieldType, RichFieldCategory.DocumentProperties),
    //           [RichCalculatedFieldType.TitleField] = RichFieldInfo.StringField(RichCalculatedFieldType.TitleField, TitleField.FieldType, RichFieldCategory.DocumentProperties),
    //       };

    //    // change it also oposit
    //    private static readonly ConcurrentDictionary<string, RichCalculatedFieldType> ReverseCache;

    //    static RichFieldInfoProvider()
    //    {
    //        // Build reverse lookup dictionary once
    //        var reverse = new ConcurrentDictionary<string, RichCalculatedFieldType>(StringComparer.OrdinalIgnoreCase);
    //        foreach (var kvp in FieldsCache)
    //        {
    //            reverse[kvp.Value.FieldName] = kvp.Key;
    //        }

    //        ReverseCache = reverse;
    //    }

    //    public static IEnumerable<RichFieldInfo> GetAllFields()
    //    {
    //        return FieldsCache.Values;
    //    }

    //    public static string[] GetAllFieldsName()
    //    {
    //        return FieldsCache.Values.Select(x => x.FieldName).ToArray();
    //    }

    //    /// <summary>
    //    /// Gets the string representation of the field type
    //    /// </summary>
    //    public static RichFieldInfo? GetFieldInfo(RichCalculatedFieldType fieldType)
    //    {
    //        return FieldsCache.TryGetValue(fieldType, out RichFieldInfo value) ? value : null;
    //    }

    //    public static RichFieldInfo GetFieldInfoOrEmpty(RichCalculatedFieldType fieldType)
    //    {
    //        if (fieldType == RichCalculatedFieldType.Unknown) return RichFieldInfo.Unknown;

    //        return GetFieldInfo(fieldType) ?? RichFieldInfo.Unknown;
    //    }

    //    /// <summary>
    //    /// Gets the string representation of the field type
    //    /// </summary>
    //    public static string GetFieldName(RichCalculatedFieldType fieldType)
    //    {
    //        if(FieldsCache.TryGetValue(fieldType, out RichFieldInfo value))
    //        {
    //            return value.FieldName;
    //        }

    //        return null;
    //    }

    //    /// <summary>
    //    /// Determines the field type from the field name prefix (O(1) lookup)
    //    /// </summary>
    //    public static RichCalculatedFieldType GetFieldType(string fieldName)
    //    {
    //        if (string.IsNullOrEmpty(fieldName))
    //            return RichCalculatedFieldType.Unknown;

    //        fieldName = fieldName.Trim();

    //        // Try exact match first (fastest)
    //        if (ReverseCache.TryGetValue(fieldName, out var fieldType))
    //            return fieldType;

    //        return RichCalculatedFieldType.Unknown;
    //    }

    //    //public static RichFieldType GetRichFieldType(string fieldName)
    //    //{
    //    //    KeyValuePair<RichFieldType, RichFieldInfo>? entry = FieldsCache?.FirstOrDefault(delegate (KeyValuePair<RichFieldType, RichFieldInfo> pair)
    //    //    {
    //    //        if (pair.Value.FieldName.Equals(fieldName, StringComparison.OrdinalIgnoreCase))
    //    //            return true;
    //    //        else
    //    //            return false;
    //    //    });

    //    //    return entry.HasValue ? entry.Value.Key : RichFieldType.Unknown;
    //    //}
    //}

    public static class RichFieldInfoProvider
    {
        // Single source of truth: the list of all field definitions
        private static readonly RichFieldInfo[] AllFields = [

            RichFieldInfo.StringField(RichFieldType.MergeField, MergefieldField.FieldType, RichFieldCategory.DataFields,true),

            RichFieldInfo.BoolField(RichFieldType.IfField, IfField.FieldType, RichFieldCategory.EquationAndFormula,true),
            RichFieldInfo.StringField(RichFieldType.SymbolField, SymbolField.FieldType, RichFieldCategory.EquationAndFormula,true),
            RichFieldInfo.BoolField(RichFieldType.CheckboxField, CheckBoxField.FieldType, RichFieldCategory.EquationAndFormula),
            RichFieldInfo.StringField(RichFieldType.ExpEditCalculatedField, RichExpressionEditorCalculatedField.FieldType, RichFieldCategory.EquationAndFormula,true),

            RichFieldInfo.StringField(RichFieldType.IndexEntryField, IndexEntryField.FieldType, RichFieldCategory.IndexAndTables,true),
            RichFieldInfo.StringField(RichFieldType.TocField, TocField.FieldType, RichFieldCategory.IndexAndTables,true),
            RichFieldInfo.StringField(RichFieldType.TocEntryField, TocEntryField.FieldType, RichFieldCategory.IndexAndTables,true),

            RichFieldInfo.StringField(RichFieldType.HyperlinkField, HyperlinkField.FieldType, RichFieldCategory.LinksAndReferences,true),
            RichFieldInfo.StringField(RichFieldType.IncludePictureField, IncludepictureField.FieldType, RichFieldCategory.LinksAndReferences,true),
            RichFieldInfo.StringField(RichFieldType.RefField, RefField.FieldType, RichFieldCategory.LinksAndReferences,true),
            RichFieldInfo.StringField(RichFieldType.PageRefField, PageRefField.FieldType, RichFieldCategory.LinksAndReferences,true),
            RichFieldInfo.StringField(RichFieldType.StyleRefField, StyleRefField.FieldType, RichFieldCategory.LinksAndReferences,true),

            RichFieldInfo.IntField(RichFieldType.PageField, PageField.FieldType, RichFieldCategory.Numbering),
            RichFieldInfo.IntField(RichFieldType.NumPagesField, NumPagesField.FieldType, RichFieldCategory.Numbering),
            RichFieldInfo.IntField(RichFieldType.SeqField, SequenceField.FieldType, RichFieldCategory.Numbering),
            RichFieldInfo.IntField(RichFieldType.SectionPagesField, SectionPageField.FieldType, RichFieldCategory.DocumentProperties),

            RichFieldInfo.DateOnlyField(RichFieldType.DateField, DateField.FieldType, RichFieldCategory.DateAndTime),
            RichFieldInfo.TimeOnlyField(RichFieldType.TimeField, TimeField.FieldType, RichFieldCategory.DateAndTime),

            RichFieldInfo.StringField(RichFieldType.DocVariableField, DocVariableField.FieldType, RichFieldCategory.DocumentVariables,true),
            RichFieldInfo.StringField(RichFieldType.CustomPropertyField, RichCustomPropertyField.FieldType, RichFieldCategory.CustomProperties,true),

            RichFieldInfo.StringField(RichFieldType.DocPropertyField, DocumentPropertyField.FieldType, RichFieldCategory.DocumentProperties,true),
            RichFieldInfo.StringField(RichFieldType.InfoField, InfoField.FieldType, RichFieldCategory.DocumentProperties,true),
            RichFieldInfo.StringField(RichFieldType.AuthorField, AuthorField.FieldType, RichFieldCategory.DocumentProperties),
            RichFieldInfo.StringField(RichFieldType.CommentsField, CommentsField.FieldType, RichFieldCategory.DocumentProperties),
            RichFieldInfo.DateTimeField(RichFieldType.CreateDateField, CreatedateField.FieldType, RichFieldCategory.DocumentProperties),
            RichFieldInfo.DateTimeField(RichFieldType.SaveDateField, SaveDateField.FieldType, RichFieldCategory.DocumentProperties),
            RichFieldInfo.DateTimeField(RichFieldType.PrintDateField, PrintDateField.FieldType, RichFieldCategory.DocumentProperties),
            RichFieldInfo.StringField(RichFieldType.KeywordsField, KeywordsField.FieldType, RichFieldCategory.DocumentProperties),
            RichFieldInfo.StringField(RichFieldType.LastSavedByField, LastSavedByField.FieldType, RichFieldCategory.DocumentProperties),
            RichFieldInfo.IntField(RichFieldType.RevNumField, ReviewNumberField.FieldType, RichFieldCategory.DocumentProperties),
            RichFieldInfo.StringField(RichFieldType.SubjectField, SubjectField.FieldType, RichFieldCategory.DocumentProperties),
            RichFieldInfo.StringField(RichFieldType.TitleField, TitleField.FieldType, RichFieldCategory.DocumentProperties),
       ];

        // ---- Lookup caches (immutable after static ctor) ---------------------
        private static readonly FrozenDictionary<string, RichFieldInfo> FieldsByName;
        private static readonly FrozenDictionary<RichFieldType, RichFieldInfo> FieldsByType;
        private static readonly string[] AllFieldNames;

        static RichFieldInfoProvider()
        {
            // Allocation-free array transformation compared to Linq Select()
            AllFieldNames = Array.ConvertAll(AllFields, f => f.FieldName);

            FieldsByName = AllFields.ToFrozenDictionary(f => f.FieldName, StringComparer.OrdinalIgnoreCase);
            FieldsByType = AllFields.ToFrozenDictionary(f => f.FieldType);
        }

        // ---- Read-only accessors ---------------------------------------------
        public static IReadOnlyList<RichFieldInfo> GetAllFields() => AllFields;

        public static IReadOnlyList<string> GetAllFieldsName() => AllFieldNames;

        // ---- Name → info ------------------------------------------------------
        public static RichFieldInfo? GetFieldInfoByName(string fieldName)
        {
            if (string.IsNullOrWhiteSpace(fieldName)) return null;

            return FieldsByName.TryGetValue(fieldName.Trim(), out var fieldInfo) ? fieldInfo : null;
        }

        public static RichFieldInfo GetFieldInfoOrEmpty(string fieldName)
            => GetFieldInfoByName(fieldName) ?? RichFieldInfo.Unknown;

        // ---- Type → info ------------------------------------------------------
        public static RichFieldInfo? GetFieldInfoByType(RichFieldType fieldType)
        {
            if (fieldType == RichFieldType.Unknown) return null;

            return FieldsByType.TryGetValue(fieldType, out var fieldInfo) ? fieldInfo : null;
        }

        public static bool TryGetFieldInfoByType(RichFieldType fieldType,out RichFieldInfo fieldInfo)
        {
            if (fieldType == RichFieldType.Unknown)
            {
                fieldInfo = RichFieldInfo.Unknown;
                return false;
            }

            return FieldsByType.TryGetValue(fieldType, out fieldInfo);
        }

        public static bool TryGetFieldInfoByName(string fieldName, out RichFieldInfo fieldInfo)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
            {
                fieldInfo = RichFieldInfo.Unknown;
                return false;
            }

            return FieldsByName.TryGetValue(fieldName.Trim(), out fieldInfo);
        }

        public static RichFieldInfo GetFieldInfoByTypeOrEmpty(RichFieldType fieldType)
            => GetFieldInfoByType(fieldType) ?? RichFieldInfo.Unknown;

        public static RichFieldInfo GetRequiredFieldInfoByType(RichFieldType fieldType)
           => GetFieldInfoByType(fieldType) ?? throw new ArgumentNullException(nameof(fieldType));

        // ---- Type → name ------------------------------------------------------
        public static string GetFieldName(RichFieldType fieldType)
            => GetFieldInfoByType(fieldType)?.FieldName;

        public static string GetFieldNameOrEmpty(RichFieldType fieldType)
            => GetFieldInfoByTypeOrEmpty(fieldType).FieldName;

        /// <summary>
        /// Validates if a field type string is in the supported types collection.
        /// </summary>
        public static bool IsSupportedFieldType(string fieldType)
        {
            return !string.IsNullOrEmpty(fieldType) &&
                   AllFieldNames.Contains(fieldType);
        }

        /// <summary>
        /// Validates if a field type string is in the supported types collection.
        /// </summary>
        public static bool IsSupportedFieldType(RichFieldType fieldType)
        {
            return fieldType!= RichFieldType.Unknown &&
                   FieldsByType.ContainsKey(fieldType);
        }
    }
}
