using ISoft.Printing.Core.Data;
using ISoft.RichReportDesigner.Core.Data;
using ISoft.RichReportDesigner.Core.Templates.FileSystem;

namespace ISoft.RichReportDesigner.Core.Reports
{
    /// <summary>
    /// Factory for creating report instances
    /// </summary>
    public class RichReportFactory
    {
        public static IRichReport CreateReport(IRichReportContext context)
        {
            return new RichReport(context);
        }

        /// <summary>
        /// Creates a desktop report instance
        /// </summary>
        public static IRichReport CreateReport(RichReportContextDependencies dependencies)
        {
            var context = RichReportContextBuilder.Build(dependencies);

            return new RichReport(context);
        }

        public static IRichReport CreateReport(
            IReportInfo reportInfo,
            IRichDataSource dataSource,
            IRichReportTemplateStorage storageService,
            IRichDefaultTemplateInfoProvider defaultTemplateInfoProvider,
            IRichReportTemplateInfoProvider templateProvider,
            IRichReportPrintPermissionInfo permissionService,
            IRichReportProperties properties)
        {
            var context = RichReportContextBuilder
                            .Create(reportInfo, dataSource)
                            .WithTemplateStorage(storageService)
                            .WithPermissionsInfo(permissionService)
                            .WithProperties(properties)
                            .WithDefaultTemplateInfoProvider(defaultTemplateInfoProvider)
                            .WithTemplateInfoProvider(templateProvider)
                            .Build();

            return CreateReport(context);
        }

        public static IRichReport CreateReport(
            IReportInfo reportInfo,
            IRichDataSource dataSource,
            IRichReportTemplateService templateService,
            IRichReportTemplateInfoProvider templateProvider,
            IRichReportPrintPermissionInfo permissionService,
            IRichReportProperties properties,
            IRichTemplateRegistry registry)
        {
            var defaultTemplateProvider = new RichRegistryDefaultTemplateProvider(registry);

            return CreateReport(reportInfo,
                                dataSource,
                                new RichReportTemplateStorage(reportInfo, templateService),
                                defaultTemplateProvider,
                                templateProvider,
                                permissionService,
                                properties);
        }

        /// <summary>
        /// Creates a desktop report instance
        /// </summary>
        public static IRichReport CreateReport(
            IReportInfo reportInfo,
            IRichDataSource dataSource,
            IRichReportTemplateService templateService,
            IRichDefaultTemplateInfoProvider defaultTemplateInfoProvider,
            IRichReportTemplateInfoProvider templateProvider,
            IRichReportPrintPermissionInfo permissionService,
            IRichReportProperties properties)
        {
            return CreateReport(reportInfo,
                                dataSource,
                                new RichReportTemplateStorage(reportInfo, templateService),
                                defaultTemplateInfoProvider,
                                templateProvider,
                                permissionService,
                                properties);
        }
    }
}
