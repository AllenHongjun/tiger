using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Basic.Products
{
    public interface IProductAppService :
        ICrudAppService< //Defines CRUD methods
            ProductDto, //Used to show 
            Guid, //Primary key 
            GetProductListDto,
            CreateUpdateProductDto, //Used for paging/sorting
            CreateUpdateProductDto> //Used to create/update 
    {
        // TODO: Load state from previously suspended application

        // TODO: 显示隐藏

        // TODO: 上架下架

        // TODO: 商品审核

        // TODO: 批量操作 批量审核 批量删除

        // TODO: 导入 导出 
    }

}
