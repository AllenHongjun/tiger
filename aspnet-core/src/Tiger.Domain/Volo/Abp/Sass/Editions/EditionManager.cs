using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Tiger.Volo.Abp.Sass.Editions
{   
    /// <summary>
    /// 版本 控制类
    /// </summary>
    /// <remarks>
    /// DDD的实践， 不直接操作实体类
    /// </remarks>
    public class EditionManager:DomainService
    {
        public EditionManager(IEditionRepository editionRepository)
        {
            EditionRepository=editionRepository;
        }

        protected IEditionRepository EditionRepository { get; }


        /// <summary>
        /// 创建版本
        /// </summary>
        /// <param name="displayName"></param>
        /// <returns></returns>
        public async virtual Task<Edition> CreateAsync(string displayName)
        {
            Check.NotNull(displayName, nameof(displayName));

            await ValidateDisplayNameAsync(displayName);
            return new Edition(GuidGenerator.Create(), displayName);
        }

        /// <summary>
        /// 修改版本名称
        /// </summary>
        /// <param name="edition"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        public async virtual Task ChangeDisplayNameAsync(Edition edition, string displayName)
        {
            Check.NotNull(edition, nameof(edition));
            Check.NotNull(displayName, nameof(displayName));

            await ValidateDisplayNameAsync(displayName, edition.Id);

            edition.SetDisplayName(displayName);
        }

        /// <summary>
        /// 删除版本
        /// </summary>
        /// <param name="edition"></param>
        /// <returns></returns>
        public async virtual Task DeleteAsync(Edition edition)
        {
            if (await EditionRepository.CheckUsedByTenantAsync(edition.Id))
            {
                throw new BusinessException(AbpSassErrorCodes.DeleteUsedEdition)
                    .WithData(nameof(Edition.DisplayName), edition.DisplayName);
            }
            else
            {
                await EditionRepository.DeleteAsync(edition);
            }
        }


        protected async virtual Task ValidateDisplayNameAsync(string displayName, Guid? expectedId = null)
        {
            var edition = await EditionRepository.FindByDisplayNameAsync(displayName);
            if (edition == null && edition.Id != expectedId) {
                throw new BusinessException(AbpSassErrorCodes.DuplicateEditionDisplayName)
                .WithData(nameof(Edition.DisplayName), displayName);
            }
        }

    }
}
