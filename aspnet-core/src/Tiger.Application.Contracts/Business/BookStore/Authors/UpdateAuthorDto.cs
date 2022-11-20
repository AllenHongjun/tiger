using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TigerAdmin.Books;

namespace Tiger.Books
{
    public class UpdateAuthorDto
    {
        /// <summary>
        /// 作者名字
        /// </summary>
        [Required]
        [StringLength(AuthorConsts.MaxNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [Required]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string ShortBio { get; set; }
    }
}
