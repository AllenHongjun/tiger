using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Tiger.Module.Exams
{
    public class TestPaperJudgeSchool:Entity<Guid>
    {
        public Guid TestPaperId { get; set; }
        public Guid SchoolId { get; set; }

    }
}
