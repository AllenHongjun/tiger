using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Tiger.Business.Members
{   
    /// <summary>
    /// 会员规则设置表
    /// 
    /// TODO: 根据需求再设计
    /// </summary>
    public class MemberRuleSetting:Entity<Guid>
    {
        /// <summary>
        /// 连续签到天数
        /// </summary>
        public int ContinueSignDay { get; set; }

        /// <summary>
        /// 连续签到赠送数量
        /// </summary>
        public int ContinueSignPoint { get; set; }

        /// <summary>
        /// 每消费多少元获取一个点
        /// </summary>
        public decimal ConsumePerPoint { get; set; }

        
    }
}
