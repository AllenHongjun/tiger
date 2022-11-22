using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Local;

namespace Tiger.Infrastructure.EventBus.LocalEventBusTest
{
    /// <summary>
    /// 时间总线使用示例
    /// </summary>
    public class MyService : ITransientDependency
    {
        private readonly ILocalEventBus _localEventBus;

        public MyService(ILocalEventBus localEventBus)
        {
            _localEventBus = localEventBus;
        }

        public virtual async Task ChangeStockCountAsync(Guid productId, int newCount)
        {
            //TODO: IMPLEMENT YOUR LOGIC...

            // 产品的库存数量发生变化时发布本地事件
            //PUBLISH THE EVENT
            await _localEventBus.PublishAsync(
                new StockCountChangedEvent
                {
                    ProductId = productId,
                    NewCount = newCount
                }
            );
        }
    }
}
