using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Volo.Abp.Sass.Editions
{
    [Serializable]
    public class EditionEto
    {
        public Guid Id { get;set; }

        public string DisplayName { get; set; }
    }
}
