using Tiger.Infrastructure.Location.Baidu.Model;

namespace Tiger.Infrastructure.Location.Baidu.Response
{
    public class BaiduReGeocodeResponse : BaiduLocationResponse
    {
        public BaiduReGeocode Result { get; set; } = new BaiduReGeocode();
    }
}
