using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.Exams.Dtos
{
    public class ExamineeBatchInputDto
    {
        public Guid ExamId { get; set; }

        public List<Guid> UserIds { get; set; } = new List<Guid>();
    }
}
