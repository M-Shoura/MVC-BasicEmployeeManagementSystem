﻿using Microsoft.AspNetCore.Http;

namespace IKIA.BLL.Common.Services.Attachments
{
    public interface IAttachmentService
    {
        Task<string?> UploadAsync(IFormFile file, string folderName);  
        
        bool Delete(string filePath);
    }
}