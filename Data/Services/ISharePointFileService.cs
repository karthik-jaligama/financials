using Financials.Data.Classes;

namespace Financials.Data.Services;

public interface ISharePointFileService
{
    Task<List<SharePointFileInfo>> SearchFilesAsync(string fileName, DateTime? lastModified = null);
    Task<SharePointFileInfo?> FindMatchingFileAsync(FileData localFile);
    Task<string> CreateShareableLinkAsync(string driveItemId);
    Task<FileData> LinkToSharePointAsync(FileData localFile, SharePointFileInfo sharePointFile);
}
