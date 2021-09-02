using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
namespace Tiger.Domain.CoreModule.Utilities
{
    public class Utility
    {
        /// <summary>
        /// 删除数组中指定的元素
        /// </summary>
        /// <param name="array">源数据</param>
        /// <param name="items">要删除的原元</param>
        /// <exception cref="ArgumentNullException">array,item不能为空</exception>
        /// <returns></returns>
        /// 
        public static string[] Splice(string[] array, params string[] items)
        {
            if (array == null || items == null)
            {
                throw new ArgumentNullException();
            }
            List<string> result = new List<string>();
            foreach (var str in array)
            {
                if (string.IsNullOrEmpty(str))
                {
                    continue;
                }
                bool flag = false;
                foreach (var item in items)
                {
                    if (str.Trim() != item)
                    {
                        continue;
                    }
                    flag = true;
                    break;
                }
                if (flag)
                {
                    continue;
                }
                result.Add(str.Trim());
            }
            return result.ToArray();
        }

        public static string StringToMD5Hash(string inputString, string encode = "ASCII")
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encryptedBytes = md5.ComputeHash(Encoding.GetEncoding(encode).GetBytes(inputString));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                sb.AppendFormat("{0:x2}", encryptedBytes[i]);
            }
            return sb.ToString();
        }

        public static string GenPassword()
        {
            return StringToMD5Hash(DateTime.Now.Ticks.ToString()).Substring(10);
        }

        public static int RandomNumber(int number, int min = 0)
        {
            return new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0)).Next(number) + min;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string CreateOrderID()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff") + RandomNumber(8999, 1000);
        }
        public static string CreateOrderID(string prefix)
        {
            return prefix + DateTime.Now.ToString("yyyyMMddHHmmssffff") + RandomNumber(8999, 1000);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string CreateKsdOrderID()
        {
            return "K" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + RandomNumber(8999, 1000);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string CreateCCBOrderID()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssff");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string CreateDeliveryID()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssff") + RandomNumber(89, 10);
        }
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeSpan()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeSpan(DateTime now)
        {
            TimeSpan ts = now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        /// <summary>
        /// 时间戳转换成日期
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime ConvertTimestamp(long timestamp)
        {
            DateTime converted = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            DateTime newDateTime = converted.AddSeconds(timestamp);
            return newDateTime.ToLocalTime();
        }
        /// <summary>
        /// 时间戳转换成日期
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime ConvertUnixTimestamp(long timestamp)
        {
            DateTime converted = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            DateTime newDateTime = converted.AddMilliseconds(timestamp);
            return newDateTime.ToLocalTime();
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetUnixTimeSpan()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetUnixTimeSpan(DateTime now)
        {
            TimeSpan ts = now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }
        /// <summary>
        /// 遮罩
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="start">前几位</param>
        /// <param name="end">后几位</param>
        /// <param name="maskCount">加*几位</param>
        /// <returns></returns>
        public static string Mask(string str, int start, int end, int maskCount)
        {
            var len = str.Length;
            if (len < start || len < end || len < start + end)
            {
                return string.Join("", Enumerable.Repeat("*", maskCount));
            }
            else
            {
                return str.Substring(0, start) + string.Join("", Enumerable.Repeat("*", maskCount)) + str.Substring(str.Length - end);
            }
        }

        /// <summary>
        /// 平摊金额
        /// </summary>
        /// <param name="totalAmount">总金额</param>
        /// <param name="thisAmount">本次金额</param>
        /// <param name="splitAmount">分摊金额</param>
        /// <param name="isLast">是否最后一次</param>
        /// <returns></returns>
        public static decimal GoShares(decimal totalAmount, decimal thisAmount, ref decimal splitAmount, bool isLast = false)
        {
            var scale = Math.Round(thisAmount / totalAmount, 6);
            var getAmount = Math.Round(splitAmount * scale, 2);

            if (getAmount >= splitAmount || isLast)
            {
                getAmount = splitAmount;
                splitAmount = 0;
            }
            else
            {
                splitAmount -= getAmount;
            }

            return getAmount;
        }

        ///// <summary>
        ///// 汉字转首字母
        ///// </summary>
        ///// <param name="strChinese"></param>
        ///// <returns></returns>
        //public static string GetFirstSpell(string strChinese)
        //{
        //    //NPinyin.Pinyin.GetInitials(strChinese)  有Bug  洺无法识别
        //    //return NPinyin.Pinyin.GetInitials(strChinese);

        //    try
        //    {
        //        if (strChinese.Length != 0)
        //        {
        //            StringBuilder fullSpell = new StringBuilder();
        //            for (int i = 0; i < strChinese.Length; i++)
        //            {
        //                var chr = strChinese[i];
        //                fullSpell.Append(GetSpell(chr)[0]);
        //            }

        //            return fullSpell.ToString().ToUpper();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        LogHelper.WriteError("首字母转化出错！" + e.Message);
        //        Console.WriteLine("首字母转化出错！" + e.Message);
        //    }

        //    return string.Empty;
        //}
        //private static string GetSpell(char chr)
        //{
        //    var coverchr = NPinyin.Pinyin.GetPinyin(chr);

        //    return coverchr;

        //}
        public class Pos
        {
            public double lng { get; set; }
            public double lat { get; set; }
        }
        public static bool IsPointInPolygon(double ALon, double ALat, List<Pos> APoints)
        {
            int iSum = 0, iCount;
            double dLon1, dLon2, dLat1, dLat2, dLon;
            if (APoints.Count < 3)
                return false;
            iCount = APoints.Count;
            for (int i = 0; i < iCount - 1; i++)
            {
                if (i == iCount - 1)
                {
                    dLon1 = APoints[i].lng;
                    dLat1 = APoints[i].lat;
                    dLon2 = APoints[0].lng;
                    dLat2 = APoints[0].lat;
                }
                else
                {
                    dLon1 = APoints[i].lng;
                    dLat1 = APoints[i].lat;
                    dLon2 = APoints[i + 1].lng;
                    dLat2 = APoints[i + 1].lat;
                }
                //以下语句判断A点是否在边的两端点的水平平行线之间，在则可能有交点，开始判断交点是否在左射线上
                if (((ALat >= dLat1) && (ALat < dLat2)) || ((ALat >= dLat2) && (ALat < dLat1)))
                {
                    if (Math.Abs(dLat1 - dLat2) > 0)
                    {
                        //得到 A点向左射线与边的交点的x坐标：
                        dLon = dLon1 - ((dLon1 - dLon2) * (dLat1 - ALat)) / (dLat1 - dLat2);

                        // 如果交点在A点左侧（说明是做射线与 边的交点），则射线与边的全部交点数加一：
                        if (dLon < ALon)
                            iSum++;
                    }
                }
            }
            if (iSum % 2 != 0)
                return true;
            return false;
        }

        public static bool IsPointInPolygon2(double ALon, double ALat, List<Pos> APoints)
        {
            int counter = 0;
            int i;
            double xinters;
            Pos p1, p2;
            int pointCount = APoints.Count;
            p1 = APoints[0];
            for (i = 1; i <= pointCount; i++)
            {
                p2 = APoints[i % pointCount];
                if (ALat > Math.Min(p1.lat, p2.lat)//校验点的Y大于线段端点的最小Y  
                    && ALat <= Math.Max(p1.lat, p2.lat))//校验点的Y小于线段端点的最大Y  
                {
                    if (ALon <= Math.Max(p1.lng, p2.lng))//校验点的X小于等线段端点的最大X(使用校验点的左射线判断).  
                    {
                        if (p1.lat != p2.lat)//线段不平行于X轴  
                        {
                            xinters = (ALat - p1.lat) * (p2.lng - p1.lng) / (p2.lat - p1.lat) + p1.lng;
                            if (p1.lng == p2.lng || ALon <= xinters)
                            {
                                counter++;
                            }
                        }
                    }

                }
                p1 = p2;
            }

            if (counter % 2 == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string FriendlyNumber(int delta)
        {
            if (delta < 1000)
            {
                return delta.ToString();
            }
            else if (delta < 10000)
            {
                return (delta / 1000.0).ToString("0.##k");
            }
            else
            {
                return (delta / 10000.0).ToString("0.##w");
            }
        }

        /// <summary>
        /// 简单还债算法
        /// </summary>
        /// <param name="total">欠债</param>
        /// <param name="leave">可还量</param>
        /// <param name="offset">还了多少(每次计算前都会重置0)</param>
        /// <returns>计算是否成功</returns>
        public static bool SimpleOffset(ref int total, ref int leave, out int offset)
        {
            offset = 0;
            if (total <= 0 || leave <= 0) return false;

            offset = Math.Min(total, leave);
            leave -= leave;
            total -= leave;

            return true;
        }



        #region 生成指定长度的随机字符串
        /// <summary>
        /// 生成指定长度的随机字符串
        /// </summary>
        /// <param name="intLength">随机字符串长度</param>
        /// <param name="booNumber">生成的字符串中是否包含数字</param>
        /// <param name="booSign">生成的字符串中是否包含符号</param>
        /// <param name="booSmallword">生成的字符串中是否包含小写字母</param>
        /// <param name="booBigword">生成的字符串中是否包含大写字母</param>
        /// <returns></returns>
        public static string GetRandomizer(int intLength, bool booNumber, bool booSign, bool booSmallword, bool booBigword)
        {
            //定义
            Random ranA = new Random();
            int intResultRound = 0;
            int intA = 0;
            string strB = "";

            while (intResultRound < intLength)
            {
                //生成随机数A，表示生成类型
                //1=数字，2=符号，3=小写字母，4=大写字母

                intA = ranA.Next(1, 5);

                //如果随机数A=1，则运行生成数字
                //生成随机数A，范围在0-10
                //把随机数A，转成字符
                //生成完，位数+1，字符串累加，结束本次循环

                if (intA == 1 && booNumber)
                {
                    intA = ranA.Next(0, 10);
                    strB = intA.ToString() + strB;
                    intResultRound = intResultRound + 1;
                    continue;
                }

                //如果随机数A=2，则运行生成符号
                //生成随机数A，表示生成值域
                //1：33-47值域，2：58-64值域，3：91-96值域，4：123-126值域

                if (intA == 2 && booSign == true)
                {
                    intA = ranA.Next(1, 5);

                    //如果A=1
                    //生成随机数A，33-47的Ascii码
                    //把随机数A，转成字符
                    //生成完，位数+1，字符串累加，结束本次循环

                    if (intA == 1)
                    {
                        intA = ranA.Next(33, 48);
                        strB = ((char)intA).ToString() + strB;
                        intResultRound = intResultRound + 1;
                        continue;
                    }

                    //如果A=2
                    //生成随机数A，58-64的Ascii码
                    //把随机数A，转成字符
                    //生成完，位数+1，字符串累加，结束本次循环

                    if (intA == 2)
                    {
                        intA = ranA.Next(58, 65);
                        strB = ((char)intA).ToString() + strB;
                        intResultRound = intResultRound + 1;
                        continue;
                    }

                    //如果A=3
                    //生成随机数A，91-96的Ascii码
                    //把随机数A，转成字符
                    //生成完，位数+1，字符串累加，结束本次循环

                    if (intA == 3)
                    {
                        intA = ranA.Next(91, 97);
                        strB = ((char)intA).ToString() + strB;
                        intResultRound = intResultRound + 1;
                        continue;
                    }

                    //如果A=4
                    //生成随机数A，123-126的Ascii码
                    //把随机数A，转成字符
                    //生成完，位数+1，字符串累加，结束本次循环

                    if (intA == 4)
                    {
                        intA = ranA.Next(123, 127);
                        strB = ((char)intA).ToString() + strB;
                        intResultRound = intResultRound + 1;
                        continue;
                    }

                }

                //如果随机数A=3，则运行生成小写字母
                //生成随机数A，范围在97-122
                //把随机数A，转成字符
                //生成完，位数+1，字符串累加，结束本次循环

                if (intA == 3 && booSmallword == true)
                {
                    intA = ranA.Next(97, 123);
                    strB = ((char)intA).ToString() + strB;
                    intResultRound = intResultRound + 1;
                    continue;
                }

                //如果随机数A=4，则运行生成大写字母
                //生成随机数A，范围在65-90
                //把随机数A，转成字符
                //生成完，位数+1，字符串累加，结束本次循环

                if (intA == 4 && booBigword == true)
                {
                    intA = ranA.Next(65, 89);
                    strB = ((char)intA).ToString() + strB;
                    intResultRound = intResultRound + 1;
                    continue;
                }
            }
            return strB;

        }
        #endregion
    }
}
