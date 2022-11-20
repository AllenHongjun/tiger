using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tiger.Books
{
    public class CreateUpdateBookDto
    {
        /// <summary>
        /// 书籍名称
        /// </summary>
        [Required]
        [StringLength(8, ErrorMessage = "长度不能超过8个字符")]
        public string Name { get; set; }


        /// <summary>
        /// 书籍类型
        /// </summary>
        public BookType Type { get; set; } = BookType.Undefined;

        /// <summary>
        /// 发布日期
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 价格
        /// </summary>
        [Required]
        public float Price { get; set; }

        /// <summary>
        /// 作者id
        /// </summary>
        public Guid AuthorId { get; set; }

    }
}
