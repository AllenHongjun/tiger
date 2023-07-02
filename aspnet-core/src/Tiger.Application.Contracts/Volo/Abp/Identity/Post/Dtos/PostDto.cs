using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.Identity.Post.Dtos;

[Serializable]
public class PostDto : FullAuditedEntityDto<Guid>
{
    public string Remark { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    public int Sort { get; set; }

    public bool IsActive { get; set; }

}