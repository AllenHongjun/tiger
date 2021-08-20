using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Basic.Products;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Basic
{
    public class ProductAppService
    :
        CrudAppService<
            Product, //The Book entity
            ProductDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateProductDto>, //Used to create/update a book
        IProductAppService //implement the IBookAppService
    {
        public ProductAppService(IRepository<Product, Guid> repository)
            : base(repository)
        {

        }
    }
}
