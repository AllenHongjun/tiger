using Newtonsoft.Json;
using Tiger.Infrastructure.Location.Tencent.Model;

namespace Tiger.Infrastructure.Location.Tencent.Response
{
    public class TencentIPGeocodeResponse : TencentLocationResponse
    {
        /// <summary>
        /// IP定位结果
        /// </summary>
        [JsonProperty("result")]
        public TencentIPGeocode Result { get; set; } = new TencentIPGeocode();
    }
}
