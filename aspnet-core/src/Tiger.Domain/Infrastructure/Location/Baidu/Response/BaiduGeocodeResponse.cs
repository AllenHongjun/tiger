using Tiger.Infrastructure.Location.Baidu.Model;

namespace Tiger.Infrastructure.Location.Baidu.Response
{
    public class BaiduGeocodeResponse : BaiduLocationResponse
    {
        public BaiduGeocode Result { get; set; } = new BaiduGeocode();
    }
}
