using System.IO;

namespace Cocktails.Mailing.Models
{
    public sealed class Attachment
    {
        public Attachment()
        {
        }

        public Attachment(string path)
            : this(File.ReadAllBytes(path), Path.GetFileName(path), MimeType.GetByFilePath(path))
        {
        }

        public Attachment(byte[] content, string name, string mediaType)
        {
            Content = content;
            Name = name;
            MediaType = mediaType;
        }

        public byte[] Content { get; set; }

        public string MediaType { get; set; }

        public string Name { get; set; }
    }
}
