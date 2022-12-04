using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;

namespace Tiger.Volo.Abp.Identity.ClaimTypes.Dto
{
    public class CreateClaimTypeDto : ExtensibleObject
    {
        public string Name { get; set; }

        public bool Required { get; set; }

        public string Regex { get; set; }

        public string RegexDescription { get; set; }

        public string Description { get; set; }

        public IdentityClaimValueType ValueType { get; set; }
    }
}
