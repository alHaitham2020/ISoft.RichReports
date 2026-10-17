using System.ComponentModel.Design;

namespace ISoft.RichReportDesigner.Core.RichEdit
{
    public interface IRichServiceContainer
    {
        void CopyRequiredServices(IServiceContainer targetContainer);

        T GetService<T>() where T : class;

        T ReplaceService<T>(T newService) where T : class;

        bool TryGetService<T>(out T service) where T : class;

        bool HasService<T>() where T : class;

    }
}
