using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Users;

namespace Tiger.CoreModule.AuditLog
{
    /// <summary>
    /// 
    /// 扩展审计系统的方法
    /// </summary>
    public class MyAuditLogContributor : AuditLogContributor
    {
        public override void PreContribute(AuditLogContributionContext context)
        {
            // 可以从依赖注入系统中解析服务.
            var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();
            context.AuditInfo.SetProperty(
                "MyCustomClaimValue",
                currentUser.FindClaimValue("MyCustomClaim")
            );

            



        }

        public override void PostContribute(AuditLogContributionContext context)
        {
            //可以用来访问当前审计日志的对象并进行操作.
            context.AuditInfo.Comments.Add("Some comment...");
        }


        /// <summary>
        /// 审计对象相关类学习
        /// 文档内容:https://docs.abp.io/zh-Hans/abp/latest/Audit-Logging
        /// </summary>
        public void TestAuditLog()
        {
            // 默认为每个web请求创建一个审计日志对象
            AuditLogInfo auditLogInfo = new AuditLogInfo();

            // 一个 审计日志动作通常是web请求期间控制器动作或应用服务方法调用. 一个审计日志可以包含多个动作
            AuditLogActionInfo auditLogActionInfo = new AuditLogActionInfo();

            // 表示一个实体在Web请求中的变更. 审计日志可以包含0个或多个实体的变更. 实体变更具有以下属性
            EntityChangeInfo entityChangeInfo = new EntityChangeInfo();

            // 表示一个实体的属性的更改.一个实体的更改信息(上面已说明)可含有具有以下属性的一个或多个属性的更改
            EntityPropertyChangeInfo entityPropertyChangeInfo = new EntityPropertyChangeInfo();
             
        }
    }

    public interface IAuditLogTest: IAuditingStore
    {

    }

}
