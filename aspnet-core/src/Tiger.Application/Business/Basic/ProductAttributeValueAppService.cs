using System;
using Tiger.Permissions;
using Tiger.Business.Basic.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Business.Basic
{
    public class ProductAttributeValueAppService : CrudAppService<ProductAttributeValue, ProductAttributeValueDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateProductAttributeValueDto, CreateUpdateProductAttributeValueDto>,
        IProductAttributeValueAppService
    {
        protected override string GetPolicyName { get; set; } = TigerPermissions.ProductAttributeValue.Default;
        protected override string GetListPolicyName { get; set; } = TigerPermissions.ProductAttributeValue.Default;
        protected override string CreatePolicyName { get; set; } = TigerPermissions.ProductAttributeValue.Create;
        protected override string UpdatePolicyName { get; set; } = TigerPermissions.ProductAttributeValue.Update;
        protected override string DeletePolicyName { get; set; } = TigerPermissions.ProductAttributeValue.Delete;

        private readonly IProductAttributeValueRepository _repository;
        
        public ProductAttributeValueAppService(IProductAttributeValueRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
