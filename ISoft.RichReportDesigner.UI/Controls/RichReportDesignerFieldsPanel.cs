using DevExpress.Mvvm.UI.Native;
using ISoft.RichReportDesigner.Core.Fields.Display;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ISoft.RichReportDesigner.UI.Controls
{
    public class RichReportDesignerFieldsPanel : Control
    {
        public static readonly DependencyProperty FieldsSourceProperty;

        static RichReportDesignerFieldsPanel()
        {
            DependencyPropertyRegistrator<RichReportDesignerFieldsPanel>
                .New()
                .OverrideDefaultStyleKey()
                .Register<IEnumerable<IRichDisplayFieldGroup>>(nameof(FieldsSource), out FieldsSourceProperty, null);
        }

        public IEnumerable<IRichDisplayFieldGroup> FieldsSource
        {
            get => (IEnumerable<IRichDisplayFieldGroup>)GetValue(FieldsSourceProperty);
            set => SetValue(FieldsSourceProperty, value);
        }
    }
}
