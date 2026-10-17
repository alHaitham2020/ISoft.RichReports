using DevExpress.Data;
using System;
using System.Collections.Generic;

namespace ISoft.RichReportDesigner.Core.Fields.Core
{
    //public static class RichCalculatedFieldTypeMapper
    //{
    //    public static Type ToClrType(this RichCalculatedFieldType? fieldType) => fieldType switch
    //    {
    //        null => typeof(object),
    //        RichCalculatedFieldType.None => typeof(object),
    //        RichCalculatedFieldType.String => typeof(string),
    //        RichCalculatedFieldType.DateTime => typeof(DateTime),
    //        RichCalculatedFieldType.Date => typeof(DateOnly),   // or DateTime
    //        RichCalculatedFieldType.Time => typeof(TimeOnly),   // or TimeSpan
    //        RichCalculatedFieldType.Int => typeof(int),
    //        RichCalculatedFieldType.Float => typeof(float),
    //        RichCalculatedFieldType.Double => typeof(double),
    //        RichCalculatedFieldType.Decimal => typeof(decimal),
    //        RichCalculatedFieldType.Boolean => typeof(bool),
    //        RichCalculatedFieldType.Guid => typeof(Guid),
    //        _ => throw new ArgumentOutOfRangeException(nameof(fieldType), fieldType, null)
    //    };

    //    public static RichCalculatedFieldType FromClrType(Type type)
    //    {
    //        type = Nullable.GetUnderlyingType(type) ?? type;

    //        if (type == typeof(string)) return RichCalculatedFieldType.String;
    //        if (type == typeof(DateTime)) return RichCalculatedFieldType.DateTime;
    //        if (type == typeof(DateOnly)) return RichCalculatedFieldType.Date;
    //        if (type == typeof(TimeOnly) || type == typeof(TimeSpan)) return RichCalculatedFieldType.Time;
    //        if (type == typeof(int)) return RichCalculatedFieldType.Int;
    //        if (type == typeof(float)) return RichCalculatedFieldType.Float;
    //        if (type == typeof(double)) return RichCalculatedFieldType.Double;
    //        if (type == typeof(decimal)) return RichCalculatedFieldType.Decimal;
    //        if (type == typeof(bool)) return RichCalculatedFieldType.Boolean;
    //        if (type == typeof(Guid)) return RichCalculatedFieldType.Guid;

    //        return RichCalculatedFieldType.None;
    //    }
    //}

    public static class RichCalculatedFieldTypeMapper
    {
        // ---------------------------------------------------------------------
        // Single source of truth: RichCalculatedFieldType -> .NET Type
        // Both ToClrType (public) and FromClrType (reverse) read from this,
        // so adding a new enum member only requires editing one place.
        // ---------------------------------------------------------------------
        private static readonly IReadOnlyDictionary<RichCalculatedFieldType, Type> ClrTypeMap =
            new Dictionary<RichCalculatedFieldType, Type>
            {
                [RichCalculatedFieldType.None] = typeof(object),
                [RichCalculatedFieldType.String] = typeof(string),
                [RichCalculatedFieldType.DateTime] = typeof(DateTime),
                [RichCalculatedFieldType.Date] = typeof(DateOnly),
                [RichCalculatedFieldType.Time] = typeof(TimeOnly),
                [RichCalculatedFieldType.Int] = typeof(int),
                [RichCalculatedFieldType.Float] = typeof(float),
                [RichCalculatedFieldType.Double] = typeof(double),
                [RichCalculatedFieldType.Decimal] = typeof(decimal),
                [RichCalculatedFieldType.Boolean] = typeof(bool),
                [RichCalculatedFieldType.Guid] = typeof(Guid),
            };

        // ---------------------------------------------------------------------
        // Single source of truth: RichCalculatedFieldType -> DevExpress UnboundColumnType.
        //
        // Notes on the mapping:
        //  - UnboundColumnType has no dedicated Guid / Float / Time slot, so they
        //    fall into the closest supported bucket (Object / Decimal / DateTime).
        //  - DateOnly / TimeOnly also don't have a slot; DateTime is the closest.
        //  - If DevExpress ever adds support for a new UnboundColumnType member,
        //    only this dictionary needs updating.
        // ---------------------------------------------------------------------
        private static readonly IReadOnlyDictionary<RichCalculatedFieldType, UnboundColumnType> UnboundTypeMap =
            new Dictionary<RichCalculatedFieldType, UnboundColumnType>
            {
                [RichCalculatedFieldType.None] = UnboundColumnType.Object,
                [RichCalculatedFieldType.String] = UnboundColumnType.String,
                [RichCalculatedFieldType.DateTime] = UnboundColumnType.DateTime,
                [RichCalculatedFieldType.Date] = UnboundColumnType.DateTime,   // no Date slot
                [RichCalculatedFieldType.Time] = UnboundColumnType.DateTime,   // no Time slot
                [RichCalculatedFieldType.Int] = UnboundColumnType.Integer,
                [RichCalculatedFieldType.Float] = UnboundColumnType.Decimal,    // no Float slot
                [RichCalculatedFieldType.Double] = UnboundColumnType.Decimal,    // no Double slot
                [RichCalculatedFieldType.Decimal] = UnboundColumnType.Decimal,
                [RichCalculatedFieldType.Boolean] = UnboundColumnType.Boolean,
                [RichCalculatedFieldType.Guid] = UnboundColumnType.Object,     // no Guid slot
            };

        // ---------------------------------------------------------------------
        // Public: RichCalculatedFieldType? -> DevExpress UnboundColumnType
        // Null / unknown values default to Object rather than throwing, so the
        // UI can always render *something* even if the stored type is invalid.
        // ---------------------------------------------------------------------
        public static UnboundColumnType ToUnboundType(this RichCalculatedFieldType? fieldType)
        {
            if (fieldType is null)
                return UnboundColumnType.Object;

            return UnboundTypeMap.TryGetValue(fieldType.Value, out var result)
                ? result
                : UnboundColumnType.Object;
        }

        // ---------------------------------------------------------------------
        // Public: RichCalculatedFieldType? -> .NET Type
        // Null / unknown values default to typeof(object) for the same reason.
        // ---------------------------------------------------------------------
        public static Type ToClrType(this RichCalculatedFieldType? fieldType)
        {
            if (fieldType is null)
                return typeof(object);

            return ClrTypeMap.TryGetValue(fieldType.Value, out var result)
                ? result
                : typeof(object);
        }

        // ---------------------------------------------------------------------
        // Public: .NET Type -> RichCalculatedFieldType
        // Unwraps Nullable<T> so typeof(int?) maps to Int, not None.
        // Unknown types return None (matches the original behavior).
        // ---------------------------------------------------------------------
        public static RichCalculatedFieldType FromClrType(Type type)
        {
            if (type is null)
                return RichCalculatedFieldType.None;

            type = Nullable.GetUnderlyingType(type) ?? type;

            // Reverse lookup against the same map used by ToClrType.
            foreach (var pair in ClrTypeMap)
            {
                if (pair.Value == type)
                    return pair.Key;
            }

            return RichCalculatedFieldType.None;
        }
    }
}
