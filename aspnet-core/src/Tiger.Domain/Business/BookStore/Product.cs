/**
 * 类    名：Product   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 16:33:30       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Tiger.Business.Demo
{   
    /// <summary>
    ///   
    /// </summary>
    public class Product : AggregateRoot<Guid>
    {
        /*
         GUID是自然唯一的在以下情况下有一些优势;
        你需要与外部系统集成,
        你需要拆分或合并不同的表.
        你正在创建分布式系统
        GUID是无法猜测的,因此在某些情况下与自动递增的Id值相比,GUID更安全.
         
         */
        public string Name { get; set; }

        private Product() { /* This constructor is used by the ORM/database provider */ }

        public Product(Guid id, string name)
            : base(id)
        {
            Name = name;
        }
    }
}
