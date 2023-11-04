using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.Exams.Dtos;

namespace Tiger.Module.Exams
{
    public class TestPaperApplicationMappingProfile : Profile
    {
        public TestPaperApplicationMappingProfile()
        {
            CreateMap<TestPaper, TestPaperDto>();
            CreateMap<CreateUpdateTestPaperDto, TestPaper>();
        }
    }
}
