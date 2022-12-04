using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Identity.OrganizationUnits.Dto;
using Tiger.Volo.Abp.Identity.Users;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.Identity;
using Volo.Abp.Sms;

namespace Tiger.Volo.Abp.Identity
{
    [RemoteService(IsEnabled = false)]
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IIdentityUserAppService),
        typeof(IdentityUserAppService),
        typeof(ITigerIdentityUserAppService),
        typeof(TigerIdentityUserAppService))]
    public class TigerIdentityUserAppService : IdentityUserAppService, ITigerIdentityUserAppService
    {
        //private readonly IStringLocalizer<HelloAbpResource> _localizer;

        private readonly IBlobContainer _blobContainer;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly IIdentityUserRepository _identityUserRepository;
        public TigerIdentityUserAppService(
            IdentityUserManager userManager, 
            IIdentityUserRepository userRepository, 
            IIdentityRoleRepository roleRepository,
            IBlobContainer blobContainer,
            IEmailSender emailSender,
            ISmsSender smsSender,
            IIdentityUserRepository identityUserRepository
            ) : base(userManager, userRepository, roleRepository)
        {
            _blobContainer = blobContainer;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _identityUserRepository = identityUserRepository;
        }




        #region 测试代码
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <returns></returns>
        public async Task SmsSend()
        {
            await _smsSender.SendAsync(
                "15958456864",        // target phone number
                "This is test sms..."   // message text
            );
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <returns></returns>
        public async Task EmailSend()
        {
            await _emailSender.SendAsync(
                "hongjy1991@gmail.com",     // target email address
                "这是一封测试邮件",         // subject
                "这里是邮件的内容"  // email body
            );
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public async Task SaveBytesAsync(byte[] bytes)
        {
            await _blobContainer.SaveAsync("my-blob-1", bytes);
        }

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <returns></returns>
        public async Task<byte[]> GetBytesAsync()
        {
            return await _blobContainer.GetAllBytesOrNullAsync("my-blob-1");
        }

        /*
         子类继承父类来，来扩展abp原有的方法
         可以从写覆盖原来的方法。也可以扩展原有的功能。
         
         */
        #endregion

        /// <summary>
        /// 将用户关联组织机构
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ouId"></param>
        /// <returns></returns>
        public virtual async Task AddToOrganizationUnitsAsync(Guid userId, List<Guid> ouId)
        {
            await UserManager.SetOrganizationUnitsAsync(userId, ouId.ToArray());
        }

        public override async Task<IdentityUserDto> CreateAsync(IdentityUserCreateDto input)
        {
            return await base.CreateAsync(input);
        }

        /// <summary>
        /// 添加用户，同时用户关联组织id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<IdentityUserDto> CreateAsync(IdentityUserOrgCreateDto input)
        {
            var identity = await CreateAsync(
                ObjectMapper.Map<IdentityUserOrgCreateDto, IdentityUserCreateDto>(input)
            );
            if (input.OrgIds != null)
            {
                await UserManager.SetOrganizationUnitsAsync(identity.Id, input.OrgIds.ToArray());
            }
            return identity;
        }

        /// <summary>
        /// 获取用户关联的组织机构
        /// </summary>
        /// <param name="id">组织机构id</param>
        /// <param name="includeDetails"></param>
        /// <returns></returns>
        public virtual async Task<ListResultDto<OrganizationUnitDto>> GetListOrganizationUnitsAsync(Guid id, bool includeDetails = false)
        {
            var list = await UserRepository.GetOrganizationUnitsAsync(id, includeDetails);
            return new ListResultDto<OrganizationUnitDto>(
                ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(list)
            );
        }

        /// <summary>
        /// 修改用户及用户关联的组织
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<IdentityUserDto> UpdateAsync(Guid id, IdentityUserOrgUpdateDto input)
        {   
            
            var update = ObjectMapper.Map<IdentityUserOrgUpdateDto, IdentityUserUpdateDto>(input);
            var result = await base.UpdateAsync(id, update);
            await UserManager.SetOrganizationUnitsAsync(result.Id, input.OrgIds.ToArray());
            return result;
        }

        //public virtual async Task<IdentityUserDto> UpdateAsync(Guid id)
        //{
        //    var result = await base.UpdateAsync(id, update);
        //    return result;
        //}

    }
}
