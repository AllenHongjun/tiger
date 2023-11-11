using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.QuestionBank.Dtos;

namespace Tiger.Module.QuestionBank
{
    public class QuestionBankApplicationMappingProfile : Profile
    {
        public QuestionBankApplicationMappingProfile()
        {
            CreateMap<QuestionCategory, QuestionCategoryDto>();
            CreateMap<CreateUpdateQuestionCategoryDto, QuestionCategory>()
                ;

            CreateMap<Question,QuestionDto>()
                .ForMember(dst => dst.QuestionCateogryName, opt => opt.MapFrom(src => src.QuestionCategory.Name));
            CreateMap<CreateUpdateQuestionDto, Question>();
        }
    }
}
