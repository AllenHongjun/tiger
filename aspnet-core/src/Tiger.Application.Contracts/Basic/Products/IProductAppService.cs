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

    }
    
}
