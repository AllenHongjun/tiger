using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;

namespace Tiger.Volo.Abp.Sass.Editions
{
    /// <summary>
    /// 版本数据种子
    /// </summary>
    public class EditionDataSeeder : IEditionDataSeeder, ITransientDependency
    {
        public EditionDataSeeder(
            IGuidGenerator guidGenerator, 
            IEditionRepository editionRepository)
        {
            GuidGenerator=guidGenerator;
            EditionRepository=editionRepository;
        }

        protected IGuidGenerator GuidGenerator { get; set; }

        protected IEditionRepository EditionRepository { get; set; }


        public async virtual Task SeedDefaultEditionAsync()
        {
            await AddEditionIfNotExistsAsync("Free");
            await AddEditionIfNotExistsAsync("Standard");
            await AddEditionIfNotExistsAsync("Professional");
            await AddEditionIfNotExistsAsync("Enterprise");
        }

        /// <summary>
        /// 如果版本不存在就添加
        /// </summary>
        /// <param name="displayName"></param>
        /// <returns></returns>
        protected async virtual Task AddEditionIfNotExistsAsync(string displayName)
        {
            if (await EditionRepository.FindByDisplayNameAsync(displayName) != null)
            {
                return;
            }

            // 使用abp自带的 guid生成接口
            await EditionRepository.InsertAsync(
                new Edition(GuidGenerator.Create(),
                displayName));
        }
    }
}
