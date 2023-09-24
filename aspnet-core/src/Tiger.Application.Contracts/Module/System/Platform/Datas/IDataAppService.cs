using System;
using System.Threading.Tasks;
using Tiger.Module.System.Platform.Datas.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.System.Platform.Datas;

/// <summary>
/// 数据字典接口
/// </summary>
public interface IDataAppService :
    ICrudAppService< 
        LanguageTextDto, 
        Guid, 
        DataGetListInput,
        DataCreateDto,
        DataUpdateDto>
{

    /// <summary>
    /// 根据名称查询数据字典
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<LanguageTextDto> GetAsync(string name);

    /// <summary>
    /// 查询所有
    /// </summary>
    /// <returns></returns>
    Task<ListResultDto<LanguageTextDto>> GetAllAsync();

    /// <summary>
    /// 移动数据字典
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<LanguageTextDto> MoveAsync(Guid id, DataMoveDto input);

    /// <summary>
    /// 创建字典数据
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task CreateItemAsync(Guid id, DataItemCreateDto input);

    /// <summary>
    /// 更新字典数据
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task UpdateItemAsync(Guid id, string name, DataItemUpdateDto input);

    /// <summary>
    /// 删除字典数据
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    Task DeleteItemAsync(Guid id, string name);

}