using System;

namespace FileUpload.Data.Data
{
    public class File
    {
        public File(string url, string format)
        {
            Id = Guid.NewGuid();
            Url = url;
            Format = format;
        }

        public Guid Id { get; private set; }
        public string Url { get; private set; }
        public string Format { get; private set; }
    }
}
