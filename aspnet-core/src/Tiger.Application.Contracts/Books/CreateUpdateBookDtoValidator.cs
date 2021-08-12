/**
 * 类    名：CreateUpdateBookDtoValidator   
 * 作    者：hongjy       
 * 创建时间：2021/8/11 21:17:31       
 * 说    明: 
 * 
 */

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Books
{
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
