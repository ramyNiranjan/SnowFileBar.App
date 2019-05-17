using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnowFileBar.App.Model;
using SnowFileBar.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFileBar.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileDataApiController : ControllerBase
    {
        private readonly IFileRepository fileRepository;
        private readonly IMapper mapper;

        public FileDataApiController(IFileRepository fileRepository,IMapper mapper)
        {
            this.fileRepository = fileRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<FileBarDataModel[]>> GetAllFileBarInfo()
        {
            try
            {
                var result = await fileRepository.GetAllFilesData();
                if (result == null)
                    return NotFound();
                return mapper.Map<FileBarDataModel[]>(result);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failiure");
            }


        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<FileBarDataModel>> Get(int id)
        {
            try
            {
                var result = await fileRepository.GetFileDataById(id);
                if (result == null) return NotFound();
                return mapper.Map<FileBarDataModel>(result);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failiure");
            }
        }
    }
}
