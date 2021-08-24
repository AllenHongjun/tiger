/**
 * 类    名：AuthorAlreadyExistsException   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/17 1:01:51       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using TigerAdmin.Books;
using Volo.Abp;

namespace Tiger.Business.Demo
{
    public class AuthorAlreadyExistsException : BusinessException
    {
        public AuthorAlreadyExistsException(string name)
            : base(BookStoreDomainErrorCodes.AuthorAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
