/**
 * 名    称：IAuthorAppService   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/17 1:20:50       
 * 说    明: 
 * 
 */


using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Books
{
    /// <summary>
    /// 作者应用服务接口
    /// </summary>
    /// <remarks>
    /// `IApplicationService` 是一个常规接口, 所有应用服务都继承自它, 所以 ABP 框架可以识别它们.
    /// 在 `Author` 实体中定义标准方法用于CRUD操作.
    /// `PagedResultDto` 是一个ABP框架中预定义的 DTO 类. 它拥有一个 `Items` 集合 和一个 `TotalCount` 属性, 用于返回分页结果.
    /// 优先从 `CreateAsync` 方法返回 `AuthorDto` (新创建的作者), 虽然在这个程序中没有这么做 - 这里只是展示一种不同用法.
    /// </remarks>
    public interface IAuthorAppService : IApplicationService
    {
        Task<AuthorDto> GetAsync(Guid id);

        Task<PagedResultDto<AuthorDto>> GetListAsync(GetAuthorListDto input);

        Task<AuthorDto> CreateAsync(CreateAuthorDto input);

        Task UpdateAsync(Guid id, UpdateAuthorDto input);

        Task DeleteAsync(Guid id);
    }
}
