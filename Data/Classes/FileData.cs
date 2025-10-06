namespace Financials.Data.Classes
{
    public class FileData
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public long Size { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.Now;
    }
}
