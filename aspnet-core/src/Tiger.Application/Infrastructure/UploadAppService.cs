/**
 * 类    名：Upload   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/15 15:07:04       
 * 说    明: 
 * 
 */

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Identity;

namespace Tiger.Utilities.DownLoadUpLoadFilesHelper
{

    [RemoteService(Name = "Upload")]
    [Area("upload")]
    [Route("api/upload")]
    [ApiExplorerSettings(GroupName = "admin")]
    public  class UploadAppService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UploadAppService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// 上传 文件,并返回相对url(不包含 host port wwwroot)
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [Route("upload-file")]
        [HttpPost]
        public async Task<string> Upload(IFormFile file)
        {
            // 网站根目录
            string content_path = _webHostEnvironment.ContentRootPath;
            // wwwroot 静态资源根目录
            string web_path = _webHostEnvironment.WebRootPath;


            string webRootPath = web_path; // wwwroot 文件夹
            string uploadPath = Path.Combine("uploads", DateTime.Now.ToString("yyyyMMdd"));
            string dirPath = Path.Combine(webRootPath, uploadPath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            string fileExt = Path.GetExtension(file.FileName).Trim('.'); //文件扩展名，不含“.”
            string newFileName = Guid.NewGuid().ToString().Replace("-", "") + "." + fileExt; //随机生成新的文件名
            var filePath = Path.Combine(dirPath, newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var url = $@"\{uploadPath}\{newFileName}";

            return url;
        }
    }
}
