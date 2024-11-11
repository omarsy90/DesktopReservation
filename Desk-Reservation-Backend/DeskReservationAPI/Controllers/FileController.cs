using Microsoft.AspNetCore.Mvc;

namespace DeskReservationAPI.Controllers
{


    [Route("api/[controller]")]
    public class FileController : Controller
    {

        /// <summary>
        /// return file by file name if the file available to be exposed.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>

        [HttpGet("{fileName}")]
        public async Task<IActionResult> Index(string fileName)
        {
           FileType fileType =  GetFileType(fileName);
            string filePath = string.Empty;
            if(fileType == FileType.Image)
            {
                 filePath = Path.Combine(Directory.GetCurrentDirectory(), "Asset", "img", fileName);
            }
            else if(fileType == FileType.Txt)
            {
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "Asset", "file", fileName);
            }
            
            
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("file not found");
            }


            string contentType = GetContentType(filePath);
            var file = await System.IO.File.ReadAllBytesAsync(filePath);

            return File(file, contentType);

        }
        private string GetContentType(string filePath)
        {
            string extension = Path.GetExtension(filePath)?.ToLower();

            switch (extension)
            {
                case ".jpg":
                    return "image/jpeg";
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                case ".bmp":
                    return "image/bmp";
                default:
                    return "application/octet-stream";  // defaulr binary type
            }
        }


        private FileType GetFileType(string filePath)
        {
            string[] imagesExtensions = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            var ext = Path.GetExtension(filePath)?.ToLower();
            if (imagesExtensions.Contains(ext))
            {
                return FileType.Image;
            }
             
            if(ext.Contains(".txt"))
            {
                return FileType.Txt;
            }

            return FileType.Binary;
        }
    }


    public enum FileType
    {
        Txt
        ,Image
        ,Binary
    }
}
