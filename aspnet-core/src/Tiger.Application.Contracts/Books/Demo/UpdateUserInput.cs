/**
 * 类    名：UpdateUserInput   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 15:14:24       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace TigerAdmin.Books.Demo
{   
    /// <summary>
    /// 修改用户信息
    /// </summary>
    public class UpdateUserInput
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }

    }
}
