using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.Schools;
using Volo.Abp.Domain.Entities;

namespace Tiger.Module.Exams
{
    public class TestPaperJudgeSchool:Entity<Guid>
    {
        public Guid TestPaperId { get; set; }

        public Guid SchoolId { get; set; }

        public virtual TestPaper TestPaper { get; set; }

        public virtual School School { get; set; }

    }
}
