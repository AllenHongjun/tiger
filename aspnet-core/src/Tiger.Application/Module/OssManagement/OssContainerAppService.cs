using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Module.OssManagement.Dto;
using Tiger.Module.OssManagement.Dtos;
using Tiger.Module.OssManagement.Permissions;
using Volo.Abp;

namespace Tiger.Module.OssManagement
{
    /// <summary>
    /// 容器服务
    /// </summary>
    [RemoteService(IsEnabled = false)]
    [Authorize(AbpOssManagementPermissions.Container.Default)]
    public class OssContainerAppService : OssManagementApplicationServiceBase, IOssContainerAppService
    {
        protected IOssContainerFactory OssContainerFactory { get; }

        public OssContainerAppService(
            IOssContainerFactory ossContainerFactory)
        {
            OssContainerFactory = ossContainerFactory;
        }

        /// <summary>
        /// 创建容器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Authorize(AbpOssManagementPermissions.Container.Create)]
        public async virtual Task<OssContainerDto> CreateAsync(string name)
        {
            var oss = CreateOssContainer();
            var container = await oss.CreateAsync(name);

            return ObjectMapper.Map<OssContainer, OssContainerDto>(container);
        }

        /// <summary>
        /// 删除容器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Authorize(AbpOssManagementPermissions.Container.Delete)]
        public async virtual Task DeleteAsync(string name)
        {
            var oss = CreateOssContainer();

            await oss.DeleteAsync(name);
        }

        /// <summary>
        /// 根据名称查询容器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async virtual Task<OssContainerDto> GetAsync(string name)
        {
            var oss = CreateOssContainer();
            var container = await oss.GetAsync(name);

            return ObjectMapper.Map<OssContainer, OssContainerDto>(container);
        }

        /// <summary>
        /// 获取容器列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async virtual Task<OssContainersResultDto> GetListAsync(GetOssContainersInput input)
        {
            var oss = CreateOssContainer();

            var containerResponse = await oss.GetListAsync(
                input.Prefix, input.Marker, input.SkipCount, input.MaxResultCount);

            return ObjectMapper.Map<GetOssContainersResponse, OssContainersResultDto>(containerResponse);
        }

        /// <summary>
        /// 获取容器的对象列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async virtual Task<OssObjectsResultDto> GetObjectListAsync(GetOssObjectsInput input)
        {
            var oss = CreateOssContainer();

            var ossObjectResponse = await oss.GetObjectsAsync(
                input.Bucket, input.Prefix, input.Marker,
                input.Delimiter, input.EncodingType, input.MD5,
                input.SkipCount, input.MaxResultCount);

            return ObjectMapper.Map<GetOssObjectsResponse, OssObjectsResultDto>(ossObjectResponse);
        }

        protected virtual IOssContainer CreateOssContainer()
        {
            return OssContainerFactory.Create();
        }
    }
}
