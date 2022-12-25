namespace Tiger.Settings
{
    public static class TigerSettings
    {
        private const string Prefix = "Tiger";

        //Add your own setting names here. Example:
        //public const string MySetting1 = Prefix + ".MySetting1";


        /// <summary>
        /// 七牛
        /// </summary>
        public static class Qiniu
        {
            private const string QiniuPrefix = Prefix + ".Qiniu";

            /// <summary>
            /// oss对象存储
            /// </summary>
            public static class Oss
            {
                private const string OssPrefix = QiniuPrefix + ".Oss";

                public const string AccessKey = OssPrefix + ".AccessKey";

                public const string SecretKey = OssPrefix + ".SecretKey";

                public const string Bucket = OssPrefix + ".Bucket";
            }
        }

        public static class Aliyun
        {
            private const string AliyunPrefix = Prefix + ".Aliyun";
            
            /// <summary>
            /// 对象存储服务
            /// </summary>
            public static class Oss
            {
                private const string OssPrefix = AliyunPrefix + ".Oss";

                public const string AccessKeyId = OssPrefix + ".AccessKeyId";

                public const string AccessKeySecret = OssPrefix + ".AccessKeySecret";

                public const string Endpoint = OssPrefix + ".Endpoint";
            }

            /// <summary>
            /// 短信服务
            /// </summary>
            public static class Sms
            {
                private const string SmsPrefix = AliyunPrefix + ".Sms";

                public const string AccessKeyId = SmsPrefix + ".AccessKeyId";

                public const string AccessKeySecret = SmsPrefix + ".AccessKeySecret";

                public const string Endpoint = SmsPrefix + ".Endpoint";
            }

        }


    }
}