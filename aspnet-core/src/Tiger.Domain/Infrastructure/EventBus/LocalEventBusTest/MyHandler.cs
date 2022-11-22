using log4net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.Emailing;
using Volo.Abp.EventBus;
using Volo.Abp.Identity;

namespace Tiger.Infrastructure.EventBus.LocalEventBusTest
{
    /// <summary>
    /// 事件处理程序
    /// </summary>
    public class MyHandler
        : ILocalEventHandler<StockCountChangedEvent>,
          ITransientDependency  // 事件处理程序类必须注册到依赖注入(DI),示例中使用了 ITransientDependency.
    {
        //一个服务可以实现 ILocalEventHandler<TEvent> 来处理事件.

        private readonly ILog _log;
        private readonly IEmailSender _emailSender;

        public MyHandler(ILog log, IEmailSender emailSender)
        {
            _log = log;
            this._emailSender=emailSender;
        }

        public async Task HandleEventAsync(StockCountChangedEvent eventData)
        {
            // 这就是全部,MyHandler 由ABP框架自动发现,并在发生 StockCountChangedEvent 事件时调用 HandleEventAsync.

            // 事件可以由0个或多个处理程序订阅.
            // 一个事件处理程序可以订阅多个事件,但是需要为每个事件实现 ILocalEventHandler<TEvent> 接口.


            // 当一个请求操作之后 可以直接返回成功。。其他的操作可以返回事件
            // 通过事件总线的方式 可以减少用户的等待时间 比如 我要修改库存 后续的 发送邮件 添加任务 计算其他库存等 都可以放到事件里面来后续操作
            //TODO: your code that does somthing on the event

            /*
             如果您执行数据库操作并在事件处理程序中使用仓储,那么您可能需要创建一个工作单元,因为一些存储库方法需要在活动的工作单元中工作. 确保处理方法设置为 virtual,并为该方法添加一个 [UnitOfWork] attribute. 或者手动使用 IUnitOfWorkManager 创建一个工作单元范围.
             
             */
            _log.Info("");
            await _emailSender.SendAsync(
                    "hongjy1991@gmail.com",     // target email address
                    "Email subject",         // subject
                    "This is email body..."  // email body
                );


            try
            {
                // 如果处理程序抛出一个异常,它会影响发布该事件的代码. 这意味着它在 PublishAsync 调用上获得异常. 因此如果你想隐藏错误,在事件处理程序中使用try-catch. 
            }
            catch (Exception ex)
            {

                throw ex;
            }

            throw new NotImplementedException();
        }
    }



    /// <summary>
    /// 预定义的事件
    /// </summary>
    public class MyHandler2
        : ILocalEventHandler<EntityCreatedEventData<IdentityUser>>,
          ITransientDependency
    {

        private readonly IEmailSender _emailSender;

        public MyHandler2(IEmailSender emailSender)
        {
            _emailSender=emailSender;
        }

        public async Task HandleEventAsync(
            EntityCreatedEventData<IdentityUser> eventData)
        {

            // 发布实体创建,更新,删除事件是常见的操作. ABP框架为所有的实体自动发布这些事件. 你只需要订阅相关的事件.
            var userName = eventData.Entity.UserName;
            var email = eventData.Entity.Email;

            Console.WriteLine($"{userName}-{email}");
            await _emailSender.SendAsync(
                    "hongjy1991@gmail.com",     // target email address
                    "Email subject",         // subject
                    "This is email body..."  // email body
                );
            //...

            // 这个类订阅 EntityCreatedEventData<IdentityUser>,它在用户创建后发布. 你可能需要向新用户发送一封"欢迎"电子邮件.


            /*
             
             用过去时态事件
            当相关工作单元完成且实体更改成功保存到数据库时,将发布带有过去时态的事件. 如果在这些事件处理程序上抛出异常,则无法回滚事务,因为事务已经提交.

            事件类型;

            EntityCreatedEventData<T> 当实体创建成功后发布.
            EntityUpdatedEventData<T> 当实体更新成功后发布.
            EntityDeletedEventData<T> 当实体删除成功后发布.
            EntityChangedEventData<T> 当实体创建,更新,删除后发布. 如果你需要监听任何类型的更改,它是一种快捷方式 - 而不是订阅单个事件.



            用于进行时态事件
            带有进行时态的事件在完成事务之前发布(如果数据库事务由所使用的数据库提供程序支持). 如果在这些事件处理程序上抛出异常,它会回滚事务,因为事务还没有完成,更改也没有保存到数据库中.

            事件类型;

            EntityCreatingEventData<T> 当新实体保存到数据库前发布.
            EntityUpdatingEventData<T> 当已存在实体更新到数据库前发布.
            EntityDeletingEventData<T> 删除实体前发布.
            EntityChangingEventData<T> 当实体创建,更新,删除前发布. 如果你需要监听任何类型的更改,它是一种快捷方式 - 而不是订阅单个事件.
             
             */
        }
    }
}
