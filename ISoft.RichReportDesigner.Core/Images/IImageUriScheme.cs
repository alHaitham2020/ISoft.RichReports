namespace ISoft.RichReportDesigner.Core.Images
{
    public interface IImageUriScheme
    {
        bool IsEmpty { get; }

        bool IsKnownScheme { get; }

        string Name { get; }

        string Prefix { get; }
        ImageUriSchemeType Type { get; }

        bool CanHandle(string uri);

        bool TryParsePath(string uri, out string path);

        string CreateUriString(string path);

        ImageUri CreateUri(string path);

        bool Equals(ImageUriScheme other);

        bool Equals(object obj);

        int GetHashCode();

        string ToString();
       
    }
}
