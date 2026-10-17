using ISoft.RichReportDesigner.Core.Fields.Core;
using ISoft.RichReportDesigner.Core.RichEdit.Fields;
using ISoft.Utils.Helpers;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;

namespace ISoft.RichReportDesigner.Core.RichEdit.Services.Implementation
{
    /// <summary>
    /// Provides comprehensive information about all rich edit fields with localization support
    /// </summary>
    public class RichEditFieldsManagerService : IRichEditFieldsManagerService
    {
        private readonly IRichEditFieldDisplayNameProvider _displayNameProvider;

        public RichEditFieldsManagerService() : this(new RichEditFieldDisplayNameProvider())
        {

        }

        public RichEditFieldsManagerService(IRichEditFieldDisplayNameProvider displayNameProvider)
        {
            _displayNameProvider = displayNameProvider;
        }

        #region RichEditFieldsManagerService Implemntation
        public string GetFieldDisplayName(RichFieldType fieldType)
        {
            return _displayNameProvider.GetDisplayName(fieldType);
        }

        /// <summary>
        /// Gets field information by field type
        /// </summary>
        /// <param name="fieldType">The field type</param>
        /// <returns>The field information</returns>
        public RichFieldInfo? GetFieldInfo(RichFieldType fieldType)
        {
            return RichFieldInfoProvider.GetFieldInfoByType(fieldType);
        }

        /// <summary>
        /// Tries to get field information by field type
        /// </summary>
        public bool TryGetFieldInfo(RichFieldType fieldType, out RichFieldInfo info)
        {
            return RichFieldInfoProvider.TryGetFieldInfoByType(fieldType, out info);
        }

        /// <summary>
        /// Gets field information by field name (case-insensitive)
        /// </summary>
        /// <param name="fieldName">The field name</param>
        /// <returns>The field information if found</returns>
        public RichFieldInfo? GetFieldInfoByName(string fieldName)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
                throw new ArgumentException("Field name cannot be null or empty", nameof(fieldName));

            return RichFieldInfoProvider.GetFieldInfoByName(fieldName);
        }

        /// <summary>
        /// Tries to get field information by field name
        /// </summary>
        public bool TryGetFieldInfoByName(string fieldName, out RichFieldInfo info)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
            {
                info = default;
                return false;
            }

            return RichFieldInfoProvider.TryGetFieldInfoByName(fieldName, out info);

        }

        /// <summary>
        /// Gets all field information
        /// </summary>
        /// <returns>Dictionary of all field information</returns>
        public IReadOnlyDictionary<RichFieldType, RichFieldInfo> GetAllFieldInfos()
        {
            return RichFieldInfoProvider.GetAllFields().ToFrozenDictionary(x => x.FieldType);
        }

        /// <summary>
        /// Gets all field names
        /// </summary>
        public IEnumerable<string> GetAllFieldNames()
        {
            return RichFieldInfoProvider.GetAllFieldsName();
        }

        /// <summary>
        /// Gets fields of a specific type
        /// </summary>
        /// <param name="type">The field value type</param>
        /// <returns>Fields that return the specified type</returns>
        public IEnumerable<RichFieldInfo> GetFieldsByType(Type type)
        {
            ArgumentGuardHelper.ArgumentNotNull(type);
            return RichFieldInfoProvider.GetAllFields().Where(info => info.Type == type);
        }

        /// <summary>
        /// Checks if a field type exists
        /// </summary>
        public bool HasFieldType(RichFieldType fieldType)
        {
            return RichFieldInfoProvider.IsSupportedFieldType(fieldType);
        }

        /// <summary>
        /// Checks if a field name exists
        /// </summary>
        public bool HasFieldName(string fieldName)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
                return false;

            return RichFieldInfoProvider.IsSupportedFieldType(fieldName);
        }

        /// <summary>
        /// Gets the field type by field name
        /// </summary>
        public RichFieldType? GetFieldTypeByName(string fieldName)
        {
            if (TryGetFieldInfoByName(fieldName, out var info))
                return info.FieldType;

            return null;
        }
        #endregion
    }
}
