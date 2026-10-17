namespace ISoft.RichReportDesigner.Core.Constants
{
    /// <summary>
    /// Centralized error messages for calculated-field validation.
    /// Kept as constants so they can be reused across forms, view-models, and
    /// tests, and so localization can later swap this for a resource lookup
    /// without touching every call site.
    /// </summary>
    internal class RichFieldDisplayErrors
    {
        // --- Required fields ---
        public const string FieldNameRequired = "إسم الحقل مطلوب";
        public const string FieldDisplayNameRequired = "وصف الحقل مطلوب";
        public const string ExpressionRequired = "المعادلة مطلوبة";

        // --- Uniqueness ---
        public const string FieldNameDuplicate = "إسم الحقل مستخدم من سابق";
        public const string FieldDisplayNameDuplicate = "وصف الحقل مستخدم من سابق";
        public const string FieldDuplicateInDocument = "الحقل موجود من سابق";

        // --- Expression ---
        public const string ExpressionInvalid = "المعادلة غير صحيحة";

        // --- Data-member conflict ---
        // Raised when the columns referenced by the expression come from more than
        // one detail data source (i.e. two or more distinct DataMember values).
        public const string ExpressionDiffrentDataMembers =
            "لا يمكن استخدام حقول من مصادر بيانات مختلفة في نفس المعادلة";

        public const string ExpressionInvalidDataMember =
            "المعادلة تحتوي على حقول من مصدر بيانات غير موجود";

        public const string ExpressionInvalidColumn =
            "المعادلة تحتوي على حقل غير موجود";

        public const string ExpressionInvalidColumnType =
            "المعادلة تحتوي على حقل من نوع غير متوافق مع نوع الحقل المحسوب";

        public const string ExpressionInvalidColumnTypeForOperation =
            "المعادلة تحتوي على حقل من نوع غير متوافق مع العملية المستخدمة";

        public const string FieldMustBeInTable = "هذا الحقل يجب ان يكون ضمن جدول";

        public const string TableRowDiffrentDataMembers = "لا يمكن استخدام حقول من مصادر بيانات مختلفة في نفس الصف";
    }
}
