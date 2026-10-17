using DevExpress.XtraRichEdit.Fields;
using ISoft.RichReportDesigner.Core.RichEdit.Fields;
using ISoft.Utils.Helpers;
using System.Collections.Generic;
using System.Reflection;

namespace ISoft.RichReportDesigner.Core.RichEdit
{
    public static class RichEditInitializer
    {
        private static volatile bool _initialized;
        private static readonly object _lock = new();

        public static bool Initialized => _initialized;

        public static void TryInitialize()
        {
            if (!DesignModeHelper.IsInDesignMode() && !Initialized)
            {
                Initialize();
            }
        }

        public static void Initialize()
        {
            if (_initialized) return;

            lock (_lock)
            {
                if (_initialized) return;

                var fieldTypes = GetFieldTypes();
                if (fieldTypes is null) return;

                Register(fieldTypes, MergefieldField.FieldType, RichMergeField.Create);
                Register(fieldTypes, DateField.FieldType, RichDateField.Create);
                Register(fieldTypes, TimeField.FieldType, RichTimeField.Create);
                Register(fieldTypes, PrintDateField.FieldType, RichPrintDateField.Create);
                Register(fieldTypes, IncludepictureField.FieldType, RichIncludePictureField.Create);
                Register(fieldTypes, PageField.FieldType, RichPageField.Create);
                Register(fieldTypes, NumPagesField.FieldType, RichNumPagesField.Create);
                Register(fieldTypes, DocumentPropertyField.FieldType, RichDocumentPropertyField.Create);
                Register(fieldTypes, DocVariableField.FieldType, RichDocVariableField.Create);

                _initialized = true;
            }
        }

        private static Dictionary<string, CreateFieldDelegate> GetFieldTypes() =>
            typeof(FieldCalculatorService)
                .GetField("fieldTypes", BindingFlags.NonPublic | BindingFlags.Static)
                ?.GetValue(null) as Dictionary<string, CreateFieldDelegate>;

        private static void Register(
            Dictionary<string, CreateFieldDelegate> dic,
            string fieldType,
            CreateFieldDelegate creator) => dic[fieldType] = creator;
    }
}
