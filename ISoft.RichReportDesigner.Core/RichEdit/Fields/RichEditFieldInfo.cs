using ISoft.RichReportDesigner.Core.Fields.Core;
using ISoft.RichReportDesigner.Core.Fields.Format;
using System;

namespace ISoft.RichReportDesigner.Core.RichEdit.Fields
{
    /// <summary>
    /// Represents the information of a calculated field
    /// </summary>
    public readonly struct RichEditFieldInfo
    {
        public static readonly RichEditFieldInfo Unknown = new(RichFieldType.Unknown, string.Empty, string.Empty, null);

        private readonly RichFieldType _fieldType;

        private readonly string _fieldName;
        private readonly string _fieldDisplayName;
        private readonly Type _fieldDataType;

        public RichFieldType FieldType => _fieldType;

        public string FieldName => _fieldName;

        public string FieldDisplayName => _fieldDisplayName;

        public Type FieldDataType => _fieldDataType;

        /// <summary>
        /// Default Format
        /// </summary>
        public RichFieldFormat? DefaultFormat => RichFieldFormatHelper.GetDefaultFormat(_fieldDataType);

        public RichEditFieldInfo(RichFieldType fieldType, string fieldName, string displayName, Type fieldDataType)
        {
            _fieldType = fieldType;
            _fieldName = fieldName;
            _fieldDisplayName = displayName;
            _fieldDataType = fieldDataType;
        }

        public override string ToString() => $"{FieldDisplayName} ({FieldName})";
    }
}
