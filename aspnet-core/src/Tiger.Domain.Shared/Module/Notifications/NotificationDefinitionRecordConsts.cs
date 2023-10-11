﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.Notifications
{
    public static class NotificationDefinitionRecordConsts
    {
        public static int MaxNameLength { get; set; } = 64;

        public static int MaxDisplayNameLength { get; set; } = 255;

        public static int MaxDescriptionLength { get; set; } = 255;

        public static int MaxProvidersLength { get; set; } = 200;
    }
}
