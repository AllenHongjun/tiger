using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Volo.Abp.Sass
{   
    /// <summary>
    /// Sass 模块错误编码
    /// </summary>
    public class AbpSaasErrorCodes
    {
        public const string Namespace = "Sass";

        public const string DuplicateEditionDisplayName = Namespace + ":010001";

        public const string DeleteUsedEdition = Namespace + ":010002";

        public const string DuplicateTenantName = Namespace + ":020001";

    }
}
