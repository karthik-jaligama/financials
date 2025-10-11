using Financials.Data.Classes;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Financials.Data.Services;

public class RealSharePointFileService : ISharePointFileService
{
    private readonly HttpClient _httpClient;
    private readonly SharePointConfiguration _config;
    private readonly ILogger<RealSharePointFileService> _logger;

    public RealSharePointFileService(
        HttpClient httpClient, 
        IOptions<SharePointConfiguration> config,
        ILogger<RealSharePointFileService> logger)
    {
        _httpClient = httpClient;
        _config = config.Value;
        _logger = logger;
    }

    public async Task<List<SharePointFileInfo>> SearchFilesAsync(string fileName, DateTime? lastModified = null)
    {
        try
        {
            Console.WriteLine($"=== SHAREPOINT SEARCH DEBUG ===");
            Console.WriteLine($"Searching for: '{fileName}'");
            Console.WriteLine($"Site ID: {_config.SiteId}");
            Console.WriteLine($"Base URL: {_config.BaseUrl}");
            
            // Get access token
            var accessToken = await GetAccessTokenAsync();
            if (string.IsNullOrEmpty(accessToken))
            {
                Console.WriteLine("❌ Failed to get access token for SharePoint");
                _logger.LogWarning("Failed to get access token for SharePoint");
                return new List<SharePointFileInfo>();
            }
            
            Console.WriteLine($"✅ Access token obtained: {accessToken.Substring(0, 20)}...");

            // First, let's try to get the site by hostname and path
            var siteUrl = $"https://graph.microsoft.com/v1.0/sites/{_config.BaseUrl.Replace("https://", "")}:/sites/KickstandWealth";
            Console.WriteLine($"🔍 Getting site info from: {siteUrl}");
            
            var siteResponse = await _httpClient.GetAsync(siteUrl);
            Console.WriteLine($"🔍 Site response status: {siteResponse.StatusCode}");
            
            string actualSiteId = _config.SiteId;
            if (siteResponse.IsSuccessStatusCode)
            {
                var siteContent = await siteResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"🔍 Site response content: {siteContent}");
                
                var siteResult = JsonSerializer.Deserialize<SharePointSiteResult>(siteContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                if (siteResult?.Id != null)
                {
                    actualSiteId = siteResult.Id;
                    Console.WriteLine($"✅ Found actual site ID: {actualSiteId}");
                }
            }
            else
            {
                var errorContent = await siteResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"❌ Site lookup failed: {errorContent}");
            }

            // Now try to list all files in the root directory
            var listUrl = $"https://graph.microsoft.com/v1.0/sites/{actualSiteId}/drive/root/children";
            Console.WriteLine($"📁 Listing all files from: {listUrl}");
            
            _httpClient.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var listResponse = await _httpClient.GetAsync(listUrl);
            Console.WriteLine($"📁 List response status: {listResponse.StatusCode}");
            
            if (listResponse.IsSuccessStatusCode)
            {
                var listContent = await listResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"📁 List response content: {listContent}");
                
                var listResult = JsonSerializer.Deserialize<SharePointSearchResult>(listContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                if (listResult?.Value != null)
                {
                    Console.WriteLine($"📁 Found {listResult.Value.Length} items in root directory:");
                    foreach (var item in listResult.Value)
                    {
                        Console.WriteLine($"  - {item.Name} (ID: {item.Id})");
                    }
                }
            }
            else
            {
                var errorContent = await listResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"❌ List failed: {errorContent}");
            }

            // Now try the search
            var searchUrl = $"https://graph.microsoft.com/v1.0/sites/{_config.SiteId}/drive/root/search(q='{Uri.EscapeDataString(fileName)}')";
            Console.WriteLine($"🔍 Search URL: {searchUrl}");
            
