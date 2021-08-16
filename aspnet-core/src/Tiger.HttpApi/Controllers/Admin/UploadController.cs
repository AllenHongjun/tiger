/**
 * 类    名：UploadController   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/15 15:13:12       
 * 说    明: 
 * 
 */


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Tiger.Controllers.Admin
{
    public class UploadController : AbpController
    {
        public UploadController()
        {
        }

        ///// <summary>
        ///// 上传 文件,并返回相对url(不包含 host port wwwroot)
        ///// </summary>
        ///// <param name="file"></param>
        ///// <returns></returns>
        //[Route("upload-file")]
        //[HttpPost]
        //public async Task<string> UploadFile(IFormFile file)
        //{
        //    if (file == null)
        //    {
        //        return null;
        //    }

        //    UploadHelper
        //}

    }
}
