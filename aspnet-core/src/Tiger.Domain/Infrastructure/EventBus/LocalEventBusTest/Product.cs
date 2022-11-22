using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Tiger.Infrastructure.EventBus.LocalEventBusTest
{
    public class Product : AggregateRoot<Guid>
    {
        //实体不能通过依赖注入注入服务,但是在实体/聚合根类中发布本地事件是非常常见的.
        public string Name { get; set; }

        public int StockCount { get; private set; }

        private Product() { }

        public Product(Guid id, string name)
            : base(id)
        {
            Name = name;
        }

        public void ChangeStockCount(int newCount)
        {
            StockCount = newCount;

            //ADD an EVENT TO BE PUBLISHED 在实体上发布时间

            // AggregateRoot 类定义了 AddLocalEvent 来添加一个新的本地事件,事件在聚合根对象保存(创建,更新或删除)到数据库时发布.

            // 调用 AddLocalEvent 不会立即发布事件. 当你将更改保存到数据库时发布该事件;
            // 对于 EF Core, 它在 DbContext.SaveChanges 中发布.
            // 对于 MongoDB, 它在你调用仓储的 InsertAsync, UpdateAsync 或 DeleteAsync 方法时发由(因为MongoDB没有更改跟踪系统).
            AddLocalEvent(
                new StockCountChangedEvent
                {
                    ProductId = Id,
                    NewCount = newCount
                }
            );
        }
    }
}
