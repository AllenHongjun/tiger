/**
 * 类    名：OrderLine   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 12:17:22       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Tiger.Business.Demo
{   
    /// <summary>
    ///  每一个订单项目
    /// </summary>
    public class OrderLine : Entity
    {
        public virtual Guid OrderId { get; protected set; }

        public virtual Guid ProductId { get; protected set; }

        public virtual int Count { get; protected set; }

        /// <summary>
        /// 需要保留空的构造函数 没有参数的构造函数是 读取的时候 反序列化会用到。
        /// </summary>
        protected OrderLine()
        {

        }

        /// <summary>
        /// 创建一个实体类的构造函数
        /// OrderLine的构造函数是internal的,所以它只能由领域层来创建.在Order.AddProduct这个方法的内部被使用.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productId"></param>
        /// <param name="count"></param>
        internal OrderLine(Guid orderId, Guid productId, int count)
        {
            OrderId = orderId;
            ProductId = productId;
            Count = count;
        }

        internal void ChangeCount(int newCount)
        {
            Count = newCount;
        }

        public override object[] GetKeys()
        {
            return new Object[] { OrderId, ProductId };
        }
    }
}
