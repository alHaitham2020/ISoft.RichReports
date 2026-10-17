using System;

namespace ISoft.RichReportDesigner.Core.Reports.DataModel
{
    public interface IRichReportTemplatePreviewItem
    {
        Guid? Id { get; }

        string Name { get; }

        bool? IsShared { get; }

        DateTime? ModifiedDate { get; }

        byte[] Content { get;}

        Guid? OwnerId { get; }

        string OwnerName { get;}

        void ClearPreview();

        void RefreshPreview();
    }
}
