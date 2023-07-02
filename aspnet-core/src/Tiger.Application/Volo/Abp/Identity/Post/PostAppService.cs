using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Volo.Abp.Identity.Post.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Volo.Abp.Identity.Post;


public class PostAppService : CrudAppService<Post, PostDto, Guid, PostGetListInput, CreatePostDto, UpdatePostDto>,
    IPostAppService
{
    protected override string GetPolicyName { get; set; } = TigerPermissions.Post.Default;
    protected override string GetListPolicyName { get; set; } = TigerPermissions.Post.Default;
    protected override string CreatePolicyName { get; set; } = TigerPermissions.Post.Create;
    protected override string UpdatePolicyName { get; set; } = TigerPermissions.Post.Update;
    protected override string DeletePolicyName { get; set; } = TigerPermissions.Post.Delete;

    private readonly IPostRepository _repository;

    public PostAppService(IPostRepository repository) : base(repository)
    {
        _repository = repository;
    }

    //protected override async Task<IQueryable<Post>> CreateFilteredQueryAsync(PostGetListInput input)
    //{
    //    // TODO: AbpHelper generated
    //    return (await base.CreateFilteredQueryAsync(input))
    //        .WhereIf(!input.Remark.IsNullOrWhiteSpace(), x => x.Remark.Contains(input.Remark))
    //        .WhereIf(!input.Code.IsNullOrWhiteSpace(), x => x.Code.Contains(input.Code))
    //        .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
    //        .WhereIf(input.Sort != null, x => x.Sort == input.Sort)
    //        .WhereIf(input.IsActive != null, x => x.IsActive == input.IsActive)
    //        ;
    //}
}
