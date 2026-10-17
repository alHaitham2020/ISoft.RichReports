using ISoft.RichReportDesigner.Core.RichEdit.Services;
using ISoft.RichReportDesigner.Core.RichEdit.Services.Implementation;
using ISoft.Utils.Data;

namespace ISoft.RichReportDesigner.Core.Reports
{
    /// <summary>
    /// a rich report for all platform
    /// </summary>
    public class RichReport : RichReportBase
    {
        #region Constructors
        public RichReport(IRichReportContext context) : base(context)
        {
        }
        #endregion

        #region Proprties
        #endregion

        #region Actions

        protected override void RegisterServices()
        {
            base.RegisterServices();
            AddService(typeof(IRichWaitIndicationService), new RichDefaultWaitIndicationService());
        }

        protected override bool HandelResult(DataValidationResult validationResult)
        {
            return validationResult.IsValid;
        }
        #endregion
    }
}
