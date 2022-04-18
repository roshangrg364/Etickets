using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Service.Image
{
    public interface ImageUploaderInterface
    {
        Task<string> UploadToTemporary(IFormFile file);
    }
}
