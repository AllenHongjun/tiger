using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Tiger.Business.Demo;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.System.Platform.Datas;

/// <summary>
/// �����ֵ�ִ�ʵ��
/// </summary>
public class DataRepository : EfCoreRepository<TigerDbContext, Data, Guid>, IDataRepository
{
    public DataRepository(
        IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    /// <summary>
    /// �������Ʋ�ѯ
    /// </summary>
    /// <param name="name"></param>
    /// <param name="includeDetails"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Data> FindByNameAsync(
        string name, 
        bool includeDetails = true, 
        CancellationToken cancellationToken = default)
    {
        return await DbSet.IncludeDetails(includeDetails)
            .Where(x => x.Name == name)
            .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
    }

    /// <summary>
    /// ��ѯ����
    /// </summary>
    /// <param name="parentId"></param>
    /// <param name="includeDetails"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<Data>> GetChildrenAsync(
        Guid? parentId, 
        bool includeDetails = false, 
        CancellationToken cancellationToken = default)
    {
        return await DbSet.Include(x => x.Items)
            // Ϊnull��ֵ C#Ҳ������ == �ж�
            .Where(x => x.ParentId == parentId)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    /// <summary>
    /// ��ѯ��ҳ������
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<int> GetCountAsync(
        string filter = "", 
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .WhereIf(!filter.IsNullOrWhiteSpace(), x => x.Code.Contains(filter) || x.Description.Contains(filter) || x.DisplayName.Contains(filter) || x.Name.Contains(filter))
            .CountAsync(cancellationToken);
    }

    /// <summary>
    /// ��ҳ��ѯ�����ֵ�
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="sorting"></param>
    /// <param name="includeDetails"></param>
    /// <param name="skipCount"></param>
    /// <param name="maxResultCount"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<Data>> GetPagedListAsync(
        string filter = "", 
        string sorting = "Code Desc", 
        bool includeDetails = false, 
        int skipCount = 0, 
        int maxResultCount = 10, 
        CancellationToken cancellationToken = default)
    {
        sorting ??= nameof(Data.Code);

        return await DbSet
            .IncludeDetails(includeDetails)
            .WhereIf(!filter.IsNullOrWhiteSpace(), x =>
                  x.Code.Contains(filter) || x.Description.Contains(filter) ||
                  x.DisplayName.Contains(filter) || x.Name.Contains(filter))
            .OrderBy(sorting)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    /// <summary>
    /// �����Ӽ�
    /// </summary>
    /// <remarks>
    /// ��д���෽������Ȼ���ȡ����
    /// </remarks>
    /// <returns></returns>
    public override IQueryable<Data> WithDetails()
    {
        return GetQueryable().IncludeDetails(); // Uses the extension method defined above
    }

}