using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tiger.Books
{
    /// <summary>
    /// 添加创建删除书籍Dto
    /// </summary>
    /// <remarks>
    ///  
    /// 
    /// </remarks>
    public class CreateUpdateBookDto : IValidatableObject
    {
        /// <summary>
        /// 书籍名称
        /// </summary>
        /// <remarks>
        /// Asp.net core 的模型验证 https://learn.microsoft.com/zh-cn/aspnet/core/mvc/models/validation?view=aspnetcore-7.0
        /// ABP的优点
        /// 定义 `IValidationEnabled` 向任意类添加自动验证. 所有的[应用服务](Application-Services.md)都实现了该接口,所以它们会被自动验证.
        /// 自动将数据注解属性的验证错误信息本地化.
        /// 提供可扩展的服务来验证方法调用或对象的状态.
        /// 提供[FluentValidation](https://fluentvalidation.net/)的集成.
        /// 
        /// 数据注解 当使用该类作为[应用服务](Application-Services.md)或控制器的参数时,将对其自动验证并抛出本地化异常(由ABP框架[处理](Exception-Handling.md)).
        /// </remarks>
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

        /// <summary>
        /// 可以将DTO实现 `IValidatableObject` 接口进行自定义验证逻辑. 下面的示例中 `CreateBookDto` 实现了这个接口,并检查 `Name` 是否等于 `Description` 并返回一个验证错误.
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name.Length > 50000)
            {
                yield return new ValidationResult("书籍的名称过长", new[] { "Name" });
            }
        }
    }
}
