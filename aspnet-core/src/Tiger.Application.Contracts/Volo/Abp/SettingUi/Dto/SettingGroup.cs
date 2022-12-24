using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Volo.Abp.SettingUi
{
    [Serializable]
    public class SettingGroup
    {
        public string GroupName { get; set; }
        public string GroupDisplayName { get; set; }
        public List<SettingInfo> SettingInfos { get; set; }
        public string Permission { get; set; }
    }
}
