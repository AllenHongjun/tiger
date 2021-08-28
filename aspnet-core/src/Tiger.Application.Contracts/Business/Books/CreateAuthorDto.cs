/**
 * 类    名：CreateAuthorDto   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/17 1:24:58       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TigerAdmin.Books;

namespace Tiger.Books
{   
    /// <summary>
    /// poco 
    /// </summary>
    public class CreateAuthorDto
    {
        [Required]
        [StringLength(AuthorConsts.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public string ShortBio { get; set; }
    }
}
