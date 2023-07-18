﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Tiger.Module.OssManagement.Dto;
using Tiger.Module.OssManagement.Dtos;
using Tiger.Module.OssManagement.Permissions;
using Volo.Abp;

namespace Tiger.Module.OssManagement
{
    [RemoteService(IsEnabled = false)]
    [Authorize(AbpOssManagementPermissions.OssObject.Default)]
    public class OssObjectAppService : OssManagementApplicationServiceBase, IOssObjectAppService
    {
        protected FileUploadMerger Merger { get; }
        protected IOssContainerFactory OssContainerFactory { get; }

        public OssObjectAppService(
            FileUploadMerger merger,
            IOssContainerFactory ossContainerFactory)
        {
            Merger = merger;
            OssContainerFactory = ossContainerFactory;
        }

        [Authorize(AbpOssManagementPermissions.OssObject.Create)]
        public async virtual Task<OssObjectDto> CreateAsync(CreateOssObjectInput input)
        {
            // 内容为空时建立目录 || !input.File.ContentLength.HasValue
            if (input.File == null )
            {
                var oss = CreateOssContainer();
                var request = new CreateOssObjectRequest(
                    HttpUtility.UrlDecode(input.Bucket),
                    HttpUtility.UrlDecode(input.FileName),
                    Stream.Null,
                    HttpUtility.UrlDecode(input.Path));
                var ossObject = await oss.CreateObjectAsync(request);

                return ObjectMapper.Map<OssObject, OssObjectDto>(ossObject);
            }
            else
            {
                var ossObject = await Merger.MergeAsync(input);

                return ObjectMapper.Map<OssObject, OssObjectDto>(ossObject);
            }
        }

        [Authorize(AbpOssManagementPermissions.OssObject.Delete)]
        public async virtual Task BulkDeleteAsync(BulkDeleteOssObjectInput input)
        {
            var oss = CreateOssContainer();

            await oss.BulkDeleteObjectsAsync(input.Bucket, input.Objects, input.Path);
        }

        [Authorize(AbpOssManagementPermissions.OssObject.Delete)]
        public async virtual Task DeleteAsync(GetOssObjectInput input)
        {
            var oss = CreateOssContainer();

            await oss.DeleteObjectAsync(input.Bucket, input.Object, input.Path);
        }

        public async virtual Task<OssObjectDto> GetAsync(GetOssObjectInput input)
        {
            var oss = CreateOssContainer();

            var ossObject = await oss.GetObjectAsync(input.Bucket, input.Object, input.Path, input.MD5);

            return ObjectMapper.Map<OssObject, OssObjectDto>(ossObject);
        }

        public async virtual Task<Stream> GetContentAsync(GetOssObjectInput input)
        {
            var oss = CreateOssContainer();

            var ossObject = await oss.GetObjectAsync(input.Bucket, input.Object, input.Path, input.MD5);

            //return new Stream(ossObject.Content, ossObject.Name);
            return ossObject.Content;
        }

        protected virtual IOssContainer CreateOssContainer()
        {
            return OssContainerFactory.Create();
        }
    }
}