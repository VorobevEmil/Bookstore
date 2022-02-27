using Bookstore.Shared.DbModels.DbInterfaces;
using Bookstore.Shared.Model;

namespace Bookstore.Server.Data.Service
{
    public class FileManagementService<TEntitiy> where TEntitiy : IFileManagement
    {
        public async Task<string> UploadFile(FileData fileData, string folderRecord)
        {
            string path = FilePath.FOLDER_PATH;

            path = path[^1] == '\\' ? path : path + '\\';
            folderRecord = folderRecord[^1] == '\\' ? folderRecord : folderRecord + '\\';

            string? errorMessage = null;
            if (fileData.ContentType.Split('/')[0] == "image")
            {

                if (File.Exists($"{path}{folderRecord}{fileData.FileName}"))
                {
                    errorMessage = $"Изображение {fileData.FileName} уже существует по пути {path}{folderRecord}{fileData.FileName}";
                }
                else
                {
                    using (var fileStream = File.Create($"{path}{folderRecord}{fileData.FileName}"))
                    {
                        await fileStream.WriteAsync(fileData.Data);
                    }
                }
            }

            return errorMessage;
        }
    }
}
