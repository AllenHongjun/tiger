using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Basic.Comments;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Basic
{   
    /// <summary>
    /// 商品评论
    /// </summary>
    [RemoteService(false)]
    public class CommentAppService :
        CrudAppService<
            Comment, //The  entity
            CommentDto,
            Guid, //Primary key 
            GetCommentListDto,
            CreateUpdateCommentDto,
            CreateUpdateCommentDto>, //Used to create/update
        ICommentAppService
    {
        public CommentAppService(IRepository<Comment, Guid> repository) : base(repository)
        {
        }
    }
}
