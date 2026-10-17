using ISoft.RichReportDesigner.Core.Fields.Data;
using System;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.Core.Data
{
    public interface IRichDataSource
    {
        object Data { get; }

        Type SourceType { get; }

        RichDataFieldGroup[] GetDataFields();

        Task<RichDataFieldGroup[]> GetDataFieldsAsync();
    }
}
