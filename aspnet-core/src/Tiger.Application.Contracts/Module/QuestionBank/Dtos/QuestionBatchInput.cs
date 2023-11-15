using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.QuestionBank.Dtos
{
    public class QuestionBatchInput
    {
        public List<Guid> QuestionIds { get; set; } = new List<Guid>();
    }
}
