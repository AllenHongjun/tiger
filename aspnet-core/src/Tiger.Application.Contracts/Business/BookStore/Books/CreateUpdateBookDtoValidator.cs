
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Books
{
    /// <summary>
    /// Book数据验证
    /// </summary>
    /// <remarks>
    /// 
    /// FluentValidation文档 https://docs.fluentvalidation.net/en/latest/
    /// ABP会自动找到这个类并在对象验证时与 `CreateUpdateBookDto` 关联.
    /// </remarks>
    public class CreateUpdateBookDtoValidator : AbstractValidator<CreateUpdateBookDto>
    {
        public CreateUpdateBookDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().Length(3, 10).WithMessage("名称必须在3到10个字符之间");
            RuleFor(x => x.Price).ExclusiveBetween(0.0f, 999.0f).WithMessage("价格必须在0-999元之间");
            //RuleFor(x => x.Price).NotNull();
            RuleSet("Names", () => {
                RuleFor(x => x.Price).NotNull().WithMessage("价格不能为空");
                RuleFor(x => x.Price).ExclusiveBetween(0.0f, 999.0f).WithMessage("价格必须在0-999元之间");
            });
        }
    }
}
