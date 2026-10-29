using ISoft.RichReportDesigner.Core.Fields.Display;
using ISoft.RichReportDesigner.Core.Reports;
using ISoft.RichReportDesigner.Core.RichEdit;
using System;

namespace ISoft.RichReportDesigner.Core.Designer
{
    public interface IRichReportDesigner : IRichEditorControlOwner
    {
        bool IsInDesignTime { get; }

        bool IsLoading { get;}

        bool IsReady { get; }

        IRichDisplayFieldGroupCollection DisplayFields { get; }

        IRichReport Report { get; }

        void HandleException(Exception ex);

        void OnCalculatedDisplayFieldChanged(string fieldName);

        void OnCalculatedDisplayFieldRemoved(string fieldName);

        bool CanRemoveCalculatedField(string fieldName);

        T GetService<T>() where T : class;
    }
}
