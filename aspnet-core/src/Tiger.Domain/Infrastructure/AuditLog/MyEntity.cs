using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace Tiger.CoreModule.AuditLog
{
    /// <summary>
    /// 使用 Audited 来启用实体更改审计日志:
    /// </summary>
    [Audited]
    internal class MyEntity:Entity<Guid>
    {
    }


    /// <summary>
    /// 禁用实体
    /// </summary>
    [DisableAuditing]
    public class MyEntity2 : Entity<Guid>
    {
        //...
    }


    /// <summary>
    /// 仅禁用实体的某些属性的审计,以审计日志记录进行精细控制
    /// </summary>
    [Audited]
    public class MyUser : Entity<Guid>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        [DisableAuditing] //Ignore the Passoword on audit logging
        public string Password { get; set; }
    }


    /// <summary>
    /// 在某些情况下你可能要保存一些属性,但忽略所有其他属性. 
    /// </summary>

    [DisableAuditing]
    public class MyUser2 : Entity<Guid>
    {
        [Audited] //Only log the Name change
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }



}
