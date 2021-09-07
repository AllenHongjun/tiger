using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Business.Stocks.Dtos
{
    [Serializable]
    public class ShipmentDetailDto : FullAuditedEntityDto<Guid>
    {
        public string ProductName { get; set; }

        public string ProductSn { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public int ShipQty { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public int RequestQty { get; set; }

        /// <summary>
        /// ���κ�
        /// </summary>
        public string Batch { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        public DateTime AgingDate { get; set; }

        /// <summary>
        /// ���״̬
        /// </summary>
        public int InventorySts { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public decimal TotalWeight { get; set; }

        /// <summary>
        /// �����
        /// </summary>
        public decimal TotalVolume { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public string ProcessStamp { get; set; }

        /// <summary>
        /// ������λ
        /// </summary>
        public string QuantityUm { get; set; }

        /// <summary>
        /// ��ȡ������
        /// </summary>
        public int CanceledQty { get; set; }




        public Guid ProductId { get; set; }



        public Guid ShipmentId { get; set; }




        public Guid? TenantId { get; set; }


    }
}