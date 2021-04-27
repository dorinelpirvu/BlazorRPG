using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorRPG.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IHostEnvironment _environment;
        public ImagesController(IHostEnvironment environment)
        {
            _environment = environment;
        }
        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile fisier)
        {
            if (fisier == null || fisier.Length == 0)  //aici nu ajung, este verificata in interfata
                return BadRequest("incarca un fisier!");

            string fileName = fisier.FileName;
            string fileNameMic = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);
            string[] allowedExtensions = { ".pdf", ".rtf" };

            if (!allowedExtensions.Contains(extension))
                return BadRequest("format nerecunoscut");

            var numeChar = fileNameMic;
            
            //var numeChar = DateTime.Today.ToShortDateString();
            string newFileName = $"{numeChar}{extension}";
            string filePath = Path.Combine(_environment.ContentRootPath, "Pdfuri", newFileName);
            try
            {
                var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                await fisier.CopyToAsync(fileStream);
                return Ok($"/Pdfuri/{newFileName}");
            }
            catch (Exception e)
            {
                var eroare = e.Message;
            }
            
            return BadRequest("eroare la scriere");
        }

        [HttpGet("{pathToFile}")]
        public  async Task<ActionResult> DownloadFile( string pathToFile)
        {
            //return File(pathToFileInSharedDrive,".pdf");
            string filePath1 = Path.Combine(_environment.ContentRootPath, "Pdfuri", pathToFile);

           
                
                var fileStream = new FileStream(filePath1, FileMode.Open, FileAccess.Read);

            //var fileInfo = _environment.ContentRootFileProvider.GetFileInfo(pathToFile);
            var pictureBytes = await System.IO.File.ReadAllBytesAsync(filePath1);
            var fileD = System.IO.File.OpenRead(filePath1);
            return  File(pictureBytes, "application/pdf");
            
           
        }

        [HttpGet("DownloadServerFile/{pathToFile}")]
        public async Task<string> DownloadServerFile(string pathToFile)
        {
            var filePath = Path.Combine(_environment.ContentRootPath, "Pdfuri", pathToFile);

            using (var fileInput = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                MemoryStream memoryStream = new MemoryStream();
                await fileInput.CopyToAsync(memoryStream);

                var buffer = memoryStream.ToArray();
                return Convert.ToBase64String(buffer);
            }
        }

    }
}
