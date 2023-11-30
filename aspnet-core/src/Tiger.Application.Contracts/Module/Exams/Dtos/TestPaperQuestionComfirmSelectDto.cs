using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.Exams.Dtos
{
    /// <summary>
    /// 试卷题目手动确认选择
    /// </summary>
    public class TestPaperQuestionComfirmSelectDto
    {
        public Guid TestPaperId { get; set; }

        public Guid TestPaperSectionId { get; set; }

        public List<CreateUpdateTestPaperQuestionDto> Questions { get; set; }
    }
}
