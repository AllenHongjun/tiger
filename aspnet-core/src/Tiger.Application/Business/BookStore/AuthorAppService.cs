/**
 * 类    名：AuthorAppService   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/17 1:28:39       
 * 说    明: 
 * 
 */

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Business.Demo;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tiger.Books
{
    /// <summary>
    /// 实现 author 的服务层
    /// </summary>
    [RemoteService(true)]
    [ApiExplorerSettings(GroupName = "admin")]
    [Authorize(BookStorePermissions.Authors.Default)]
    public class AuthorAppService : TigerAppService, IAuthorAppService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly AuthorManager _authorManager;

        public AuthorAppService(
            IAuthorRepository authorRepository,
            AuthorManager authorManager)
        {
            _authorRepository = authorRepository;
            _authorManager = authorManager;
        }

        #region 作者管理
        /// <summary>
        /// 添加作者
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(BookStorePermissions.Authors.Create)]
        public async Task<AuthorDto> CreateAsync(CreateAuthorDto input)
        {   
            // Validation 表单数据验证

            // 数据逻辑验证错误抛出

            var author = await _authorManager.CreateAsync(
                    input.Name,
                    input.BirthDate,
                    input.ShortBio
                );

            await _authorRepository.InsertAsync(author);

            return ObjectMapper.Map<Author, AuthorDto>(author);
        }

        /// <summary>
        /// 删除作者
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(BookStorePermissions.Authors.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _authorRepository.DeleteAsync(id);
        }

        /// <summary>
        /// 获取作者明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AuthorDto> GetAsync(Guid id)
        {
            var author = await _authorRepository.GetAsync(id);
            return ObjectMapper.Map<Author, AuthorDto>(author);
        }

        /// <summary>
        /// 获取作者列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<AuthorDto>> GetListAsync(GetAuthorListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Author.Name);
            }

            var authors = await _authorRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = await AsyncExecuter.CountAsync<Author>(
                _authorRepository.WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    author => author.Name.Contains(input.Filter) && author.Books.Any()
                )
            );

            return new PagedResultDto<AuthorDto>(
                totalCount,
                ObjectMapper.Map<List<Author>, List<AuthorDto>>(authors)
            );
        }

        /// <summary>
        /// 修改作者信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(BookStorePermissions.Authors.Update)]
        public async Task UpdateAsync(Guid id, UpdateAuthorDto input)
        {
            var author = await _authorRepository.GetAsync(id);

            if (author.Name != input.Name)
            {
                await _authorManager.ChangeNameAsync(author, input.Name);
            }

            author.BirthDate = input.BirthDate;
            author.ShortBio = input.ShortBio;

            await _authorRepository.UpdateAsync(author);
        } 
        #endregion

    }
}
