using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace Tiger.Infrastructure.RealTime
{
    [Serializable]
    [GenericEventName(Prefix = "abp.realtime.")]
    public class RealTimeEto<T> : EtoBase
    {
        public T Data { get; set; }
        public RealTimeEto() : base()
        {
        }

        public RealTimeEto(T data) : base()
        {
            Data = data;
        }
    }
}
