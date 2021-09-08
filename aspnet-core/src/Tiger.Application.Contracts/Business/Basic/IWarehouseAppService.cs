using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiger.Basic.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Basic
{
    public interface IWarehouseAppService :
        ICrudAppService< 
            WarehouseDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            WarehouseCreateDto,
            WarehouseUpdateDto>
    {

        /// <summary>
        /// 获取所有仓库
        /// </summary>
        /// <returns></returns>
        Task<List<WarehouseDto>> GetAllAsync();
    }
}