using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Volo.Abp.SettingUi
{
    [Serializable]
    public class SettingInfo
    {
        public string Name { get; set; }  
        public string DisplayName { get; set; }
        public string Description { get; set; }

        public string Value { get; set; }
        public Dictionary<string, object> Properties { get; set; }
        public string Permission { get; set; }
    }
}
