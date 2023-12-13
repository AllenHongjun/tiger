using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Module.Exams
{
    /// <summary>
    /// 试卷跳转链接配置表
    /// </summary>
    public class TestPaperLink:FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public TestPaperLinkType Type { get; set; }

        /// <summary>
        /// 跳转链接
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 关联的实训平台配置表Id
        /// </summary>
        public Guid? TrainPlatformId { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Eable { get; set; }
    }
}
