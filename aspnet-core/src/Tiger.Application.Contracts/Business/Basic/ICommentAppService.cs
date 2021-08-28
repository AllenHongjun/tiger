using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace Tiger.Basic.Comments
{
    public interface ICommentAppService:
        ICrudAppService< //Defines CRUD methods
            CommentDto, //Used to show 
            Guid, //Primary key 
            GetCommentListDto,
            CreateUpdateCommentDto, //Used for paging/sorting
            CreateUpdateCommentDto> //Used to create/update 
    {

    }
}
