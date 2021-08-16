/**
 * 类    名：TestAppService   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 21:37:03       
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
    /// 类名不匹配,你需要手动公开服务接口:  替换默认的服务
    /// 
    /// 依赖注入系统允许为一个接口注册多个服务. 注入接口时会解析最后一个注入的服务. 显式的替换服务是一个好习惯.
    /// </summary>
    [ExposeServices(typeof(IIdentityUserAppService))]
    [Dependency(ReplaceServices = true)]
    public class TestAppService : IIdentityUserAppService, ITransientDependency
    {
        public TestAppService()
        {
        }

        //...
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
