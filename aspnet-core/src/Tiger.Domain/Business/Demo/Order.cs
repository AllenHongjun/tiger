/**
 * 类    名：Order   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 12:14:52       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tiger.Business.Demo;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Tiger.Entities.Demo
{
    /// <summary>
    /// DDD规则或模式.
    /// 
    /// 聚合根需要维护自身的完整性,所有的实体也是这样.但是聚合根也要维护子实体的完整性.所以,聚合根必须一直有效.
    ///使用Id引用聚合根,而不使用导航属性
    ///聚合根被视为一个单元.它是作为一个单元检索和更新的.它通常被认为是一个交易边界.
    ///不单独修改聚合根中的子实体
    ///
    ///所有属性都有protected的set.这是为了防止实体在实体外部任意改变.因此,在没有向订单中添加新产品的情况下设置 TotalItemCount将是危险的.它的值由AddProduct方法维护.
    /// </summary>
    public class Order : AggregateRoot<Guid>
    {
        public virtual string ReferenceNo { get; protected set; }

        public virtual int TotalItemCount { get; protected set; }

        public virtual DateTime CreationTime { get; protected set; }

        public virtual List<OrderLine> OrderLines { get; protected set; }


        /// <summary>
        /// protected/private的构造函数只有从数据库读取对象时 反序列化 才需要.
        /// </summary>
        protected Order()
        {

        }

        /// <summary>
        /// Order有一个公共的构造函数,它需要 minimal requirements 来构造一个"订单"实例.因此,在没有Id和referenceNo的时候是无法创建订单的
        /// 定义一个公开的构造函数。。如果需要创建实体类就必须通过这个public 的构造函数。  这个是在添加数据 修改数据的时候用到。
        /// </summary>
        public Order(Guid id, string referenceNo)
        {
            Check.NotNull(referenceNo, nameof(referenceNo));

            Id = id;
            ReferenceNo = referenceNo;

            OrderLines = new List<OrderLine>();
        }

        /// <summary>
        /// Order.AddProduct实现了业务规则将商品添加到订单中
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="count"></param>
        public void AddProduct(Guid productId, int count)
        {
            if (count <= 0)
            {
                throw new ArgumentException(
                    "You can not add zero or negative count of products!",
                    nameof(count)
                );
            }

            var existingLine = OrderLines.FirstOrDefault(ol => ol.ProductId == productId);

            if (existingLine == null)
            {
                OrderLines.Add(new OrderLine(this.Id, productId, count));
            }
            else
            {
                existingLine.ChangeCount(existingLine.Count + count);
            }

            TotalItemCount += count;
        }
    }
}
