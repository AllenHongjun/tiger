/**
 * 类    名：Category   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:13:19       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Tiger.Basic
{
    public class Category:AggregateRoot<Guid>
    {
        public Guid ParentId { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; }

        public int ProductCount { get; set; }

        /// <summary>
        /// 显示状态 0->不显示 1->显示
        /// </summary>
        public int ShowStatus { get; set; }

        public int Sort { get; set; }

        public string Icon { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }

        public string Description { get; set; }




    }
}
