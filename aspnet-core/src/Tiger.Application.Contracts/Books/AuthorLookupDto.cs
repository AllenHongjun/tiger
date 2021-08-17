/**
 * 类    名：AuthorLookupDto   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/17 10:15:58       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Books
{
    public class AuthorLookupDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
