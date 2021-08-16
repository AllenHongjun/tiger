/**
 * 类    名：EmailSendingArgs   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/12 8:00:56       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.BackgroundJob
{
    public class EmailSendingArgs
    {
        public string EmailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
