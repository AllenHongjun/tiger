/**
 * 类    名：Person   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 12:48:51       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Tiger.Business.Demo
{
    public class Person:Entity<Guid>
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
