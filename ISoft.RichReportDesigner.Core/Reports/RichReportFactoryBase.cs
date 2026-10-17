using System;

namespace ISoft.RichReportDesigner.Core.Reports
{
    public interface IRichReportFactory<TReport> where TReport : IRichReport
    {
        TReport CreateReport();
    }

    public abstract class RichReportFactoryBase<TReport> : IRichReportFactory<TReport> where TReport : IRichReport
    {
        private readonly Func<TReport> _factory;

        protected RichReportFactoryBase(Func<TReport> factory)
        {
            _factory = factory;
        }

        TReport IRichReportFactory<TReport>.CreateReport()
        {
            return _factory();
        }
    }
}
