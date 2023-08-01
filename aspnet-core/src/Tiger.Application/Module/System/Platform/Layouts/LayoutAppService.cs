using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Module.System.Platform.Layouts.Dto;
using Tiger.Module.System.Platform.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.System.Platform.Layouts
{
    /// <summary>
    /// 布局
    /// </summary>
    [Authorize(PlatformPermissions.Layout.Default)]
    [RemoteService(IsEnabled = false)]
    public class LayoutAppService : ApplicationService, ILayoutAppService
    {
        public LayoutAppService(ILayoutRepository layoutRepository)
        {
            LayoutRepository=layoutRepository;
        }

        protected ILayoutRepository LayoutRepository { get; }

        /// <summary>
        /// 创建布局
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        [Authorize(PlatformPermissions.Layout.Create)]
        public async Task<LayoutDto> CreateAsync(LayoutCreateDto input)
        {
            var layout = await LayoutRepository.FindByNameAsync(input.Name);
            if ( layout != null)
            {
                throw new UserFriendlyException(L["DuplicateLayout", input.Name]);
            }

            layout = new Layout(
                GuidGenerator.Create(),
                input.Path,
                input.Name,
                input.Description,
                input.DataId,
                input.Framework,
                input.Redirect,
                input.Description,
                CurrentTenant.Id
                );
            layout = await LayoutRepository.InsertAsync( layout );
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<Layout, LayoutDto>(layout);

        }

        [Authorize(PlatformPermissions.Layout.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            var layout = await LayoutRepository.GetAsync(id);
            //if ( await LayoutRepository.AnyMenuAsync(layout.Id))
            //{
            //    throw new UserFriendlyException($"不能删除存在菜单的布局！");
            //}

            await LayoutRepository.DeleteAsync(layout);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 获取所有布局
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<LayoutDto>> GetAllListAsync()
        {   
            var layouts = await LayoutRepository.GetListAsync();

            return new ListResultDto<LayoutDto>(
                ObjectMapper.Map<List<Layout>, List<LayoutDto>>(layouts));
        }

        /// <summary>
        /// 根据id获取布局
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<LayoutDto> GetAsync(Guid id)
        {
            var layout = await LayoutRepository.GetAsync(id);

            return ObjectMapper.Map<Layout, LayoutDto>(layout);

        }

        /// <summary>
        /// 分页查询布局列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<PagedResultDto<LayoutDto>> GetListAsync(GetLayoutListInput input)
        {
            var count = await LayoutRepository.GetCountAsync(input.Framework, input.Filter);

            var layouts = await LayoutRepository.GetPagedListAsync(
                input.Framework, input.Filter,
                input.Sorting, input.Reverse,
                false, input.SkipCount, input.MaxResultCount);

            return new PagedResultDto<LayoutDto>(count,
                ObjectMapper.Map<List<Layout>, List<LayoutDto>>(layouts));
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新布局
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(PlatformPermissions.Layout.Update)]
        public async Task<LayoutDto> UpdateAsync(Guid id, LayoutUpdateDto input)
        {   
            var layout =await LayoutRepository.GetAsync(id);
            layout.Name = input.Name;
            layout.DisplayName = input.DisplayName;
            layout.Description = input.Description;
            layout.Path = input.Path;
            layout.Redirect = input.Redirect;
            layout = await LayoutRepository.UpdateAsync(layout);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<Layout, LayoutDto>(layout);

        }
    }
}
