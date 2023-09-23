using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Data;
using Volo.Abp.Identity;

namespace Tiger.Volo.Abp.Identity
{

    /// <summary>
    /// 扩展角色对象的属性
    /// </summary>
    /// <remarks>
    /// 对象扩展 https://docs.abp.io/zh-Hans/abp/latest/Object-Extensions
    /// </remarks>
    public static class IdentityRoleDtoExtensions
    {
        private const string UserCountPropertyName = "UserCount";

        public static void SetUserCount(this IdentityRoleDto role, int? userCount)
        {
            role.SetProperty(UserCountPropertyName, userCount);
        }

        public static int? GetUserCount(this IdentityRoleDto role)
        {
            return role.GetProperty<int?>(UserCountPropertyName);
        }
    }

}
