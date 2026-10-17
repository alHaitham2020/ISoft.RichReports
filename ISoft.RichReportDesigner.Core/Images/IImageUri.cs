namespace ISoft.RichReportDesigner.Core.Images
{
    public interface IImageUri
    {
        string FullUri { get; }

        bool IsEmpty { get; }

        bool IsValid { get; }

        string Path { get; }

        ImageUriScheme Scheme { get; }

        ImageUri WithPath(string newPath);

        ImageUri WithScheme(ImageUriScheme newScheme);

        ImageUri ConvertTo(ImageUriScheme targetScheme);

        void Deconstruct(out ImageUriScheme scheme, out string path);

        bool Equals(ImageUri other);

        bool Equals(object obj);

        int GetHashCode();

        string ToString();
       
    }
}
