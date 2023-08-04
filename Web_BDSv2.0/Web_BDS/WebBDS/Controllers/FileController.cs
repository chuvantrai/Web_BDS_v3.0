using Microsoft.AspNetCore.Mvc;
using WebBDS.Emuns;
using WebBDS.Extensions;
using WebBDS.Models;

namespace WebBDS.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly Bds_CShapContext _context;
    private readonly IExtensionFile _extensionFile;
    private readonly long _sizeFileImage = 10; //10mb

    public FileController(Bds_CShapContext context, IExtensionFile extensionFile)
    {
        _context = context;
        _extensionFile = extensionFile;
    }

    [HttpPost]
    public async Task<IActionResult> CreateImage([FromForm] IFormFile imageFile)
    {
        try
        {
            var fileSize = imageFile.Length / (1024 * 1024);
            if (fileSize < _sizeFileImage)
            {
                var nameImage = await _extensionFile.CreateImage(imageFile);
                return Ok(new
                {
                    success = true,
                    status = 200,
                    data = nameImage
                });
            }

            return NotFound(ExpressionLogic.GetEnumDescription(MessageApiEnum.SizeImageLimit));
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateListImage([FromForm] List<IFormFile> imageFile)
    {
        try
        {
            var listNameImage = new List<string>();
            foreach (var file in imageFile)
            {
                var fileSize = file.Length / (1024 * 1024);
                if (fileSize < _sizeFileImage)
                {
                    var nameImage = await _extensionFile.CreateImage(file);
                    listNameImage.Add(nameImage);
                }
                else
                {
                    return NotFound(ExpressionLogic.GetEnumDescription(MessageApiEnum.SizeImageLimit));
                }
            }

            return Ok(new
            {
                success = true,
                status = 200,
                data = listNameImage
            });
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}