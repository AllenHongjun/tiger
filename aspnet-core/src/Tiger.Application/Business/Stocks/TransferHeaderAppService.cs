using System;
using System.Threading.Tasks;
using Tiger.Domain.CoreModule.Utilities;
using Tiger.Permissions;
using Tiger.Stock.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Stock
{
    [RemoteService(false)]
    public class TransferHeaderAppService : CrudAppService<TransferHeader, TransferHeaderDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateTransferHeaderDto, CreateUpdateTransferHeaderDto>,
        ITransferHeaderAppService
    {
        //protected override string GetPolicyName { get; set; } = TigerPermissions.TransferHeader.Default;
        //protected override string GetListPolicyName { get; set; } = TigerPermissions.TransferHeader.Default;
        //protected override string CreatePolicyName { get; set; } = TigerPermissions.TransferHeader.Create;
        //protected override string UpdatePolicyName { get; set; } = TigerPermissions.TransferHeader.Update;
        //protected override string DeletePolicyName { get; set; } = TigerPermissions.TransferHeader.Delete;

        private readonly ITransferHeaderRepository _repository;
        private readonly ITransferDetailRepository _transferDetailRepository;


        public TransferHeaderAppService(
            ITransferHeaderRepository repository,
            ITransferDetailRepository transferDetailRepository
            ) : base(repository)
        {
            _repository = repository;
            _transferDetailRepository = transferDetailRepository;
        }

        public override async Task<TransferHeaderDto> GetAsync(Guid id)
        {
            var query = await _repository.GetAsync(id);

            return ObjectMapper.Map<TransferHeader, TransferHeaderDto>(query);
        }

        public override async Task<TransferHeaderDto> CreateAsync(CreateUpdateTransferHeaderDto input)
        {
            input.Code = Utility.CreateOrderID("CK");
            foreach (var item in input.TransferDetails)
            {
                item.Id = GuidGenerator.Create();
            }

            //input.TotalQty = input.TransferDetails.Sum(x => x.TotalQty);
            //input.TotalWeight = input.TransferDetails.Sum(x => x.TotalQty);
            return await base.CreateAsync(input);
        }

        public override async Task<TransferHeaderDto> UpdateAsync(Guid id, CreateUpdateTransferHeaderDto input)
        {
            await DeleteDetail(id);
            foreach (var item in input.TransferDetails)
            {
                item.Id = GuidGenerator.Create();
            }
            return await base.UpdateAsync(id, input);
        }


        public override async Task DeleteAsync(Guid id)
        {
            await DeleteDetail(id);
            await base.DeleteAsync(id);
        }

        private async Task DeleteDetail(Guid id)
        {
            var TransferHeader = await _repository.GetAsync(id);
            foreach (var receiptDetail in TransferHeader.TransferDetails)
            {
                await _transferDetailRepository.DeleteAsync(receiptDetail.Id, true);
            }
        }
    }
}
