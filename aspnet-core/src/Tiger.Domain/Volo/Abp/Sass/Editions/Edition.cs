using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Volo.Abp.Sass.Editions
{   
    /// <summary>
    /// 版本
    /// </summary>
    public class Edition :FullAuditedAggregateRoot<Guid>
    {
        public virtual string DisplayName { get; protected set; }

        protected Edition()
        {

        }

        protected internal Edition(Guid id, [NotNull] string displayName)
            :base(id)
        {
            SetDisplayName(displayName);
        }

        protected internal virtual void SetDisplayName([NotNull] string displayName)
        {
            DisplayName = Check.NotNullOrWhiteSpace(
                displayName,
                nameof(displayName),
                1024);
        }
    }
}
