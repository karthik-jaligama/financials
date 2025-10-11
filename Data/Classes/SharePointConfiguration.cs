namespace Financials.Data.Classes;

public class SharePointConfiguration
{
    public string BaseUrl { get; set; } = string.Empty;
    public string SiteId { get; set; } = string.Empty;
    public string DocumentLibrary { get; set; } = "Shared Documents";
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string TenantId { get; set; } = string.Empty;
    public string[] Scopes { get; set; } = new[] { "https://graph.microsoft.com/.default" };
}
