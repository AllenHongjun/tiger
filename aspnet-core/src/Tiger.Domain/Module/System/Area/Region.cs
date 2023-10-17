using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Module.System.Area
{
    /// <summary>
    /// 地区表
    /// </summary>
    /// <remarks>
    /// 数据源中国五级行政区域库 https://github.com/kakuilan/china_area_mysql
    /// </remarks>
    public class Region : FullAuditedAggregateRoot<int>
    {
        /// <summary>
        /// 地区级别 0-省、自治区、直辖市 1-地级市、地区、自治州、盟 2-市辖区、县级市、县 3-街道、镇
        /// </summary>
        public short Level { get; set; }

        /// <summary>
        /// 父级行政地区编码
        /// </summary>
        public long ParentCode { get; set; }

        /// <summary>
        /// 行政地区编码
        /// </summary>
        public long AreaCode { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// 区号
        /// </summary>
        public string CityCode { get; set; }

        /// <summary>
        /// 地区名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 地区缩写(只有省才有)
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// 组合名称
        /// </summary>
        public string MergerName { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string Pinyin { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public decimal Lng { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public decimal Lat { get; set; }


    protected Region()
    {
    }

    public Region(
        short level,
        long parentCode,
        long areaCode,
        string zipCode,
        string cityCode,
        string name,
        string shortName,
        string mergerName,
        string pinyin,
        decimal lng,
        decimal lat
    ) : base()
    {
        Level = level;
        ParentCode = parentCode;
        AreaCode = areaCode;
        ZipCode = zipCode;
        CityCode = cityCode;
        Name = name;
        ShortName = shortName;
        MergerName = mergerName;
        Pinyin = pinyin;
        Lng = lng;
        Lat = lat;
    }
    }
}
