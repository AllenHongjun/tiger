using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Tiger.Module.System.Area.Dtos;
using Tiger.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.System.Area;


/// <summary>
/// 区域服务
/// </summary>
[RemoteService(IsEnabled = false)]
public class RegionAppService : CrudAppService<Region, RegionDto, int, RegionGetListInput, CreateUpdateRegionDto, CreateUpdateRegionDto>,
    IRegionAppService
{
    #region 构造函数

    protected IRegionRepository RegionRepository { get; }

    public RegionAppService(IRegionRepository regionRepository) : base(regionRepository)
    {
        RegionRepository = regionRepository;
    } 
    #endregion

    #region CRUD
    protected async Task<IQueryable<Region>> CreateFilteredQueryAsync(RegionGetListInput input)
    {
        // TODO: AbpHelper generated
        return  RegionRepository
            .WhereIf(input.Level != null, x => x.Level == input.Level)
            .WhereIf(input.ParentCode != null, x => x.ParentCode == input.ParentCode)
            .WhereIf(input.AreaCode != null, x => x.AreaCode == input.AreaCode)
            .WhereIf(!input.ZipCode.IsNullOrWhiteSpace(), x => x.ZipCode.Contains(input.ZipCode))
            .WhereIf(!input.CityCode.IsNullOrWhiteSpace(), x => x.CityCode.Contains(input.CityCode))
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
            .WhereIf(!input.ShortName.IsNullOrWhiteSpace(), x => x.ShortName.Contains(input.ShortName))
            .WhereIf(!input.MergerName.IsNullOrWhiteSpace(), x => x.MergerName.Contains(input.MergerName))
            .WhereIf(!input.Pinyin.IsNullOrWhiteSpace(), x => x.Pinyin.Contains(input.Pinyin))
            .WhereIf(input.Lng != null, x => x.Lng == input.Lng)
            .WhereIf(input.Lat != null, x => x.Lat == input.Lat)
            ;
    }

    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="UserFriendlyException"></exception>
    public override async Task<RegionDto> CreateAsync(CreateUpdateRegionDto input)
    {
        var region = await RegionRepository.FindAsync(x => x.Name.Equals(input.Name));
        if (region != null)
        {
            throw new UserFriendlyException(L["DuplicateRegion"], input.Name);
        }

        region = new Region(
            input.Level, input.ParentCode, input.AreaCode, input.ZipCode,
            input.CityCode, input.Name, input.ShortName, input.MergerName,
            input.Pinyin, input.Lng, input.Lat);

        region = await RegionRepository.InsertAsync(region);
        await CurrentUnitOfWork.SaveChangesAsync();

        return ObjectMapper.Map<Region, RegionDto>(region);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="input"></param>
    /// <returns></returns>
    public override async Task<RegionDto> UpdateAsync(int id, CreateUpdateRegionDto input)
    {
        var region = await RegionRepository.GetAsync(id);

        region.Name = input.Name;
        region.Level = input.Level;
        region.ParentCode = input.ParentCode;
        region.AreaCode = input.AreaCode;
        region.ZipCode = input.ZipCode;
        region.CityCode = input.CityCode;
        region.ShortName = input.ShortName;
        region.MergerName = input.MergerName;
        region.Pinyin = input.Pinyin;
        region.Lng = input.Lng;
        region.Lat = input.Lat;

        region = await RegionRepository.UpdateAsync(region);
        await CurrentUnitOfWork.SaveChangesAsync();

        return ObjectMapper.Map<Region, RegionDto>(region);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public override async Task DeleteAsync(int id)
    {
        var region = await RegionRepository?.GetAsync(id);
        
        await RegionRepository.DeleteAsync(region);
        await CurrentUnitOfWork.SaveChangesAsync();
    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public override async Task<RegionDto> GetAsync(int id)
    {
        var region = await RegionRepository.GetAsync(id);

        return ObjectMapper.Map<Region,RegionDto>(region);
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public override async Task<PagedResultDto<RegionDto>> GetListAsync(RegionGetListInput input)
    {
        var count =  RegionRepository.WhereIf(input.ParentCode > -1, x => x.ParentCode == input.ParentCode).Count();

        var regions = RegionRepository.WhereIf(input.ParentCode > -1, x => x.ParentCode == input.ParentCode)
            .Skip(input.SkipCount).Take(input.MaxResultCount)
            .OrderBy(input.Sorting ?? "Name ASC")
            .ToList();

        return new PagedResultDto<RegionDto>(count,
            ObjectMapper.Map<List<Region>, List<RegionDto>>(regions));
    } 

    #endregion

    public ListResultDto<RegionDto> GetListByParentCode(long parentCode)
    {
        var regions = RegionRepository.Where(x => x.ParentCode.Equals(parentCode)).ToList();
        return new ListResultDto<RegionDto>(
            ObjectMapper.Map<List<Region>, List<RegionDto>>(regions));
    }

    public ListResultDto<RegionDto> GetAllList()
    {
        var regions = RegionRepository.Where(x => x.Level < 3).ToList();
        return new ListResultDto<RegionDto>(
            ObjectMapper.Map<List<Region>, List<RegionDto>>(regions));
    }

}
