using Tiger.Infrastructure.Location.Baidu.Model;

namespace Tiger.Infrastructure.Location.Baidu.Response
{
    public class BaiduIpGeocodeResponse : BaiduLocationResponse
    {
        public string Address { get; set; }

        public Content Content { get; set; } = new Content();
    }
}
