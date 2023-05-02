namespace DataCaptureService.Models
{
    internal class FileCapturedModel
    {
        public string Format { get; }
        public string Path { get; }

        public FileCapturedModel(string format, string path)
        {
            Format = format;
            Path = path;
        }

        public void Deconstruct(out string format, out string path)
        {
            format = Format;
            path = Path;
        }
    }
}
