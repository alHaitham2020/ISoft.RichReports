using System;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.Core.Reports.DataModel
{
    public interface IRichReportCalculatedFieldModelOwner
    {
        bool IsDuplicateFieldName(Guid? id, string name);

        bool IsDuplicateFieldDisplayName(Guid? id, string name);

        Task SaveAsync(RichReportCalculatedFieldModel model);
    }
}
