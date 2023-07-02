using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Data;

namespace Tiger.Volo.Abp.Sass
{
    public class AbpSaasDbProperties
    {
        public static string DbTablePrefix { get; set; } = AbpCommonDbProperties.DbTablePrefix;

        public static string DbSchema { get; set; } = AbpCommonDbProperties.DbSchema;

        public static string ConnectionStringName = "AbpSass";


    }
}
