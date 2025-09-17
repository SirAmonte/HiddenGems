using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenGems.Application.Services.HiddenGems
{
    public class FileUploadService : IFileUploadService
    {
        public string UploadFile(Stream input, string fileName)
        {
            var filePath = Path.Combine("wwwroot/images", fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                input.CopyTo(fileStream);
            }

            return "/images/" + fileName;
        }
    }
}
