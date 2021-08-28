using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Permissions
{
    public static class MemberPermissions
    {
        public const string GroupName = "Member";
        public class Member
        {
            public const string Default = GroupName + ".Member";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class MemberLevel
        {
            public const string Default = GroupName + ".MemberLevel";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class MemberLoginLog
        {
            public const string Default = GroupName + ".MemberLoginLog";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class MemberReceiveAddress
        {
            public const string Default = GroupName + ".MemberReceiveAddress";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class MemberStatisticInfo
        {
            public const string Default = GroupName + ".MemberStatisticInfo";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }
    }
}
