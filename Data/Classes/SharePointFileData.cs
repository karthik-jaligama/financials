namespace Financials.Data.Classes;

public class SharePointFileData
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long Size { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    
    // SharePoint specific properties
    public string? SharePointUrl { get; set; }
    public string? DriveItemId { get; set; }
    public string? WebUrl { get; set; }
    public string? ShareId { get; set; }
    public string? ShareUrl { get; set; }
    public string? SiteId { get; set; }
    public string? ListId { get; set; }
    
    // File source type
    public FileSource Source { get; set; } = FileSource.Local;
}

public enum FileSource
{
    Local,      // Local file reference
    SharePoint, // SharePoint file
    OneDrive,   // OneDrive file
    Teams       // Teams file
}

