using Newtonsoft.Json;
using Tiger.Infrastructure.Location.Tencent.Model;

namespace Tiger.Infrastructure.Location.Tencent.Response
{
    public class TencentReGeocodeResponse : TencentLocationResponse
    {
        /// <summary>
        /// 逆地址解析结果
        /// </summary>
        [JsonProperty("result")]
        public TencentReGeocode Result { get; set; } = new TencentReGeocode();
    }
}
