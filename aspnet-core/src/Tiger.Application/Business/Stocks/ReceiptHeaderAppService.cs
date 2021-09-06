using System;
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
        //protected override string GetPolicyName { get; set; } = TigerPermissions.ReceiptHeader.Default;
        //protected override string GetListPolicyName { get; set; } = TigerPermissions.ReceiptHeader.Default;
        //protected override string CreatePolicyName { get; set; } = TigerPermissions.ReceiptHeader.Create;
        //protected override string UpdatePolicyName { get; set; } = TigerPermissions.ReceiptHeader.Update;
        //protected override string DeletePolicyName { get; set; } = TigerPermissions.ReceiptHeader.Delete;

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
            DeleteDetail(id);

            //receiptHeader.ReceiptDetails.Clear();
            //await CurrentUnitOfWork.CompleteAsync();
            foreach (var item in input.ReceiptDetails)
            {
                item.Id = GuidGenerator.Create();
            }

            //ObjectMapper.Map(input, receiptHeader);

            //await _repository.UpdateAsync(receiptHeader, autoSave: true);

            return await base.UpdateAsync(id, input);
            //return ObjectMapper.Map<ReceiptHeader, ReceiptHeaderDto>(receiptHeader);
        }


        public override async Task DeleteAsync(Guid id)
        {
            DeleteDetail(id);
            //await CurrentUnitOfWork.CompleteAsync();
            //await _repository.UpdateAsync(receiptHeader, true);
            await base.DeleteAsync(id);
        }

        private async void DeleteDetail(Guid id)
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


        //private async Task SaveTags(ICollection<string> newTags, Post post)
        //{
        //    await RemoveOldTags(newTags, post);

        //    await AddNewTags(newTags, post);
        //}

        //private async Task RemoveOldTags(ICollection<string> newTags, Post post)
        //{
        //    foreach (var oldTag in post.Tags.ToList())
        //    {
        //        var tag = await _tagRepository.GetAsync(oldTag.TagId);

        //        var oldTagNameInNewTags = newTags.FirstOrDefault(t => t == tag.Name);

        //        if (oldTagNameInNewTags == null)
        //        {
        //            post.RemoveTag(oldTag.TagId);

        //            tag.DecreaseUsageCount();
        //            await _tagRepository.UpdateAsync(tag);
        //        }
        //        else
        //        {
        //            newTags.Remove(oldTagNameInNewTags);
        //        }
        //    }
        //}

        //private async Task AddNewTags(IEnumerable<string> newTags, Post post)
        //{
        //    var tags = await _tagRepository.GetListAsync(post.BlogId);

        //    foreach (var newTag in newTags)
        //    {
        //        var tag = tags.FirstOrDefault(t => t.Name == newTag);

        //        if (tag == null)
        //        {
        //            tag = await _tagRepository.InsertAsync(new Tag(GuidGenerator.Create(), post.BlogId, newTag, 1));
        //        }
        //        else
        //        {
        //            tag.IncreaseUsageCount();
        //            tag = await _tagRepository.UpdateAsync(tag);
        //        }

        //        post.AddTag(tag.Id);
        //    }
        //}
    }
}
