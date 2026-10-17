using DevExpress.Xpf.Accordion;
using ISoft.RichReportDesigner.Core.Fields.Display;
using ISoft.RichReportDesigner.UI.Controls;
using ISoft.Utils.Extentions;
using ISoft.Utils.Helpers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ISoft.RichReportDesigner.UI.Controllers
{
    internal class RichDesignerDragDropManager
    {
        private readonly RichReportDesignerControl _designer;

        private RichReportDesignerFieldsPanel fieldsPanel;

        private XpfRichEditorControl editor;

        private const string FieldDropFormat = "RichFieldDropFormate"; // keep the string value!

        private Point _dragStartPoint;
        private IRichDisplayField _draggedField;

        public RichDesignerDragDropManager(RichReportDesignerControl designerControl)
        {
            _designer = designerControl;
        }

        public void Install(RichReportDesignerFieldsPanel fieldsPanel, XpfRichEditorControl editorControl)
        {
            this.fieldsPanel = fieldsPanel;
            this.editor = editorControl;

            Subscribe();
        }

        public void UnInstall()
        {
            if (fieldsPanel != null && editor != null)
            {
                Unsubscribe();
                fieldsPanel = null;
                editor = null;
            }
        }

        private void Subscribe()
        {
            fieldsPanel.DoIfNotNull(panel =>
            {
                panel.PreviewMouseLeftButtonDown += Accordion_PreviewMouseLeftButtonDown;
                panel.PreviewMouseMove += Accordion_PreviewMouseMove;
                panel.PreviewMouseDoubleClick += Accordion_PreviewMouseDoubleClick;
            });

            editor.DoIfNotNull(editor =>
            {
                // Set up event handlers
                editor.DragEnter += Editor_DragEnter;
                editor.DragOver += Editor_DragOver;
                editor.Drop += Editor_DragDrop;
            });
        }

        private void Unsubscribe()
        {
            fieldsPanel.DoIfNotNull(panel =>
            {
                // Set up event handlers
                panel.PreviewMouseLeftButtonDown -= Accordion_PreviewMouseLeftButtonDown;
                panel.PreviewMouseMove -= Accordion_PreviewMouseMove;
                panel.PreviewMouseDoubleClick -= Accordion_PreviewMouseDoubleClick;
            });

            editor.DoIfNotNull(editor =>
            {
                editor.DragEnter -= Editor_DragEnter;
                editor.DragOver -= Editor_DragOver;
                editor.Drop -= Editor_DragDrop;
            });
        }

        // ---------- Panel side (source) ----------

        private void Accordion_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_draggedField == null || e.LeftButton != MouseButtonState.Pressed)
                return;

            if (!DragDropHelper.ShouldStartDrag(_dragStartPoint, e.GetPosition(null)))
                return;

            var fieldToDrag = _draggedField;
            _draggedField = null;

            DragDrop.DoDragDrop(
                fieldsPanel,
                new DataObject(FieldDropFormat, fieldToDrag),
                DragDropEffects.Copy);
        }

        private void Accordion_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dragStartPoint = e.GetPosition(null);
            _draggedField = null;

            var item = FindAccordionItem(e.OriginalSource as DependencyObject);
            if (item?.DataContext is IRichDisplayField field)
                _draggedField = field;
        }

        private void Accordion_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = FindAccordionItem(e.OriginalSource as DependencyObject);

            if (item?.DataContext is not IRichDisplayField field)
                return;   // header / scrollbar / empty area → let the accordion handle it

            e.Handled = true;
            InsertDocumentField(field);
        }

        // ---------- Editor side (target) ----------

        private void Editor_DragEnter(object sender, DragEventArgs e)
        {
            editor.Focus();
        }

        private void Editor_DragOver(object sender, DragEventArgs e)
        {
            var point = e.GetPosition(editor);

            var position = editor.GetDropPosition(new System.Drawing.Point((int)point.X, (int)point.Y));
            if (position == null)
            {
                e.Effects = DragDropEffects.None;
                return;
            }

            editor.Document.CaretPosition = position;
            editor.ScrollToCaret();
            e.Effects = DragDropEffects.Copy;
            e.Handled = true;
        }

        private void Editor_DragDrop(object sender, DragEventArgs e)
        {
            e.Handled = true;

            if (e.Data.GetDataPresent(FieldDropFormat) && e.Data.GetData(FieldDropFormat) is IRichDisplayField field)
            {
                InsertDocumentField(field);
            }
        }

        private void InsertDocumentField(IRichDisplayField field)
        {
            editor.Focus();
            _designer.InsertField(field);
        }

        private static AccordionItem FindAccordionItem(DependencyObject source)
        {
            for (var node = source; node != null; node = VisualTreeHelper.GetParent(node))
            {
                if (node is ScrollViewer) return null;
                if (node is AccordionItem item) return item;
            }
            return null;
        }

        //private FrameworkElement CreateDragVisual(DataField field)
        //        {
        //            var border = new Border
        //            {
        //                Background = Brushes.LightBlue,
        //                BorderBrush = Brushes.DarkBlue,
        //                BorderThickness = new Thickness(1),
        //                Padding = new Thickness(8, 4, 8, 4),
        //                CornerRadius = new CornerRadius(3),
        //                Opacity = 0.8,
        //                Child = new TextBlock
        //                {
        //                    Text = field.DisplayName,
        //                    FontWeight = FontWeights.Bold,
        //                    Foreground = Brushes.DarkBlue
        //                }
        //            };

        //            return border;
        //        }
    }
}
