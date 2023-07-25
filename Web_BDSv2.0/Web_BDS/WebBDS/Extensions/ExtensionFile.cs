using System.Net;
using System.Net.Mail;

namespace WebBDS.Extensions;

public class ExtensionFile : IExtensionFile
{
    public string FolderFile { get; set; } = "myfiles";
    public string EmailServer { get; set; } = "traicvhe153014@fpt.edu.vn";
    public string PassEmailServer { get; set; } = "0362351671";
    public async Task<bool> SendEmailAsync(string recipient, string subject, string body)
    {
        using (var client = new SmtpClient("smtp.gmail.com", 587))
        {
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(EmailServer, PassEmailServer);

            var message = new MailMessage
            {
                From = new MailAddress(EmailServer),
                To = { recipient },
                Subject = subject,
                Body = body
            };
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.ReplyToList.Add(new MailAddress(EmailServer));
            message.Sender = new MailAddress(EmailServer);

            try
            {
                await client.SendMailAsync(message);
                return true;
            }
            catch (SmtpException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    public string GeneratePassword(int length)
    {
        const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        Random random = new Random();
        char[] chars = new char[length];

        for (int i = 0; i < length; i++)
        {
            chars[i] = validChars[random.Next(0, validChars.Length)];
        }

        return new string(chars);
    }

    public async Task<string> CreateImage(IFormFile myFile)
    {
        if (myFile == null || myFile.Length == 0)
        {
            throw new Exception("File not found or empty.");
        }

        //add img
        var newFileName = Guid.NewGuid();
        var extension = Path.GetExtension(myFile.FileName);
        string fileName = newFileName + extension;

        string filePath = Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot", FolderFile, fileName);
        using (var file = new FileStream(filePath, FileMode.Create))
        {
            await myFile.CopyToAsync(file);
        }

        return fileName;
    }

    public async Task<string> UpdateImage(IFormFile myFile, string oldFile)
    {
        var fileName = await CreateImage(myFile);
        // delete img 
        if (!string.IsNullOrEmpty(oldFile))
        {
            string imgPath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", FolderFile, oldFile);
            FileInfo fileDelete = new FileInfo(imgPath);
            if (fileDelete.Length > 0)
            {
                File.Delete(imgPath);
                fileDelete.Delete();
            }
        }

        return fileName;
    }

    public async Task<string> UpdateImageAvatarUser(IFormFile myFile, string? oldFile)
    {
        var fileName = await CreateImageAvatarUser(myFile);
        // delete img 
        if (!string.IsNullOrEmpty(oldFile))
        {
            string imgPath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", FolderFile, oldFile);
            FileInfo fileDelete = new FileInfo(imgPath);
            if (fileDelete.Length > 0)
            {
                File.Delete(imgPath);
                fileDelete.Delete();
            }
        }

        return fileName;
    }

    public async Task<string> CreateImageAvatarUser(IFormFile myFile)
    {
        if (myFile == null || myFile.Length == 0)
        {
            throw new Exception("File not found or empty.");
        }

        //add img
        var newFileName = Guid.NewGuid();
        var extension = Path.GetExtension(myFile.FileName);
        string fileName = newFileName + extension;

        string filePath = Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot", FolderFile, fileName);
        using (var file = new FileStream(filePath, FileMode.Create))
        {
            await myFile.CopyToAsync(file);
        }

        return fileName;
    }

    public async Task<string> CreatePDF(IFormFile myFile)
    {
        if (myFile == null || myFile.Length == 0)
        {
            throw new Exception("File not found or empty.");
        }

        //add pdf
        var newFileName = Guid.NewGuid();
        var extension = Path.GetExtension(myFile.FileName);
        string fileName = newFileName + extension;

        string filePath = Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot", "pdf", fileName);
        using (var file = new FileStream(filePath, FileMode.Create))
        {
            await myFile.CopyToAsync(file);
        }

        return fileName;
    }
    
    public string CreateImage2(IFormFile myFile)
    {
        if (myFile == null || myFile.Length == 0)
        {
            throw new Exception("File not found or empty.");
        }

        //add img
        var newFileName = Guid.NewGuid();
        var extension = Path.GetExtension(myFile.FileName);
        string fileName = newFileName + extension;

        string filePath = Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot", FolderFile, fileName);
        using (var file = new FileStream(filePath, FileMode.Create))
        { myFile.CopyTo(file);
        }

        return fileName;
    }
}