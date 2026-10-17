namespace ISoft.RichReportDesigner.Core.Fields.Argumentes
{
    public interface IRichFieldArgumentBuilder
    {
        IRichFieldArgumentBuilder Add(object value);

        string Build();
    }
}

   

