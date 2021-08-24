using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tiger.Basic.ProductAttributeTpyes
{
    public class CreateUpdateProductAttributeTypeDto
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        [Required(ErrorMessage = "请输入类型名称")]
        [StringLength(300)]
        public string Name { get; set; }
    }
}
