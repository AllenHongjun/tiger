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
            CreateMap<CreateUpdateQuestionCategoryDto, QuestionCategory>();
        }
    }
}