            var response = await _httpClient.GetAsync(searchUrl);
            Console.WriteLine($"🔍 Search response status: {response.StatusCode}");
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"❌ Search failed: {errorContent}");
                _logger.LogError($"SharePoint search failed: {response.StatusCode} - {errorContent}");
                return new List<SharePointFileInfo>();
            }

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"🔍 Search response content: {content}");
            
            var searchResult = JsonSerializer.Deserialize<SharePointSearchResult>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var files = new List<SharePointFileInfo>();
            
            if (searchResult?.Value != null)
            {
                Console.WriteLine($"🔍 Found {searchResult.Value.Length} search results:");
                foreach (var item in searchResult.Value)
                {
                    Console.WriteLine($"  - {item.Name} (Modified: {item.LastModifiedDateTime})");
                    
                    // Filter by date if provided
                    if (lastModified.HasValue)
                    {
                        var itemDate = DateTime.Parse(item.LastModifiedDateTime);
                        var tolerance = TimeSpan.FromDays(1);
                        if (Math.Abs((itemDate - lastModified.Value).TotalDays) > tolerance.TotalDays)
                        {
                            Console.WriteLine($"    ⏰ Date filter: Skipping (date difference: {Math.Abs((itemDate - lastModified.Value).TotalDays)} days)");
                            continue;
                        }
                    }

                    files.Add(new SharePointFileInfo
                    {
                        Name = item.Name,
                        Url = item.WebUrl,
                        DriveItemId = item.Id,
                        WebUrl = item.WebUrl,
                        LastModified = DateTime.Parse(item.LastModifiedDateTime),
                        Size = item.Size,
                        ContentType = item.File?.MimeType ?? "application/octet-stream"
                    });
                }
            }
            
            Console.WriteLine($"✅ Returning {files.Count} files");
            Console.WriteLine($"=== END SHAREPOINT SEARCH DEBUG ===");
            return files;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Exception in SharePoint search: {ex.Message}");
            Console.WriteLine($"❌ Stack trace: {ex.StackTrace}");
            _logger.LogError(ex, "Error searching SharePoint files");
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
        try
        {
            var accessToken = await GetAccessTokenAsync();
            if (string.IsNullOrEmpty(accessToken))
                return string.Empty;

            var createLinkUrl = $"https://graph.microsoft.com/v1.0/sites/{_config.SiteId}/drive/items/{driveItemId}/createLink";
            
            _httpClient.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var linkRequest = new
            {
                type = "view",
                scope = "anonymous"
            };

            var json = JsonSerializer.Serialize(linkRequest);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(createLinkUrl, content);
            
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var linkResult = JsonSerializer.Deserialize<SharePointLinkResult>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                return linkResult?.Link?.WebUrl ?? string.Empty;
            }

            return string.Empty;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating shareable link");
            return string.Empty;
        }
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

    private async Task<string?> GetAccessTokenAsync()
    {
        try
        {
            Console.WriteLine($"=== TOKEN REQUEST DEBUG ===");
            Console.WriteLine($"Tenant ID: {_config.TenantId}");
            Console.WriteLine($"Client ID: {_config.ClientId}");
            Console.WriteLine($"Client Secret: {_config.ClientSecret.Substring(0, 10)}...");
            
            var tokenUrl = $"https://login.microsoftonline.com/{_config.TenantId}/oauth2/v2.0/token";
            Console.WriteLine($"Token URL: {tokenUrl}");
            
            var formData = new List<KeyValuePair<string, string>>
            {
                new("client_id", _config.ClientId),
                new("client_secret", _config.ClientSecret),
                new("scope", "https://graph.microsoft.com/.default"),
                new("grant_type", "client_credentials")
            };

            var formContent = new FormUrlEncodedContent(formData);
            var response = await _httpClient.PostAsync(tokenUrl, formContent);
            
            Console.WriteLine($"Token response status: {response.StatusCode}");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Token response content: {content}");
                
                var tokenResult = JsonSerializer.Deserialize<TokenResult>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                Console.WriteLine($"✅ Token obtained successfully");
                Console.WriteLine($"=== END TOKEN REQUEST DEBUG ===");
                return tokenResult?.AccessToken;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"❌ Token request failed: {errorContent}");
                _logger.LogError($"Token request failed: {response.StatusCode} - {errorContent}");
            }
            
            Console.WriteLine($"=== END TOKEN REQUEST DEBUG ===");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Exception getting token: {ex.Message}");
            Console.WriteLine($"❌ Stack trace: {ex.StackTrace}");
            _logger.LogError(ex, "Error getting access token");
            return null;
        }
    }
}

// Helper classes for JSON deserialization
public class SharePointSiteResult
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string WebUrl { get; set; } = string.Empty;
}

public class SharePointSearchResult
{
    public SharePointDriveItem[]? Value { get; set; }
}

public class SharePointDriveItem
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string WebUrl { get; set; } = string.Empty;
    public string LastModifiedDateTime { get; set; } = string.Empty;
    public long Size { get; set; }
    public SharePointFile? File { get; set; }
}

public class SharePointFile
{
    public string MimeType { get; set; } = string.Empty;
}

public class SharePointLinkResult
{
    public SharePointLink? Link { get; set; }
}

public class SharePointLink
{
    public string WebUrl { get; set; } = string.Empty;
}

public class TokenResult
{
    public string AccessToken { get; set; } = string.Empty;
}
