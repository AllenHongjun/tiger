using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.System.Platform.Datas.Dtos;
using Volo.Abp.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Tiger.Module.System.Platform.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using Tiger.Module.System.Platform.Utils;

namespace Tiger.Module.System.Platform.Datas;


/// <summary>
/// 数据字典
/// </summary>
//[Authorize(PlatformPermissions.DataDictionary.Default)]
[RemoteService(IsEnabled = false)]
public class DataAppService : CrudAppService<Data, DataDto, Guid, DataGetListInput, CreateUpdateDataDto, CreateUpdateDataDto>,
    IDataAppService
{
    

    protected IDataRepository DataRepository { get; }

    public DataAppService(IDataRepository dataRepository) : base(dataRepository)
    {
        DataRepository = dataRepository;
    }


    
    /// <summary>
    /// 根据名称查询数据字典
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<DataDto> GetAsync(string name)
    {
        var data = await DataRepository.FindByNameAsync(name);

        return ObjectMapper.Map<Data,DataDto>(data);
    }

    /// <summary>
    /// 查询所有数据字典
    /// </summary>
    /// <returns></returns>
    public async Task<ListResultDto<DataDto>> GetAllAsync()
    {
        var datas = await DataRepository.GetListAsync(includeDetails: false);

        return new ListResultDto<DataDto>(
            ObjectMapper.Map<List<Data>, List<DataDto>>(datas));
    }

    /// <summary>
    /// 分页查询数据字典
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public override async Task<PagedResultDto<DataDto>> GetListAsync(DataGetListInput input)
    {
        var count = await DataRepository.GetCountAsync(input.Filter);

        var datas = await DataRepository.GetPagedListAsync(input.Filter,
            input.Sorting,
            false, input.SkipCount, input.MaxResultCount);

        return new PagedResultDto<DataDto>(count, ObjectMapper.Map<List<Data>, List<DataDto>>(datas));
    }

    /// <summary>
    /// 创建数据字典
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="UserFriendlyException"></exception>
    public async Task<DataDto> CreateAsync(DataCreateDto input)
    {
        var data = await DataRepository.FindByNameAsync(input.Name);
        if (data != null)
        {
            throw new UserFriendlyException(L["DuplicateDate"], input.Name);
        }

        string code = string.Empty;
        var children = await DataRepository.GetChildrenAsync(input.ParentId);
        if (children.Any())
        {
            var lastChildren = children.OrderBy(x => x.Code).FirstOrDefault();
            code = CodeNumberGenerator.CalculateNextCode(lastChildren.Code);
        }
        else
        {
            var parentData = input.ParentId !=null
                ? await DataRepository.GetAsync(input.ParentId.Value)
                : null;

            code = CodeNumberGenerator.AppendCode(parentData?.Code, CodeNumberGenerator.CreateCode(1));
        }

        data = new Data(GuidGenerator.Create(),
            input.Name,
            code,
            input.DisplayName,
            input.Description,
            input.ParentId,
            CurrentTenant.Id);

        data = await DataRepository.InsertAsync(data);
        await CurrentUnitOfWork.SaveChangesAsync();

        return ObjectMapper.Map<Data, DataDto>(data);

    }

    /// <summary>
    /// 更新数据字典
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <remarks>
    /// 修改的时候同时可以修改数据字典数据
    /// </remarks>
    public async Task<DataDto> UpdateAsync(Guid id, DataUpdateDto input)
    {
        var data = await DataRepository.GetAsync(id);

        data.Name = input.Name;
        data.DisplayName = input.DisplayName;
        data.Description = input.Description;
        data = await DataRepository.UpdateAsync(data);
        await CurrentUnitOfWork.SaveChangesAsync();

        return ObjectMapper.Map<Data, DataDto>(data);

    }

    /// <summary>
    /// 移除数据字典
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="UserFriendlyException"></exception>
    public override async  Task DeleteAsync(Guid id)
    {
        var data = await DataRepository.GetAsync(id);

        var children = await DataRepository.GetChildrenAsync(data.Id);
        if (children.Any())
        {
            throw new UserFriendlyException(L["UnableRemoveHasChildNode"]);
        }
        await DataRepository.DeleteAsync(data);
        await CurrentUnitOfWork.SaveChangesAsync();
    }


    /// <summary>
    /// 移动数据字典
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<DataDto> MoveAsync(Guid id, DataMoveDto input)
    {
        var data = await DataRepository.GetAsync(id);
        data.ParentId = input.ParentId;
        data = await DataRepository.UpdateAsync(data);
        await CurrentUnitOfWork.SaveChangesAsync();
        return ObjectMapper.Map<Data, DataDto>(data);
    }

    /// <summary>
    /// 创建字典数据
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task CreateItemAsync(Guid id, DataItemCreateDto input)
    {
        var data = await DataRepository.GetAsync(id);
        var dataItem = data.FindItem(input.Name);
        if (dataItem != null)
        {
            throw new UserFriendlyException(L["DuplicateDataItem"], input.Name);
        }

        data.AddItem(GuidGenerator,
            input.Name,
            input.DisplayName,
            input.DefaultValue,
            input.ValueType,
            input.Description,
            input.AllowBeNull);

        await DataRepository.UpdateAsync(data);
        await CurrentUnitOfWork.SaveChangesAsync();
    }

    /// <summary>
    /// 更新字典数据
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="UserFriendlyException"></exception>
    public async Task UpdateItemAsync(Guid id, string name, DataItemUpdateDto input)
    {

        var data = await DataRepository.GetAsync(id);
        var dataItem = data.FindItem(name);
        if (dataItem == null)
        {
            throw new UserFriendlyException(L["DataItemNotFound"], name);
        }

        if (!String.Equals(dataItem.DefaultValue, input.DefaultValue, StringComparison.InvariantCultureIgnoreCase))
        {
            dataItem.DefaultValue = input.DefaultValue;
        }
        dataItem.DisplayName = input.DisplayName;
        dataItem.Description = input.Description;
        dataItem.AllowBeNull = input.AllowBeNull;

        await DataRepository.UpdateAsync(data);
        await CurrentUnitOfWork.SaveChangesAsync();
    }

    /// <summary>
    /// 根据名称删除字典数据
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    //[Authorize(PlatformPermissions.DataDictionary.ManageItems)]
    public async virtual Task DeleteItemAsync(Guid id, string name)
    {
        var data = await DataRepository.GetAsync(id);
        data.RemoveItem(name);

        await DataRepository.UpdateAsync(data);
        await CurrentUnitOfWork.SaveChangesAsync();
    }

    
}
