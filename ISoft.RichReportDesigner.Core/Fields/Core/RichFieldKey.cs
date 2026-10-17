using System;

namespace ISoft.RichReportDesigner.Core.Fields.Core
{
    public readonly struct RichFieldKey : IEquatable<RichFieldKey>
    {
        private readonly RichFieldInfo _fieldInfo;
        private readonly string _fieldProperty;

        public RichFieldInfo FieldInfo => _fieldInfo;

        public string FieldProperty => _fieldProperty;

        public RichFieldKey(RichFieldInfo fieldInfo, string fieldProperty = null)
        {
            _fieldInfo = fieldInfo;
            _fieldProperty = fieldProperty ?? string.Empty;
        }

        public static RichFieldKey CreateFromInfo(RichFieldInfo fieldInfo, string fieldProperty)
        {
            return new RichFieldKey(fieldInfo, fieldProperty);
        }

        public static RichFieldKey CreateFromType(RichFieldType fieldType, string fieldProperty)
        {
            return CreateFromInfo(RichFieldInfoProvider.GetRequiredFieldInfoByType(fieldType), fieldProperty);
        }

        public bool IsValid()
        {
            return _fieldInfo.IsValid() && (!_fieldInfo.SupportProperty || !string.IsNullOrEmpty(_fieldProperty));
        }

        public bool Equals(RichFieldKey other)
            => _fieldInfo == other._fieldInfo
            && (!_fieldInfo.SupportProperty || string.Equals(_fieldProperty, other._fieldProperty, StringComparison.OrdinalIgnoreCase));

        public override bool Equals(object obj)
            => obj is RichFieldKey other && Equals(other);

        public override int GetHashCode()
            => HashCode.Combine(_fieldInfo, _fieldProperty);
        
        public static bool operator ==(RichFieldKey left, RichFieldKey right)
            => left.Equals(right);

        public static bool operator !=(RichFieldKey left, RichFieldKey right)
            => !left.Equals(right);

        public override string ToString()
            => $"{_fieldInfo.FieldName}:{_fieldProperty}";

        public static bool IsNullOrEmpty(RichFieldKey? fieldKey)
            => !fieldKey.HasValue || !fieldKey.Value.IsValid();
    }

}
