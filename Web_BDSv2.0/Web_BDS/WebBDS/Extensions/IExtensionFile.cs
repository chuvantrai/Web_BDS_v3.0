namespace WebBDS.Extensions;

public interface IExtensionFile
{
    Task<bool> SendEmailAsync(string recipient, string subject, string body);

    string GeneratePassword(int length);

    Task<string> CreateImage(IFormFile myFile);
    
    Task<string> UpdateImage(IFormFile myFile,string oldFile);
    
    Task<string> UpdateImageAvatarUser(IFormFile myFile,string? oldFile);

    Task<string> CreatePDF(IFormFile myFile);
}