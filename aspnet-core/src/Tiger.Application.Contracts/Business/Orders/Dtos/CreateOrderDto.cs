using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Business.Orders.Dtos
{
    public class CreateOrderDto
    {
        public Guid memberId { get; set; }
        public int sourceType { get; set; }
        public int orderType { get; set; }
        public Guid memberReceiveId { get; set; }
        public int useIntegration { get; set; }
    }
}
