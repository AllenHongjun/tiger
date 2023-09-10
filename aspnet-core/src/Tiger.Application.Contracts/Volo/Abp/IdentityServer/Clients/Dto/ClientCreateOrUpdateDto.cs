using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.Validation;

namespace Tiger.Volo.Abp.IdentityServer.Clients
{
    public class ClientCreateOrUpdateDto
    {
        /// <summary>
        /// 客户端Id
        /// </summary>
        [Required]
        [MaxLength(ClientConsts.ClientIdMaxLength)]

        public string ClientId { get; set; }

        /// <summary>
        /// 客户端名称
        /// </summary>
        [Required]
        [MaxLength(ClientConsts.ClientNameMaxLength)]
        public string ClientName { get; set; }

        /// <summary>
        /// 客户端描述
        /// </summary>
        [MaxLength(ClientConsts.DescriptionMaxLength)]
        public string Description { get; set; }

        /// <summary>
        /// 允许的授权类型
        /// </summary>
        public List<ClientGrantTypeDto> AllowedGrantTypes { get; set; }

        protected ClientCreateOrUpdateDto()
        {
            AllowedGrantTypes = new List<ClientGrantTypeDto>();
        }
    }
}
