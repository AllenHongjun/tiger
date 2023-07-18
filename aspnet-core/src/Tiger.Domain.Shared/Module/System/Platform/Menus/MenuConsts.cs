using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.System.Platform.Menus
{
    public static class MenuConsts
    {
        public static int MaxComponentLength { get; set; } = 256;

        /// <summary>
        /// 最大深度
        /// </summary>
        /// <remarks>
        /// 默认为4 仅支持4级菜单
        /// </remarks>
        public const int MaxDepth = 4;

        public const int MaxCodeLength = MaxDepth * (PlatformConsts.CodeUnitLength + 1) - 1;
    }
}
