using Financials.Data.Classes;

namespace Financials.Data.Services;

public class SharePointFileService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public SharePointFileService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<List<SharePointFileInfo>> SearchFilesAsync(string fileName, DateTime? lastModified = null)
    {
        try
        {
            // This would integrate with Microsoft Graph API
            // For now, returning mock data to demonstrate the concept
            
            var mockResults = new List<SharePointFileInfo>
            {
                new SharePointFileInfo
                {
                    Name = fileName,
                    Url = $"https://yourcompany.sharepoint.com/sites/documents/Shared%20Documents/{fileName}",
                    DriveItemId = Guid.NewGuid().ToString(),
                    WebUrl = $"https://yourcompany.sharepoint.com/sites/documents/Shared%20Documents/{fileName}",
                    LastModified = lastModified ?? DateTime.UtcNow.AddDays(-1),
                    Size = 1024000,
                    ContentType = GetContentTypeFromFileName(fileName)
                }
            };

            // Filter by name and date if provided
            if (!string.IsNullOrEmpty(fileName))
            {
                mockResults = mockResults.Where(f => f.Name.Contains(fileName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (lastModified.HasValue)
            {
                // Allow for some tolerance in date matching (within 1 day)
                var tolerance = TimeSpan.FromDays(1);
                mockResults = mockResults.Where(f => 
                    Math.Abs((f.LastModified - lastModified.Value).TotalDays) <= tolerance.TotalDays).ToList();
            }

            return mockResults;
        }
        catch (Exception ex)
        {
            // Log the error and return empty list
            Console.WriteLine($"Error searching SharePoint files: {ex.Message}");
            return new List<SharePointFileInfo>();
        }
    }

    public async Task<SharePointFileInfo?> FindMatchingFileAsync(FileData localFile)
    {
        var searchResults = await SearchFilesAsync(localFile.Name, localFile.UploadedAt);
        return searchResults.FirstOrDefault();
    }

    public async Task<string> CreateShareableLinkAsync(string driveItemId)
    {
        // This would call Microsoft Graph API to create a shareable link
        // For now, returning a mock URL
        return $"https://yourcompany.sharepoint.com/sites/documents/Shared%20Documents/_layouts/15/guestaccess.aspx?docid={driveItemId}";
    }

    public async Task<FileData> LinkToSharePointAsync(FileData localFile, SharePointFileInfo sharePointFile)
    {
        localFile.SharePointUrl = sharePointFile.Url;
        localFile.SharePointDriveItemId = sharePointFile.DriveItemId;
        localFile.SharePointWebUrl = sharePointFile.WebUrl;
        localFile.SharePointLastModified = sharePointFile.LastModified;
        
        // Create a shareable link
        localFile.SharePointShareUrl = await CreateShareableLinkAsync(sharePointFile.DriveItemId);
        
        return localFile;
    }

    private string GetContentTypeFromFileName(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLower();
        return extension switch
        {
            ".pdf" => "application/pdf",
            ".doc" => "application/msword",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".xls" => "application/vnd.ms-excel",
            ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".txt" => "text/plain",
            _ => "application/octet-stream"
        };
    }
}

public class SharePointFileInfo
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string DriveItemId { get; set; } = string.Empty;
    public string WebUrl { get; set; } = string.Empty;
    public DateTime LastModified { get; set; }
    public long Size { get; set; }
    public string ContentType { get; set; } = string.Empty;
}
