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
        /// ��ȡ���вֿ�
        /// </summary>
        /// <returns></returns>
        Task<List<WarehouseDto>> GetAllAsync();
    }
}