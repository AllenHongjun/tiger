using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Module.OssManagement.Dtos;

namespace Tiger.Module.OssManagement
{
    public interface IFileValidater
    {
        Task ValidationAsync(UploadFile input);
    }
}
