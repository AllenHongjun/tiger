﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.System.Platform.Routes
{
    public static class RouteConsts
    {
        public static int MaxNameLength { get; set; } = 64;
        public static int MaxDisplayNameLength { get; set; } = 256;
        public static int MaxFullNameLength { get; set; } = 128;
        public static int MaxDescriptionLength { get; set; } = 256;
        public static int MaxIconLength { get; set; } = 128;
        public static int MaxPathLength { get; set; } = 256;
        public static int MaxRedirectLength { get; set; } = 255;

        /// <summary>
        /// 层级深度
        /// </summary>
        public const int MaxDepth = 16;

        /// <summary>
        /// 编码长度
        /// </summary>
        public const int CodeUnitLength = 5;
        /// <summary>
        /// 编号最大长度
        /// </summary>
        public const int MaxCodeLength = MaxDepth * (CodeUnitLength + 1) - 1;


    }
}
