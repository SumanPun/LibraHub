﻿namespace LibraHub.Helpers.Interface
{
    public interface IFileHelper
    {
        Task<string> UploadFile(IFormFile file, string folderName);
    }
}
