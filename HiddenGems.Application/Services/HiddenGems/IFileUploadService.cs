using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenGems.Application.Services.HiddenGems
{
    public interface IFileUploadService
    {
        string UploadFile(Stream input, string filePath);
    }
}
