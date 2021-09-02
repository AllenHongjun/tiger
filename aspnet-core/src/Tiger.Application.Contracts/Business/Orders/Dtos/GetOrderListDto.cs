using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Orders.Orders
{
    public class GetOrderListDto: PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// 订单编码
        /// </summary>
        public string OrderSn { get; set; }

        /// <summary>
        /// 订单状态：0->待付款；1->待发货；2->已发货；3->已完成；4->已关闭；5->无效订单
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 支付方式：0->未支付；1->支付宝；2->微信
        /// </summary>
        public int? PayType { get; set; }

        /// <summary>
        /// 订单来源：0->PC订单；1->app订单
        /// </summary>
        public int? SourceType { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string ReceiverName { get; set; }
    }
}
