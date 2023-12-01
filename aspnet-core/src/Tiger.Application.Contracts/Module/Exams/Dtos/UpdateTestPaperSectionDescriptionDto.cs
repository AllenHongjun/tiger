using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Validation;

namespace Tiger.Module.Exams.Dtos
{
    public class UpdateTestPaperSectionDescriptionDto
    {

        // <summary>
        /// 大题描述(可用于保存阅读理解题、论述题、组合题等各种复杂题型的题干内容)
        /// </summary>
        [DynamicStringLength(typeof(TestPaperSectionConsts), nameof(TestPaperSectionConsts.MaxDesctiptionLength))]
        public string Description { get; set; }
    }
}
