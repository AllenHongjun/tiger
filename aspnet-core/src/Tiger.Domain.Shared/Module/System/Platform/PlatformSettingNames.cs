using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.System.Platform
{   
    /// <summary>
    /// 平台模块限制
    /// </summary>
    public class PlatformSettingNames
    {
        public const string GroupName = "AppPlatform";

        public class AppVersion
        {
            public const string Default = GroupName + ".AppVersion";
            /// <summary>
            /// 文件限制长度
            /// </summary>
            public const string VersionFileLimitLength = Default + ".VersionFileLimitLength";
            /// <summary>
            /// 允许的文件扩展名类型
            /// </summary>
            public const string AllowVersionFileExtensions = Default + ".AllowVersionFileExtensions";
        }
    }
}
