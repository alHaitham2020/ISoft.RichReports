using ISoft.Utils.Data.AsyncData;
using System;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.Core.RichEdit.Services
{
    public interface IRichWaitIndicationService
    {
        Task BeginAsync(string state);

        Task UpdateAsync(string state);

        Task<AsyncExecutionResult> ShowWaitIndicatorAsync(Func<Task> asyncAction, string state = null, bool throwExceptions = true);

        Task EndAsync();
    }
}
