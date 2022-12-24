using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tiger.Volo.Abp.SettingManagementAppService
{
    /// <summary>
    /// 身份标识管理
    /// </summary>
    public class UpdateIdentitySettingsDto
    {
        public Password password { get; set; }
        public Lockout lockout { get; set; }
        public Signin signIn { get; set; }
        public User user { get; set; }
        
    }

    
}
