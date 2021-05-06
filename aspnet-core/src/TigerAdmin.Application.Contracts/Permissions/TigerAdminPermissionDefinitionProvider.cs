using TigerAdmin.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace TigerAdmin.Permissions
{
    public class TigerAdminPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(TigerAdminPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(TigerAdminPermissions.MyPermission1, L("Permission:MyPermission1"));
            var booksPermission = myGroup.AddPermission(TigerAdminPermissions.Books.Default, L("Permission:Books"));
            booksPermission.AddChild(TigerAdminPermissions.Books.Create, L("Permission:Book.Create"));
            booksPermission.AddChild(TigerAdminPermissions.Books.Edit, L("Permission:Book.Edit"));
            booksPermission.AddChild(TigerAdminPermissions.Books.Delete, L("Permission:Book.Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<TigerAdminResource>(name);
        }
    }
}
