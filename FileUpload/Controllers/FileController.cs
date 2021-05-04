using FileUpload.Data.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileUpload.Controllers
{
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        [Route("/api/v1/file/upload")]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            try
            {
                if(files.Count > 0)
                {
                    await _fileService.SaveFiles(files);
                    return Ok();
                }
                else
                {
                    return StatusCode(204, "Any file were sent!");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("/api/v1/file/get-files")]
        public async Task<IActionResult> GetFiles()
        {
            try
            {
                return Ok(await _fileService.GetFiles());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
