using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TigerAdmin.Books
{
    public class CreateUpdateBookDto
    {

        [Required]
        [StringLength(128)]
        public string Name { get; set; }


        public BookType Type { get; set; } = BookType.Undefined;

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; } = DateTime.Now;

        [Required]
        public float Price { get; set; }

    }
}
