using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiger.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tiger.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class TigerController : AbpController
    {   
        public Guid MemberId = Guid.Parse("ebd06de4-f535-bebb-87d1-39feb3daea6c");

        public Guid SkuId = Guid.NewGuid();

        protected TigerController()
        {
            LocalizationResource = typeof(TigerResource);
        }


        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("batchDelete")]
        public virtual Task BatchDeleteAsync(Guid[] ids)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("batchAudit")]
        public virtual Task BatchAuditAsync(Guid[] ids)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 批量关闭
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("batchClose")]
        public virtual Task BatchCloseAsync(Guid[] ids)
        {
            return Task.CompletedTask;
        }
    }
}