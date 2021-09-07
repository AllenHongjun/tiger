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
    public class CheckHeaderAppService : CrudAppService<CheckHeader, CheckHeaderDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateCheckHeaderDto, CreateUpdateCheckHeaderDto>,
        ICheckHeaderAppService
    {
        //protected override string GetPolicyName { get; set; } = TigerPermissions.CheckHeader.Default;
        //protected override string GetListPolicyName { get; set; } = TigerPermissions.CheckHeader.Default;
        //protected override string CreatePolicyName { get; set; } = TigerPermissions.CheckHeader.Create;
        //protected override string UpdatePolicyName { get; set; } = TigerPermissions.CheckHeader.Update;
        //protected override string DeletePolicyName { get; set; } = TigerPermissions.CheckHeader.Delete;

        private readonly ICheckHeaderRepository _repository;
        private readonly ICheckDetailRepository _checkDetailRepository;


        public CheckHeaderAppService(
            ICheckHeaderRepository repository,
            ICheckDetailRepository checkDetailRepository
            ) : base(repository)
        {
            _repository = repository;
            _checkDetailRepository = checkDetailRepository;
        }

        public override async Task<CheckHeaderDto> GetAsync(Guid id)
        {
            var query = await _repository.GetAsync(id);

            return ObjectMapper.Map<CheckHeader, CheckHeaderDto>(query);
        }

        public override async Task<CheckHeaderDto> CreateAsync(CreateUpdateCheckHeaderDto input)
        {
            input.Code = Utility.CreateOrderID("CK");
            foreach (var item in input.CheckDetails)
            {
                item.Id = GuidGenerator.Create();
            }

            //input.TotalQty = input.CheckDetails.Sum(x => x.TotalQty);
            //input.TotalWeight = input.CheckDetails.Sum(x => x.TotalQty);
            return await base.CreateAsync(input);
        }

        public override async Task<CheckHeaderDto> UpdateAsync(Guid id, CreateUpdateCheckHeaderDto input)
        {
            await DeleteDetail(id);
            foreach (var item in input.CheckDetails)
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
            var CheckHeader = await _repository.GetAsync(id);
            foreach (var receiptDetail in CheckHeader.CheckDetails)
            {
                await _checkDetailRepository.DeleteAsync(receiptDetail.Id, true);
            }
        }
    }
}
