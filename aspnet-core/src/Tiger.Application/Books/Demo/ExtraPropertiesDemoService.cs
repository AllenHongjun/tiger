



using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
/**
* 类    名：ExtraPropertiesDemoService   
* 作    者：花生了什么树       
* 创建时间：2021/8/16 12:40:39       
* 说    明: 
* 
*/
namespace Tiger.Books.Demo
{
    /// <summary>
    /// ABP定义了 IHasExtraProperties 接口,可以由实体实现,以便能够动态地设置和获取的实体属性. AggregateRoot 基类已经实现了 IHasExtraProperties 接口. 如果你从这个类(或者上面定义的一个相关审计类)派生,那么你可以直接使用API​.
    /// </summary>
    public class ExtraPropertiesDemoService : ITransientDependency
    {
        private readonly IIdentityUserRepository _identityUserRepository;

        public ExtraPropertiesDemoService(IIdentityUserRepository identityUserRepository)
        {
            _identityUserRepository = identityUserRepository;
        }

        public async Task SetTitle(Guid userId, string title)
        {
            var user = await _identityUserRepository.GetAsync(userId);

            //SET A PROPERTY
            user.SetProperty("Title", title);

            await _identityUserRepository.UpdateAsync(user);
        }

        public async Task<string> GetTitle(Guid userId)
        {
            var user = await _identityUserRepository.GetAsync(userId);

            //GET A PROPERTY
            return user.GetProperty<string>("Title");
        }
    }

}
