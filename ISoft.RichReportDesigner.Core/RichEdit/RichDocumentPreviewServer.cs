using DevExpress.Office.Services;
using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit;
using ISoft.RichReportDesigner.Core.MailMerge;
using ISoft.RichReportDesigner.Core.Printing;
using ISoft.RichReportDesigner.Core.Rendering;
using ISoft.RichReportDesigner.Core.RichEdit.Services;
using ISoft.RichReportDesigner.Core.Templates;
using ISoft.Utils.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ISoft.RichReportDesigner.Core.RichEdit
{
    /// <summary>
    /// Main template processor that coordinates all services
    /// </summary>
    public class RichDocumentPreviewServer : IRichDocumentPreviewServer
    {
        #region Fields
        private readonly IRichServiceContainer _serviceContainer;
        private readonly RichDocumentServer _sourceServer;
        private readonly RichDocumentServer _targetServer;

        private bool _isTemplateLoaded;
        private bool _isMailMergeExecuted;
        #endregion

        #region Constructor
        protected RichDocumentPreviewServer(IRichServiceContainer serviceContainer)
        {
            ArgumentGuardHelper.ArgumentNotNull(serviceContainer, nameof(serviceContainer));
            
            _serviceContainer = serviceContainer;
            _sourceServer = new RichDocumentServer();
            _targetServer = new RichDocumentServer();

            _isMailMergeExecuted = false;
            _isTemplateLoaded = false;
            RegisterServices();
            SubscribeEvents();
        }

        public static IRichDocumentPreviewServer Create(IRichServiceContainer serviceContainer) 
            => new RichDocumentPreviewServer(serviceContainer);

        #endregion

        #region Public Methods
        public async Task LoadTemplateAsync(object templateSource, DocumentFormat? format = null)
        {
            if (templateSource == null)
                throw new ArgumentNullException(nameof(templateSource));

            _isTemplateLoaded = false;
            try
            {
                var templateContent = ResolveTemplateContent(templateSource) ?? throw new ArgumentNullException(nameof(templateSource));
                format ??= DocumentFormat.Rtf;

                // Load template into server
                _isTemplateLoaded = await _sourceServer.LoadDocumentTemplateAsync(templateContent, format.Value);

                if (_isTemplateLoaded)
                {
                    UpdateDocumentProperties(templateSource as IRichReportTemplate);
                    UpdateCustomProprties();

                   await _serviceContainer
                            .GetService<IRichDocumentRenderingService>()
                            .RenderAsync(_sourceServer.Document);
                }
                else
                {
                    throw new InvalidOperationException("faild to load a template content");
                }
            }
            catch (Exception)
            {
                _isTemplateLoaded = false;
                throw;
            }
        }

        public async Task FillDataAsync(object dataSource, DevExpress.XtraRichEdit.API.Native.MailMergeOptions options = null)
        {
            EnsureTemplateLoaded();

            if (dataSource == null)
                throw new ArgumentNullException(nameof(dataSource));

            _isMailMergeExecuted = false;
            try
            {
                options ??= _sourceServer.CreateMailMergeOptions();
                options.DataSource = dataSource;

                // Execute mail merge using the service
               await _serviceContainer
                        .GetService<IMailMergeExecutionService>()
                        .ExecuteMailMergeAsync(_sourceServer, options, _targetServer);

                _isMailMergeExecuted = true;
            }
            catch (Exception)
            {
                _isMailMergeExecuted = false;
                throw;
            }
        }

        /// <summary>
        /// Saves the rendered template content to a byte array in the specified file format. 
        /// </summary>
        public byte[] SaveTemplate(DocumentFormat? format = null)
        {
            EnsureTemplateLoaded();

            format ??= DocumentFormat.Rtf;
            return _sourceServer.SaveDocument(format.Value);
        }

        /// <summary>
        /// Saves the rendered template content to a byte array in the specified file format. 
        /// </summary>
        public async Task<byte[]> SaveTemplateAsync(DocumentFormat? format = null)
        {
            EnsureTemplateLoaded();

            format ??= DocumentFormat.Rtf;
            return await _sourceServer.SaveDocumentAsync(format.Value);
        }

        /// <summary>
        /// Saves the rendered template content to a byte array in the specified file format. 
        /// </summary>
        public async Task SaveTemplateAsync(string fileName, DocumentFormat? format = null)
        {
            EnsureTemplateLoaded();

            format ??= DocumentFormat.Rtf;
            await _sourceServer.SaveDocumentAsync(fileName, format.Value);
        }

        /// <summary>
        /// Saves the rendered template content to a byte array in the specified file format. 
        /// </summary>
        public async Task SaveTemplateAsync(Stream stream, DocumentFormat? format = null)
        {
            EnsureTemplateLoaded();

            format ??= DocumentFormat.Rtf;
            await _sourceServer.SaveDocumentAsync(stream, format.Value);
        }

        /// <summary>
        /// Shows print preview
        /// </summary>
        public void ShowPrintPreview(string title = null)
        {
            EnsureTemplateLoaded();
            EnsureMailMergeExecuted();

            _serviceContainer
                .GetService<IRichPrintingService>()
                .ShowPrintPreview(_targetServer, title);
        }

        /// <summary>
        /// Shows print dialog
        /// </summary>
        public void ShowPrintDialog()
        {
            EnsureTemplateLoaded();
            EnsureMailMergeExecuted();

            _serviceContainer
                .GetService<IRichPrintingService>()
                .ShowPrintDialog(_targetServer);
        }

        public void Print()
        {
            EnsureTemplateLoaded();
            EnsureMailMergeExecuted();

            _serviceContainer
                .GetService<IRichPrintingService>()
                .Print(_targetServer);
        }

        public void Export(ExportFormat format, string defaultFileName = null)
        {
            EnsureTemplateLoaded();
            EnsureMailMergeExecuted();

            _serviceContainer
                .GetService<IRichPrintingService>()
                .Export(_targetServer, format, defaultFileName);
        }

        #endregion

        #region Private Methods
        private void RegisterServices()
        {
            _serviceContainer.CopyRequiredServices(_sourceServer);
            _serviceContainer.CopyRequiredServices(_targetServer);

            RegisterUriStreamProviders();
        }

        private void SubscribeEvents()
        {
            var mailMerageClientService = _serviceContainer.GetService<IRichMailMergeClientService>();

            if (mailMerageClientService != null)
            {
                _sourceServer.MailMergeStarted += (s, e) =>
                {
                    mailMerageClientService.Started(e);
                };

                _sourceServer.MailMergeFinished += (s, e) =>
                {
                    mailMerageClientService.Finished(e);
                };

                _sourceServer.MailMergeRecordStarted += (s, e) =>
                {
                    mailMerageClientService.RecordStarted(e);
                };

                _sourceServer.MailMergeRecordFinished += (s, e) =>
                {
                    mailMerageClientService.RecordFinished(e);
                };
            }

            var variableManagerService = _serviceContainer.GetService<IRichDocumentVariableManagerService>();

            if (variableManagerService != null)
            {
                _targetServer.CalculateDocumentVariable += (s, e) =>
                {
                    variableManagerService.Calculate(e);
                };
            }
        }

        private void EnsureTemplateLoaded()
        {
            if (!_isTemplateLoaded)
                throw new InvalidOperationException("Template is not loaded. Call LoadTemplate first.");
        }

        private void EnsureMailMergeExecuted()
        {
            if (!_isMailMergeExecuted)
                throw new InvalidOperationException("MailMerge is not executed. Call ExecuteMailMerge first.");
        }

        protected virtual void RegisterUriStreamProviders()
        {
            var streamService = _sourceServer.GetService<IUriStreamService>();
            if (streamService != null)
            {
                var registrationService = _sourceServer.GetService<IRichUriStreamRegistrationService>();
                if (registrationService != null)
                {
                    registrationService.RegisterUriStreamProviders(streamService);
                }
            }
        }

        protected virtual void UpdateCustomProprties()
        {
            var service = _serviceContainer.GetService<IRichDocumentCustomPropertiesManagerService>();
            if (service != null)
            {
                _sourceServer.Document.ExecuteOperation(d =>
                {
                    service.UpdateDocCustomProperties(d.CustomProperties);
                });
            }
        }

        protected virtual void UpdateDocumentProperties(IRichReportTemplate template)
        {
            var service = _serviceContainer.GetService<IRichDocumentPropertiesManagerService>();
            if (service != null)
            {
                _sourceServer.Document.ExecuteOperation(d =>
                {
                    service.UpdateDocPreviewProperties(d.DocumentProperties, template);
                });
            }
        }

        protected virtual byte[] ResolveTemplateContent(object templateSource)
        {
            if (templateSource is byte[] content)
            {
                return content;
            }
            else if (templateSource is IRichReportTemplate template)
            {
                return template.Content;
            }

            return null;
        }

        #endregion

        #region IDisposable
        public void Dispose()
        {
            _sourceServer?.Dispose();
            _targetServer?.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
