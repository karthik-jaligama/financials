namespace Financials.Data.Classes;

public class FileData
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long Size { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    public string? Url { get; set; }
    public byte[]? Content { get; set; }
    
    // SharePoint integration
    public string? SharePointUrl { get; set; }
    public string? SharePointDriveItemId { get; set; }
    public string? SharePointWebUrl { get; set; }
    public string? SharePointShareUrl { get; set; }
    public string? SharePointSiteId { get; set; }
    public DateTime? SharePointLastModified { get; set; }
    public bool IsLinkedToSharePoint => !string.IsNullOrEmpty(SharePointUrl);
}
