using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Module.System.Message.Sms
{
    /// <summary>
    /// 短信发送日志
    /// </summary>
    public class SmsLog : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 短信渠道Id
        /// </summary>
        public int ChannelId { get; set; }
        /// <summary>
        /// 短信渠道编码
        /// </summary>
        public string ChannelCode { get; set; }
        /// <summary>
        /// 短信模板Id
        /// </summary>
        public int TemplateId { get; set; }
        public string TemplateCode { get; set; }
        public int TemplateType { get; set; }
        public string TemplateContent { get; set; }
        public string ApiTemplateId { get; set; }


        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        public Guid userId { get; set; }
        public int UserType { get; set; }
        /// <summary>
        /// 发送状态
        /// </summary>
        public int SendStatus { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }
        /// <summary>
        /// 接收状态
        /// </summary>
        public int ReceiveStatus { get; set; }
        /// <summary>
        /// 接受状态
        /// </summary>
        public DateTime? ReceiveTime { get; set; }
        /// <summary>
        /// 短信验证码
        /// </summary>
        public int SendCode { get; set; }
        /// <summary>
        /// 短信内容
        /// </summary>
        public string SendMsg { get; set; }


        /// <summary>
        /// API 短信编号
        /// </summary>
        public string ApiSendCode { get; set; }
        public string ApiSendMsg { get; set; }
        /// <summary>
        /// API 请求编号
        /// </summary>
        public string ApiRequestId { get; set; }
        public string ApiSerialNo { get; set; }

        public string ApiReceiveCode { get; set; }
        /// <summary>
        /// API 接收结果
        /// </summary>
        public object ApiReceiveMsg { get; set; }
    }




}
