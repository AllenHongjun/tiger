using System;
using Tiger.Volo.Abp.Identity.Post.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Volo.Abp.Identity.Post;

public interface IPostAppService :
    ICrudAppService< 
        PostDto, 
        Guid, 
        PostGetListInput,
        CreatePostDto,
        UpdatePostDto>
{

}