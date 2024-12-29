using Microsoft.AspNetCore.Http;

namespace IKIA.BLL.Common.Services.Attachments
{
    public class AttachmentService : IAttachmentService
    {
        private List<string> _allowedExtensions = new List<string>() {".png",".jpg",".jpeg"};
        private const int _allowedMaxSize = 2_097_152;
        
        public async Task<string?> UploadAsync(IFormFile file, string folderName)
        {
            var extension = Path.GetExtension(file.FileName); 
            
            if (! _allowedExtensions.Contains(extension))
                return null;
            
            if(file.Length > _allowedMaxSize)
                return null;

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", folderName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(folderPath, fileName);


            if(!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);

            return fileName;
        }
        public bool Delete(string filename)
        {
            if (File.Exists($"{Directory.GetCurrentDirectory()}\\wwwroot\\files\\images\\{filename}"))
            {
                File.Delete($"{Directory.GetCurrentDirectory()}\\wwwroot\\files\\images\\{filename}");
                return true;
            }
            return false;
        }

    }
}
