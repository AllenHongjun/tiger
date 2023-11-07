﻿using AutoMapper;
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

            CreateMap<TestPaper, TestPaperDto>();
            CreateMap<CreateUpdateTestPaperDto, TestPaper>();

            CreateMap<TestPaperQuestion, TestPaperQuestionDto>();
            CreateMap<CreateUpdateTestPaperQuestionDto, TestPaperQuestion>();

            CreateMap<TestPaperStrategy, TestPaperStrategyDto>();
            CreateMap<CreateUpdateTestPaperStrategyDto, TestPaperStrategy>();
        }
    }
}
