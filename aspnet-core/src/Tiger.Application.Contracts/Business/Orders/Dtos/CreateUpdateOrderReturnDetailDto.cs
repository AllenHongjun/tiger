using System;

namespace Tiger.Business.Orders.Dtos
{
    [Serializable]
    public class CreateUpdateOrderReturnDetailDto
    {
        public string OrderSn { get; set; }

        public decimal ReturnAmount { get; set; }

        public string ReturnName { get; set; }

        public string ReturnPhone { get; set; }

        public int Status { get; set; }

        public DateTime HandleTime { get; set; }

        public string ProductPic { get; set; }

        public string ProductName { get; set; }

        public string ProductAttr { get; set; }

        public string ProductQuantity { get; set; }

        public string ProductPrice { get; set; }

        public string ProductRealPrice { get; set; }

        public string Reason { get; set; }

        public string Description { get; set; }

        public string ProofPics { get; set; }

        public string HandleNote { get; set; }

        public string HandleMan { get; set; }

        public string ReceiveMan { get; set; }

        public string ReceiveTime { get; set; }

        public string ReceiveNote { get; set; }

        public Guid ReceiveAddressId { get; set; }


        public Guid ProductId { get; set; }


        public Guid MemberId { get; set; }

    }
}