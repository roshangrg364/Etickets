using CoreModule.Source.Service.Image;
using ETicketing.Exceptions;
using MimeKit;
using System.Security.Cryptography;

namespace ETicketing.Helper
{
    public class FileHelper:FilerHelperInterface
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FileHelper(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        private const string UploadDirectory = "images/uploads";
        private string[] AllowedImageMimeTypes { get; } =
      {
            "image/jpeg", "image/png", "image/jpg", "image/svg+xml", "image/webp", "image/jfif", "image/bmp",
            "image/ico", "img/bmp"
        };



        public bool IsValidImage(string fileName)
        {
            var mimeType = MimeTypes.GetMimeType(fileName);
            return ValidateFileMime(mimeType, AllowedImageMimeTypes);
        }

        public void RemoveFile(string fileName)
        {
            var filePath = GetFilePathUptoUploadDirectory(fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public string SaveImageAndGetFileName(string fileName, string filePrefix = "")
        {
            if (!IsValidImage(fileName))
            {
                throw new InvalidImageTypeException();
            }
            var newFileName = GenerateFileNameWithPrefix(fileName, filePrefix);
            var filePath = GetUploadDirectory();

            var destinationFileLocation = MoveFilesToDirectory(filePath, fileName, newFileName);
            return newFileName;
        }

        private static bool ValidateFileMime(string mimeType, string[] allowedMimeTypes)
        {
            return allowedMimeTypes.Contains(mimeType.ToLower());
        }
        private static string GenerateFileNameWithPrefix(string file, string filePrefix)
        {
            return GenerateFileName(file,
                string.IsNullOrWhiteSpace(filePrefix) ? Path.GetFileNameWithoutExtension(file) : filePrefix);
        }
        private static string GenerateFileName(string file, string filePrefix)
        {
            var FileName = filePrefix.Replace(' ', '-') + RandomNumberGenerator.GetInt32(1, 1232384943) + Path.GetExtension(file);
            return FileName;
        }
        private string GetUploadDirectory()
        {
            var fileDirectoryPath = Path.Combine(_hostingEnvironment.WebRootPath, UploadDirectory);
            if (!Directory.Exists(fileDirectoryPath))
            {
                Directory.CreateDirectory(fileDirectoryPath);
            };

            return fileDirectoryPath;

        }
        private static string MoveFilesToDirectory(string directory, string file, string fileName)
        {
            var destinationFileName = Path.Combine(directory, fileName);
            File.Move(file, destinationFileName, true);
            return destinationFileName;
        }
        private string GetFilePathUptoUploadDirectory(string fileName)
        {
            return Path.Combine(GetUploadDirectory(), fileName);
        }
    }
}
