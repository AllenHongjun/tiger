using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Books
{
    
    /// <summary>
    /// 作者
    /// </summary>
    public class AuthorDto : EntityDto<Guid>
    {   
        /// <summary>
        /// 作者名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string ShortBio { get; set; }
    }
}
