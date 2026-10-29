using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit.Model;
using ISoft.Printing.Shared.Data;
using ISoft.RichReportDesigner.Core.Data;
using ISoft.RichReportDesigner.Core.Fields.Core;
using ISoft.RichReportDesigner.Core.Fields.Data;
using ISoft.RichReportDesigner.Core.MailMerge;
using ISoft.RichReportDesigner.Core.Printing;
using ISoft.RichReportDesigner.Core.Rendering;
using ISoft.RichReportDesigner.Core.RichEdit.Properties;
using ISoft.RichReportDesigner.Core.RichEdit.Services;
using ISoft.RichReportDesigner.Core.RichEdit.Services.Implementation;
using ISoft.RichReportDesigner.Core.Shared;
using ISoft.RichReportDesigner.Core.Templates;
using ISoft.Utils.Data;
using ISoft.Utils.DataModel;
using ISoft.Utils.Services;
using System;
using System.ComponentModel.Design;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.Core.Reports
{
    /// <summary>
    /// Base report strategy providing core printing functionality across all platforms
    /// </summary>
    public abstract class RichReportBase : ObservableObject, IServiceProvider, IServiceContainer, IRichReport, IDisposable
    {
        #region Fields
        private readonly IRichReportContext _context;

        private ServiceManager _serviceManager;
        private readonly RichReportPrintManager _printManager;
        private IRichDataFieldGroupCollection _dataFields;
        private RichCalculatedFieldProperties _calculatedFields;
        private bool disposedValue;
        #endregion

        #region Proprties
        protected IRichReportContext ReportContext => _context;

        public IRichDataFieldGroupCollection DataFields
        {
            get
            {
                if (_dataFields == null)
                {
                    _dataFields = new RichDataFieldGroupCollection();
                    _dataFields.LoadFromSource(_context.DataSource.GetDataFields());
                }

                return _dataFields!;
            }
        }

        public IRichCalculatedFieldProperties CalculatedFieldProperties
        {
            get
            {
                if (_calculatedFields == null)
                {
                    _calculatedFields = new RichCalculatedFieldProperties();

                    var fields = _context.CalculatedFieldStorage.GetCalculatedFields();
                    if (fields != null)
                    {
                        foreach (var field in fields)
                        {
                            _calculatedFields
                                .AddIfNotExists(new RichCalculatedFieldProperty(
                                    field.FieldName,
                                    field.DisplayName,
                                    field.FieldType.ToClrType(),
                                    field.DataMember,
                                    field.Expression));
                        }
                    }
                }

                return _calculatedFields!;
            }
        }


        IReportInfo IRichReport.ReportInfo => _context.ReportInfo;

        IRichReportDataSource IRichReport.DataSource => _context.DataSource;

        IRichReportTemplateStorage IRichReport.TemplateStorage => _context.TemplateStorage;

        IRichReportPrintPermissionInfo IRichReport.PermissionInfo => _context.PermissionInfo;

        IRichReportProperties IRichReport.Properties => _context.Properties;

        IRichDocumentProperties IRichReport.DocumentProperties => _context.DocumentProperties;

        IRichDocumentCustomProperties IRichReport.DocumentCustomProperties => _context.DocumentCustomProperties;

        IRichDocumentVariablesProperties IRichReport.DocumentVariablesProperties => _context.DocumentVariableProperties;

        IRichReportCalculatedFieldStorage IRichReport.CalculatedFieldStorage => _context.CalculatedFieldStorage;

        #endregion

        #region Constructors
        protected RichReportBase(IRichReportContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            _serviceManager = CreateServiceManager();
            _printManager = CreatePrintManager();
            
            OnInitializeInRuntime();
        }
        #endregion

        #region Public Actions
        public virtual void CopyRequiredServices(IServiceContainer targetContainer)
        {
            targetContainer.InheritService<IRichDocumentVariableManagerService>(this);
            targetContainer.InheritService<IRichDocumentPropertiesManagerService>(this);
            targetContainer.InheritService<IRichDocumentCustomPropertiesManagerService>(this);
            targetContainer.InheritService<IRichCalculatedFieldManagerService>(this);
            targetContainer.InheritService<IRichUriStreamRegistrationService>(this);

            targetContainer.InheritService<IRichEditFieldsManagerService>(this);

            targetContainer.InheritService<IRichDataFieldsProvider>(this);
            targetContainer.InheritService<IRichDataFieldsManagerService>(this);

            // devexpress builtin services
            targetContainer.InheritService<IFieldDataService>(this);
            targetContainer.InheritService<IMailMergeDataService>(this);
            targetContainer.InheritService<IFieldCalculatorService>(this);
        }

        public async Task ShowPrintPreviewAsync(IRichReportTemplate template)
        {
            await _printManager.ShowPrintPreviewAsync(template, _context.DataSource.Data, template.GetPreviewTitle());
        }

        public async Task ShowPrintDialogAsync(IRichReportTemplate template)
        {
            await _printManager.ShowPrintDialogAsync(template, _context.DataSource.Data);
        }

        public async Task PrintAsync(IRichReportTemplate template)
        {
            await _printManager.PrintAsync(template, _context.DataSource.Data);
        }

        public async Task ExportAsync(IRichReportTemplate template, ExportFormat format, string defaultFileName = null)
        {
            await _printManager.ExportAsync(template,
                                            _context.DataSource.Data,
                                            format,
                                            defaultFileName ?? _context.ReportInfo.Title);
        }

        public async Task PrintAsync(Guid? templateKey) =>
           await ExecutePrintOperationAsync(templateKey, ShowPrintDialogAsync);

        public async Task PrintDirectAsync(Guid? templateKey) =>
            await ExecutePrintOperationAsync(templateKey, PrintAsync);

        public async Task ShowPreviewAsync(Guid? templateKey) =>
            await ExecutePrintOperationAsync(templateKey, ShowPrintPreviewAsync);

        public async Task ExportAsync(Guid? templateKey, ExportFormat format, string defaultFileName = null)
        {
            await ExecutePrintOperationAsync(templateKey, (t) => ExportAsync(t, format, defaultFileName));
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~RichReportBase()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Propteced Actions
        protected virtual void OnInitializeInRuntime()
        {
            RegisterServices();
        }

        protected virtual void RegisterServices()
        {
            RegisterDataServices();
            RegisterPreviewServices();
        }

        protected virtual void RegisterDataServices()
        {
            // Data Services
            var dataFieldsProvider = new RichDataFieldsProvider(DataFields);
            var dataManagerService = new RichDataFieldsManagerService(dataFieldsProvider);
            var calculatedFieldManagerService = new RichCalculatedFieldManagerService(this);

            var mailMergeDataService = new RichMailMergeDataService(dataManagerService, calculatedFieldManagerService);

            AddService(typeof(IRichDataFieldsProvider), dataFieldsProvider);
            AddService(typeof(IRichDataFieldsManagerService), dataManagerService);

            // devexpress builtin services
            AddService(typeof(IFieldDataService), mailMergeDataService);
            AddService(typeof(IMailMergeDataService), mailMergeDataService);
            AddService(typeof(IFieldCalculatorService), new RichEditFieldCalculatorService());

            AddService(typeof(IRichEditFieldsManagerService), new RichEditFieldsManagerService());

            AddService(typeof(IRichDocumentPropertiesManagerService), new RichDefaultDocumentPropertiesManagerService(this));
            AddService(typeof(IRichDocumentCustomPropertiesManagerService), new RichDefaultDocumentCustomPropertiesManagerService(this));
            AddService(typeof(IRichDocumentVariableManagerService), new RichDefaultDocumentVariableManagerService(this));
            AddService(typeof(IRichCalculatedFieldManagerService), calculatedFieldManagerService);
        }

        protected virtual void RegisterPreviewServices()
        {
            // Preview Services
            var fieldManagerService = new RichFieldManagerService();

            AddService(typeof(IRichFieldManagerService), fieldManagerService);
            AddService(typeof(IRichDocumentRenderingService), new RichDocumentRenderingService(fieldManagerService));
            AddService(typeof(IMailMergeExecutionService), new MailMergeExecutionService());
            AddService(typeof(IRichPrintingService), new RichPrintingService());
            AddService(typeof(IRichUriStreamRegistrationService), new RichUriStreamProviderManagerService());
        }

        internal protected virtual ServiceManager CreateServiceManager()
        {
            return new ServiceManager();
        }

        internal protected virtual RichReportPrintManager CreatePrintManager()
        {
            return new RichReportPrintManager(this);
        }

        protected abstract bool HandelResult(DataValidationResult validationResult);

        protected async Task<DataValidationResult<IRichReportTemplate>> LoadTemplateAsync(Guid? templateKey)
        {
            var template = await _context.TemplateStorage.LoadAsync(templateKey);
            return template != null
                ? DataValidationResult<IRichReportTemplate>.AsSuccess(template)
                : DataValidationResult<IRichReportTemplate>.DisplayNotification("النمط المحدد غير موجود...");
        }

        protected internal virtual void DisposeCore()
        {
            if (_serviceManager != null)
            {
                _serviceManager.Dispose();
                _serviceManager = null;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DisposeCore();
                }
                disposedValue = true;
            }
        }

        #endregion

        #region Service Manager
        public void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
        {
            if (_serviceManager != null)
            {
                _serviceManager.AddService(serviceType, callback, promote);
            }
        }

        public void AddService(Type serviceType, ServiceCreatorCallback callback)
        {
            if (_serviceManager != null)
            {
                _serviceManager.AddService(serviceType, callback);
            }
        }

        public void AddService(Type serviceType, object serviceInstance, bool promote)
        {
            if (_serviceManager != null)
            {
                _serviceManager.AddService(serviceType, serviceInstance, promote);
            }
        }

        public void AddService(Type serviceType, object serviceInstance)
        {
            if (_serviceManager != null)
            {
                _serviceManager.AddService(serviceType, serviceInstance);
            }
        }

        public void RemoveService(Type serviceType, bool promote)
        {
            if (_serviceManager != null)
            {
                _serviceManager.RemoveService(serviceType, promote);
            }
        }

        public void RemoveService(Type serviceType)
        {
            if (_serviceManager != null)
            {
                _serviceManager.RemoveService(serviceType);
            }
        }

        public virtual object GetService(Type serviceType)
        {
            if (_serviceManager != null)
            {
                return _serviceManager.GetService(serviceType);
            }

            return null;
        }

        public virtual T GetService<T>() where T : class
        {
            return ServiceExtensions.GetService<T>(this);
        }

        public virtual bool HasService<T>() where T : class
        {
            return GetService<T>() != null;
        }

        public virtual T ReplaceService<T>(T newService) where T : class
        {
            return ServiceExtensions.ReplaceService(this, newService);
        }

        public bool TryGetService<T>(out T service) where T : class
        {
            service = GetService<T>();
            return service != null;
        }

        public void PerformWithServiceIfNotNull<T>(Action<T> callback) where T : class
        {
            ServiceProviderExtensions.PerformIfNotNull(this, callback);
        }

        #endregion

        #region CalculatedFields
        void IRichReport.OnCalculatedFieldChanged(string fieldName)
        {
            if (_calculatedFields != null)
            {
                var field = _context.CalculatedFieldStorage.FindByName(fieldName);
                if (field != null)
                {
                    _calculatedFields
                        .AddOrUpdate(new RichCalculatedFieldProperty(
                            field.FieldName,
                            field.DisplayName,
                            field.FieldType.ToClrType(),
                            field.DataMember,
                            field.Expression));
                }
            }
        }

        void IRichReport.OnCalculatedFieldRemoved(string fieldName)
        {
            if (_calculatedFields != null)
            {
                _calculatedFields.RemoveByKey(fieldName);
            }
        }

        #endregion

        #region Helper
        private async Task ExecutePrintOperationAsync(Guid? templateKey, Func<IRichReportTemplate, Task> operation)
        {
            try
            {
                var result = await LoadTemplateAsync(templateKey);
                if (!HandelResult(result)) return;

                await operation(result.Value);
            }
            catch (Exception ex)
            {
                throw new RichOperationException("Failed to execute print operation", ex);
            }
        }

        #endregion
    }
}
