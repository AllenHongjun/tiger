using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.Train
{
    /// <summary>
    /// 资料下载
    /// </summary>
    public class DataDownLoad : Entity<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 课程Id
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int Sorting { get; set; }
        
        /// <summary>
        /// 启用禁用
        /// </summary>
        public bool Enable { get; set; }
        
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FileUrl { get; set; }

        /// <summary>
        /// 浏览量
        /// </summary>
        public int PageView { get; set; }


    }
}
