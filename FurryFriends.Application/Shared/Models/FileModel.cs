namespace FurryFriends.Application.Shared.Models
{
    public class FileModel
    {
        public string RootPath { get; set; }
        public long FileLength { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string ContentType { get; set; }
        public Stream Content { get; set; }
    }
}