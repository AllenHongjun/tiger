using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Basic.Comments
{
    public class CommentDto:EntityDto<Guid>
    {
        public Guid ProductId { get; set; }

        public Guid OrderId { get; set; }

        public string OrderSn { get; set; }

        /// <summary>
        /// 上传图片以逗号隔开
        /// </summary>
        public string Pics { get; set; }

        public int Star { get; set; }

        public int ShowStatus { get; set; }


        public string MemberNickName { get; set; }

        public string MemberIP { get; set; }

        public string MemberIcon { get; set; }

        public string Content { get; set; }

        /// <summary>
        /// 购买时产品属性
        /// </summary>

        public string ProductAttribute { get; set; }

        public int CollectCount { get; set; }

        public int ReadCount { get; set; }

        public int ReplayCount { get; set; }
    }
}
