using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.System.Area.Dtos;

[Serializable]
public class RegionDto : FullAuditedEntityDto<int>
{
    public short Level { get; set; }

    public long ParentCode { get; set; }

    public long AreaCode { get; set; }

    public string ZipCode { get; set; }

    public string CityCode { get; set; }

    public string Name { get; set; }

    public string ShortName { get; set; }

    public string MergerName { get; set; }

    public string Pinyin { get; set; }

    public decimal Lng { get; set; }

    public decimal Lat { get; set; }
}