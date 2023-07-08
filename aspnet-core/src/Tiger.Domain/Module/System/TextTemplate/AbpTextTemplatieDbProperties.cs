using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Data;

namespace Tiger.Module.System.TextTemplate
{
    public class AbpTextTemplatieDbProperties
    {

        /// <summary>
        /// 表前缀 设置默认值
        /// </summary>
        public static string DbTablePrefix { get; set; } = AbpCommonDbProperties.DbTablePrefix;

        public static string DbSchema { get; set; } = AbpCommonDbProperties.DbSchema;

        public const string ConnectionStringName = "AbpTextTemplating";
    }
}
