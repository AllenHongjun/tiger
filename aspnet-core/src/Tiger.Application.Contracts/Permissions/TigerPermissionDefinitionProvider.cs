using Tiger.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tiger.Permissions
{

#pragma warning disable CS1570 // XML 注释出现 XML 格式错误
    /// <summary>
    /// 权限管理
    /// </summary>
    /// <remarks>
    /// 权限定义
    /// 授权配置教程: https://docs.abp.io/zh-Hans/abp/latest/Tutorials/Part-5?UI=MVC&DB=EF
    /// 配置权限步骤 1 添加权限名称 2. 添加权限定义 3. 编辑本地化文件 zh-Hans.json文件
    /// </remarks>
    public class TigerPermissionDefinitionProvider : PermissionDefinitionProvider
#pragma warning restore CS1570 // XML 注释出现 XML 格式错误
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            //Define your own permissions here.

        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<TigerResource>(name);
        }


    }
}
