namespace ISoft.RichReportDesigner.Core.RichEdit.Properties
{
    public interface IRichCalculatedFieldProperty : IRichProperty
    {
        string DataMember { get; }

        string Expression { get; }

        bool HasValidDataMember { get; }
    }
}
