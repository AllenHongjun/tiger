using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.OssManagement.Settings
{   
    /// <summary>
    /// OSS管理设置
    /// </summary>
    public class AbpOssManagementSettingNames
    {
        public const string GroupName = "Abp.OssManagement";

        /// <summary>
        /// 下载分包大小
        /// </summary>
        public const string DownloadPackageSize = GroupName + ".DownloadPackageSize";
        /// <summary>
        /// 文件限制长度
        /// </summary>
        public const string FileLimitLength = GroupName + ".FileLimitLength";
        /// <summary>
        /// 允许的文件扩展名类型
        /// </summary>
        public const string AllowFileExtensions = GroupName + ".AllowFileExtensions";

        /// <summary>
        /// 默认最大文件大小限制
        /// </summary>
        public const long DefaultFileLimitLength = 100L;

        /// <summary>
        /// 默认允许上传的文件类型
        /// </summary>
        public const string DefaultAllowFileExtensions = "dll,zip,rar,txt,log,xml,config,json,jpeg,jpg,png,bmp,ico,xlsx,xltx,xls,xlt,docs,dots,doc,dot,pptx,potx,ppt,pot,chm";
    }
}
