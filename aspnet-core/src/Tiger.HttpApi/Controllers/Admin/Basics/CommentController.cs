using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Basic.Comments;
using Tiger.Basic.Products;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tiger.Controllers.Admin.Basics
{

    /// <summary>
    /// 评论
    /// </summary>
    [RemoteService(Name = "Comment")]
    [Area("Basics")]
    [ControllerName("Comment")]
    [Route("api/basic/comment")]
    [ApiExplorerSettings(GroupName = "admin-basic")]
    public class CommentController : TigerController, ICommentAppService
    {
        private readonly ICommentAppService _commentAppService;

        public CommentController(ICommentAppService productAppService)
        {
            _commentAppService = productAppService;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<CommentDto> CreateAsync(CreateUpdateCommentDto input)
        {
            return _commentAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _commentAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<CommentDto> GetAsync(Guid id)
        {
            return _commentAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<PagedResultDto<CommentDto>> GetListAsync(GetCommentListDto input)
        {
            return _commentAppService.GetListAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<CommentDto> UpdateAsync(Guid id, CreateUpdateCommentDto input)
        {
            return _commentAppService.UpdateAsync(id, input);
        }
    }
}
