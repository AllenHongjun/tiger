using Newtonsoft.Json;
using Tiger.Infrastructure.Location.Tencent.Model;

namespace Tiger.Infrastructure.Location.Tencent.Response
{
    public class TencentGeocodeResponse : TencentLocationResponse
    {
        /// <summary>
        /// 地址解析结果
        /// </summary>
        [JsonProperty("result")]
        public TencentGeocode Result { get; set; } = new TencentGeocode();
    }
}
