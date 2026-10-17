using ISoft.RichReportDesigner.Core.Fields.Format;
using System;

namespace ISoft.RichReportDesigner.Core.Fields.Core
{
    public readonly struct RichFieldInfo : IEquatable<RichFieldInfo>
    {
        public static readonly RichFieldInfo Unknown = new(RichFieldType.Unknown, string.Empty, null, RichFieldCategory.None, false);

        private readonly RichFieldType _fieldType;
        private readonly RichFieldCategory _category;
        private readonly string _fieldName;
        private readonly Type _type;
        private readonly bool _supportProperty;

        public RichFieldCategory Category => _category;

        public RichFieldType FieldType => _fieldType;

        public string FieldName => _fieldName;

        /// <summary>
        /// the calculated value type of field
        /// </summary>
        public Type Type => _type;

        /// <summary>
        /// if field has a fieldProperty (first argument)
        /// </summary>
        public bool SupportProperty => _supportProperty;

        /// <summary>
        /// Default Format
        /// </summary>
        public RichFieldFormat? DefaultFormat => RichFieldFormatHelper.GetDefaultFormat(_type);

        public bool IsValid() =>
            _fieldType != RichFieldType.Unknown &&
            !string.IsNullOrWhiteSpace(_fieldName);

        public RichFieldInfo(
            RichFieldType fieldType,
            string fieldName,
            Type type,
            RichFieldCategory category,
            bool? supportProperty = null)
        {
            _fieldType = fieldType;
            _fieldName = fieldName ?? string.Empty;
            _type = type;
            _category = category;
            _supportProperty = supportProperty ?? false;
        }

        #region Factory methods
        public static RichFieldInfo StringField(RichFieldType fieldType, string fieldName, RichFieldCategory category, bool? supportProperty = null) =>
            new(fieldType, fieldName, typeof(string), category, supportProperty);

        public static RichFieldInfo DateTimeField(RichFieldType fieldType, string fieldName, RichFieldCategory category, bool? supportProperty = null) =>
            new(fieldType, fieldName, typeof(DateTime), category, supportProperty);

        public static RichFieldInfo DateOnlyField(RichFieldType fieldType, string fieldName, RichFieldCategory category, bool? supportProperty = null) =>
            new(fieldType, fieldName, typeof(DateOnly), category, supportProperty);

        public static RichFieldInfo TimeOnlyField(RichFieldType fieldType, string fieldName, RichFieldCategory category, bool? supportProperty = null) =>
            new(fieldType, fieldName, typeof(TimeOnly), category, supportProperty);

        public static RichFieldInfo IntField(RichFieldType fieldType, string fieldName, RichFieldCategory category, bool? supportProperty = null) =>
            new(fieldType, fieldName, typeof(int), category, supportProperty);

        public static RichFieldInfo BoolField(RichFieldType fieldType, string fieldName, RichFieldCategory category, bool? supportProperty = null) =>
            new(fieldType, fieldName, typeof(bool), category, supportProperty);
        #endregion

        /// <summary>
        /// Checks if a nullable RichFieldInfo is null or invalid.
        /// </summary>
        public static bool IsNullOrEmpty(RichFieldInfo? field) =>
            !field.HasValue || !field.Value.IsValid();

        #region IEquatable<RichFieldInfo> implementation
        public bool Equals(RichFieldInfo other) =>
            _fieldType == other._fieldType &&
            _category == other._category &&
            string.Equals(_fieldName, other._fieldName, StringComparison.OrdinalIgnoreCase) &&
            _type == other._type &&
            _supportProperty == other._supportProperty;

        public override bool Equals(object obj) =>
            obj is RichFieldInfo other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + _fieldType.GetHashCode();
                hash = hash * 31 + _category.GetHashCode();
                hash = hash * 31 + (_fieldName?.GetHashCode() ?? 0);
                hash = hash * 31 + (_type?.GetHashCode() ?? 0);
                return hash;
            }
        }

        public static bool operator ==(RichFieldInfo left, RichFieldInfo right) => left.Equals(right);

        public static bool operator !=(RichFieldInfo left, RichFieldInfo right) => !left.Equals(right);

        public override string ToString() => _fieldName;

        #endregion
    }
}
