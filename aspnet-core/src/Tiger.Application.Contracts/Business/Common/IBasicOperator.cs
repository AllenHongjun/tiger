using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Business.Common
{
    public interface IBasicOperator<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    {
        Task DeleteAsync(TKey id);

        Task AuditAsync(TKey id);

        Task<TGetOutputDto> CopyAsync(TKey id, TUpdateInput input);

        Task MarkAsync(TKey id);

        //Task BatchDelete(Guid[] guids);

        //Task BatchAudit(Guid[] guids);

        //Task BatchClose(Guid[] guids);
    }
}
