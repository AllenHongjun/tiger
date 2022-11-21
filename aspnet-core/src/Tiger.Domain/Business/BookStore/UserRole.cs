/**
 * 类    名：UserRole   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 12:07:18       
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
    /// 有些实体可能需要 复合键 .在这种情况下,可以从非泛型Entity类派生实体.如:
    /// 
    /// 上面的例子中,复合键由UserId和RoleId组成.在关系数据库中,它是相关表的复合主键. 具有复合键的实体应当实现上面代码中所示的GetKeys()方法.
    /// </summary>
    public class UserRole: Volo.Abp.Domain.Entities.Entity
    {
        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }

        public DateTime CreationTime { get; set; }

        public UserRole()
        {

        }

        public override object[] GetKeys()
        {
            return new object[] { UserId, RoleId };
            //throw new NotImplementedException();
        }
    }
}
