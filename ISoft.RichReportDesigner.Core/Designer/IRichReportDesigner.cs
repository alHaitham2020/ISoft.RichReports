using ISoft.RichReportDesigner.Core.Fields.Display;
using ISoft.RichReportDesigner.Core.Reports;
using ISoft.RichReportDesigner.Core.RichEdit;
using System;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ISoft.RichReportDesigner.Core.Designer
{
    public interface IRichReportDesigner : IRichEditControlOwner
    {
        bool IsInDesignTime { get; }

        bool IsLoading { get;}

        bool IsReady { get; }

        IRichDisplayFieldGroupCollection DisplayFields { get; }

        IRichReport Report { get; }

        void ExecuteWithDispatcher(Action action, DispatcherPriority priority);

        Task ExecuteWithDispatcherAsync(Action action, DispatcherPriority priority);

        void HandleException(Exception ex);

        void OnCalculatedDisplayFieldChanged(string fieldName);

        void OnCalculatedDisplayFieldRemoved(string fieldName);

        bool CanRemoveCalculatedField(string fieldName);

        T GetService<T>() where T : class;
    }
}
