/**
 * 类    名：User   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 15:09:07       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Tiger.Business.Demo
{
    public class User : AggregateRoot<Guid>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }
    }
}
