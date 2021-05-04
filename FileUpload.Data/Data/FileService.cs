using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileUpload.Data.Data
{
    public class FileService : IFileService
    {
        private static IList<File> _files = new List<File>();

        private readonly IConfiguration _configuration;

        public FileService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IList<File>> GetFiles()
        {
            return await Task.Run(() =>
            {
                return _files;
            });
        }

        public async Task SaveFiles(List<IFormFile> files)
        {
            foreach(var file in files)
            {
                string fileName = GenerateNewFileName(file.FileName);
                string fileFormat = GetFileFormat(fileName);
                
                byte[] bytesFile = ConvertFileInByteArray(file);

                string directory = CreateFilePath(fileName);
                await System.IO.File.WriteAllBytesAsync(directory, bytesFile);

                var url = GetFileUrl(fileName); 
                _files.Add(new File(
                    url,
                    fileFormat));
            }
        }

        private string GetFileFormat(string fullFileName)
        {
            var format = fullFileName.Split(".").Last();
            return "." + format;
        }

        private string GenerateNewFileName(string fileName)
        {
            var newFileName = (Guid.NewGuid().ToString() + "_" + fileName).ToLower();
            newFileName = newFileName.Replace("-", "");

            return newFileName;
        }

        private string CreateFilePath(string fileName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), _configuration["Directories:Files"], fileName);
        }

        private string GetFileUrl(string fileName)
        {
            var baseUrl = _configuration["Directories:BaseUrl"];

            var fileUrl = _configuration["Directories:Files"]
                .Replace("wwwroot", "")
                .Replace("\\", "");
            
            return (baseUrl + "/" + fileUrl + "/" + fileName);
        }

        private byte[] ConvertFileInByteArray(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
