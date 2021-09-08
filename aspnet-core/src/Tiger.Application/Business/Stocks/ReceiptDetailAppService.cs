using System;
using Tiger.Permissions;
using Tiger.Business.Stocks.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp;

namespace Tiger.Business.Stocks
{
    [RemoteService(false)]
    public class ReceiptDetailAppService : CrudAppService<ReceiptDetail, ReceiptDetailDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateReceiptDetailDto, CreateUpdateReceiptDetailDto>,
        IReceiptDetailAppService
    {
        //protected override string GetPolicyName { get; set; } = TigerPermissions.ReceiptDetail.Default;
        //protected override string GetListPolicyName { get; set; } = TigerPermissions.ReceiptDetail.Default;
        //protected override string CreatePolicyName { get; set; } = TigerPermissions.ReceiptDetail.Create;
        //protected override string UpdatePolicyName { get; set; } = TigerPermissions.ReceiptDetail.Update;
        //protected override string DeletePolicyName { get; set; } = TigerPermissions.ReceiptDetail.Delete;

        private readonly IReceiptDetailRepository _repository;
        
        public ReceiptDetailAppService(IReceiptDetailRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
