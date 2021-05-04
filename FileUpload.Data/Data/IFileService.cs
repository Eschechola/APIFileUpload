using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileUpload.Data.Data
{
    public interface IFileService
    {
        Task SaveFiles(List<IFormFile> files);
        Task<IList<File>> GetFiles();
    }
}
