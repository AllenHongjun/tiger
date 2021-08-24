/**
 * 类    名：MyIdentityUserAppService   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 21:34:12       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace Tiger.Books.Demo
{
    /// <summary>
    /// 如果给定的服务定义了接口,像 IdentityUserAppService 类实现了 IIdentityUserAppService 接口,你可以为这个接口创建自己的实现并且替换当前的实现. 例如:
    /// 
    /// 
    /// MyIdentityUserAppService 通过命名约定替换了 IIdentityUserAppService 的当前实现. 如果你的类名不匹配,你需要手动公开服务接口:
    /// </summary>
    public class MyIdentityUserAppService : IIdentityUserAppService, ITransientDependency
    {
        public Task<IdentityUserDto> CreateAsync(IdentityUserCreateDto input)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUserDto> FindByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUserDto> FindByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<ListResultDto<IdentityRoleDto>> GetAssignableRolesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUserDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResultDto<IdentityUserDto>> GetListAsync(GetIdentityUsersInput input)
        {
            throw new NotImplementedException();
        }

        public Task<ListResultDto<IdentityRoleDto>> GetRolesAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUserDto> UpdateAsync(Guid id, IdentityUserUpdateDto input)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRolesAsync(Guid id, IdentityUserUpdateRolesDto input)
        {
            throw new NotImplementedException();
        }
    }
}
