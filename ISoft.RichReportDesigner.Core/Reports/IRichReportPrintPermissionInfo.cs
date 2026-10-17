namespace ISoft.RichReportDesigner.Core.Reports
{
    public interface IRichReportPrintPermissionInfo
    {
        bool AllowCreateNewTemplate { get; }

        bool AllowModefiyTemplate { get; }

        bool AllowDeleteTemplate { get; }

        bool AllowImportTemplete { get; }

        bool AllowExportTemplete { get; }
    }
}
