using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiger.Business.Stocks;
using Tiger.Domain.CoreModule.Utilities;
using Tiger.Permissions;
using Tiger.Stock.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Stock
{
    [RemoteService(false)]
    public class ReceiptHeaderAppService : CrudAppService<ReceiptHeader, ReceiptHeaderDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateReceiptHeaderDto, CreateUpdateReceiptHeaderDto>,
        IReceiptHeaderAppService
    {
        private readonly IReceiptHeaderRepository _repository;
        private readonly IRepository<ReceiptDetail,Guid> _receiptDetailRepository;
        private readonly IReverseDetailRepository _reverseDetailRepository;
        
        public ReceiptHeaderAppService(
            IReceiptHeaderRepository repository,
            IRepository<ReceiptDetail, Guid> receiptDetailRepository,


            IReverseDetailRepository reverseDetailRepository) : base(repository)
        {
            _repository = repository;
            _receiptDetailRepository = receiptDetailRepository;

            _reverseDetailRepository = reverseDetailRepository;
        }

        public override async Task<ReceiptHeaderDto> CreateAsync(CreateUpdateReceiptHeaderDto input)
        {
            input.Code = Utility.CreateOrderID("CGRK");
            foreach (var item in input.ReceiptDetails)
            {
                item.Id = GuidGenerator.Create();
            }
            return await base.CreateAsync(input);
        }

        public override async Task<ReceiptHeaderDto> UpdateAsync(Guid id, CreateUpdateReceiptHeaderDto input)
        {
            await DeleteDetail(id);
            foreach (var item in input.ReceiptDetails)
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

        private async Task  DeleteDetail(Guid id)
        {
            var receiptHeader = await _repository.GetAsync(id);
            foreach (var receiptDetail in receiptHeader.ReceiptDetails)
            {
                await _receiptDetailRepository.DeleteAsync(receiptDetail.Id, true);
            }
        }

        public override async Task<ReceiptHeaderDto> GetAsync(Guid id)
        {
            var query = await _repository.GetAsync(id);
            return ObjectMapper.Map<ReceiptHeader, ReceiptHeaderDto>(query); 
        }
       
    }
}
