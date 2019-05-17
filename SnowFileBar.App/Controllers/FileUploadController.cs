using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnowFileBar.App.FileManagerService;
using SnowFileBar.App.Model;
using SnowFileBar.Data;
using SnowFileBar.Domain;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SnowFileBar.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController: ControllerBase
    {
        private readonly string[] accepted_FileTypes = new[] { ".txt", ".rtf", ".log", ".docx" };
        private readonly IFileRepository fileRepository;
        private readonly IMapper mapper;

        public FileUploadController(IFileRepository fileRepository, IMapper mapper)
        {
            this.fileRepository = fileRepository;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm]IFormFile fileData)
        {
            try
            {
                if (fileData == null) return BadRequest("null file");
                if (fileData.Length == 0)
                {
                    return BadRequest("empty file");
                }
                if (!accepted_FileTypes.Any(s => s == Path.GetExtension(fileData.FileName).ToLower()))
                    return BadRequest("invalid file type.");
                var folderName = Path.Combine("Resources", "Files");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fileName = ContentDispositionHeaderValue.Parse(fileData.ContentDisposition).FileName.Trim('"');

                var fullpath = Path.Combine(pathToSave, fileName);

                using (var stream = new FileStream(fullpath, FileMode.Create))
                {
                    await fileData.CopyToAsync(stream);
                }
                var fileSaving = new FileSaving(mapper, fileRepository);
                await fileSaving.SavingFileDataToDataBase(fullpath);


                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "internal server error");
            }

        }
       
    }
}
