using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Basic.Categorys;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Tiger.Business.Basic;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace Tiger.Basic
{
    public class CategoryAppService :
        CrudAppService<
            Category, //The  entity
            CategoryDto,
            Guid, //Primary key 
            GetCategoryListDto,
            CreateUpdateCategoryDto,
            CreateUpdateCategoryDto>, //Used to create/update
        ICategoryAppService
    {
        private readonly IDistributedCache<CategoryDto> _cache;


        private readonly ICategoryRepository _categoryRepository;
        public CategoryAppService(ICategoryRepository categoryRepository,
            IDistributedCache<CategoryDto> cache
            ) : base(categoryRepository)
        {
            _cache = cache;

            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// 获取分类列表树
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<List<CategoryDto>> GetAllListAsync(GetCategoryListDto input)
        {
            //var root =  _categoryRepository.Where(x => x.ParentId == null).ToList();

            // TODO: 把数据库查询 移到 Repository里面
            var root = ObjectMapper.Map<List<Category>, List<CategoryDto>>(
                       _categoryRepository.Where(x => x.ParentId == input.ParentId).ToList());
            foreach (var ouDto in root)
            {
                await TraverseTreeAsync(ouDto, ouDto.Children);
            }
            return root;
        }


        /// <summary>
        /// 获取分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override  async Task<CategoryDto> GetAsync(Guid id)
        {
            return await _cache.GetOrAddAsync(
               id.ToString(), //Cache key
               async () => {

                   return ObjectMapper.Map<Category, CategoryDto>(
                       await _categoryRepository.GetAsync(id)
                   );
               },
               () => new DistributedCacheEntryOptions
               {
                   AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
               }
           );
        }

        /// <summary>
        /// 获取分类树明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<CategoryDto> GetDetailsAsync(Guid id)
        {
            var ou = await _categoryRepository.GetAsync(id);
            var ouDto = ObjectMapper.Map<Category, CategoryDto>(ou);
            await TraverseTreeAsync(ouDto, ouDto.Children);
            return ouDto;
        }

        /// <summary>
        /// 根据父级id查找子分类
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public virtual async Task<List<CategoryDto>> GetChildrenAsync(Guid parentId)
        {
            //TODO:How to set is a leaf node when lazy loading
            var list = ObjectMapper.Map<List<Category>, List<CategoryDto>>(await _categoryRepository.GetChildrenAsync(parentId));
            await SetLeaf(list);
            return list;
        }

        /// <summary>
        /// 将列表转换为组织树
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="children"></param>
        /// <returns></returns>
        protected virtual async Task TraverseTreeAsync(CategoryDto dto, List<CategoryDto> children)
        {
            if (dto.Children.Count == 0)
            {
                children = await GetChildrenAsync(dto.Id);
                dto.Children.AddRange(children);
            }
            if (children == null || !children.Any())
            {
                await Task.CompletedTask;
                return;
            }
            foreach (var child in children)
            {
                var next = await GetChildrenAsync(child.Id);
                child.Children.AddRange(next);
                await TraverseTreeAsync(dto, child.Children);
            }
        }

        

        /// <summary>
        /// 设置为叶子节点
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        protected virtual async Task SetLeaf(List<CategoryDto> list)
        {
            foreach (var item in list)
            {
                if ((await _categoryRepository.GetChildrenAsync(item.Id)).Count == 0)
                {
                    item.SetLeaf();
                }
            }
        }

        
    }
}
