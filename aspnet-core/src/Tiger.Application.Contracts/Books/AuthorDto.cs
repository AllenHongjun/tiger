/**
 * 类    名：AuthorDto   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/17 1:21:34       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Books
{
    /// <summary>
    /// EntityDto<T> simply has an Id property with the given generic argument. You could create an Id property yourself instead of inheriting the EntityDto<T>.
    /// </summary>
    public class AuthorDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string ShortBio { get; set; }
    }
}
