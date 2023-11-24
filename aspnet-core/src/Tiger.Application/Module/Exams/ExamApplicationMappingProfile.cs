using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.Exams.Dtos;

namespace Tiger.Module.Exams
{
    public class ExamApplicationMappingProfile : Profile
    {
        public ExamApplicationMappingProfile()
        {
            CreateMap<Exam, ExamDto>();
            CreateMap<CreateUpdateExamDto, Exam>();

            CreateMap<AnswerSheet, AnswerSheetDto>();
            CreateMap<CreateUpdateAnswerSheetDto, AnswerSheet>();

            CreateMap<AnswerSheetDetail, AnswerSheetDetailDto>();
            CreateMap<CreateUpdateAnswerSheetDetailDto, AnswerSheetDetail>();

            CreateMap<TestPaper, TestPaperDto>();
            CreateMap<CreateUpdateTestPaperDto, TestPaper>();

            CreateMap<TestPaperSection, TestPaperSectionDto>();
            CreateMap<CreateUpdateTestPaperSectionDto, TestPaperSection>();

            CreateMap<TestPaperQuestion, TestPaperQuestionDto>();
            CreateMap<CreateUpdateTestPaperQuestionDto, TestPaperQuestion>();

            CreateMap<TestPaperStrategy, TestPaperStrategyDto>();
            CreateMap<CreateUpdateTestPaperStrategyDto, TestPaperStrategy>();
        }
    }
}
