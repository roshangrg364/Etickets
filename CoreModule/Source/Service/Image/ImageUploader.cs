using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Service.Image
{
    public class ImageUploader : ImageUploaderInterface
    {
        public async Task<string> UploadToTemporary(IFormFile file)
        {
            var filePath = Path.Combine(Path.GetTempPath(), file.FileName);
            var stream = File.Create(filePath);
            await file.CopyToAsync(stream).ConfigureAwait(true);
            await stream.DisposeAsync().ConfigureAwait(true);
            return filePath;
        }
        public static string GetTemporaryDirectory()
        {
            return Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
        }
    }
}
