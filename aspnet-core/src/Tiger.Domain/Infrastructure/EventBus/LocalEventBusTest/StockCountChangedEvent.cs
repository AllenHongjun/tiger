using System;

namespace Tiger.Infrastructure.EventBus.LocalEventBusTest
{
    /// <summary>
    /// PublishAsync 方法需要一个参数:事件对象,它负责保持与事件相关的数据,是一个简单的普通类:
    /// </summary>
    public class StockCountChangedEvent
    {
        public Guid ProductId { get; set; }
        public int NewCount { get; set; }
    }
}