/**
 * 类    名：UserAppService   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 15:03:09       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Business.Demo;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Tiger.Books.Demo
{
    /// <summary>
    /// 对象 到 对象的 映射
    /// http://automapper.org/
    /// 
    /// </summary>

    //[RemoteService(false)]
    //public class UserAppService : ApplicationService
    //{
    //    private readonly IRepository<User, Guid> _userRepository;
    //    private readonly IObjectMapper<TigerApplicationModule> _objectMapper;

    //    public UserAppService(IRepository<User, Guid> userRepository)
    //    {
    //        _userRepository = userRepository;
    //    }

    //    public void CreateUser(CreateUserInput input)
    //    {
    //        //Manually creating a User object from the CreateUserInput object
    //        // 将对象映射到另一个对象是常用并且繁琐重复的工作,大部分情况下两个类都具有相同或相似的属性
    //        // 但在实际应用程序中 User 实体会拥有很多属性,手动创建实体乏味且容易出错. User 和 CreateUserInput 添加新属性时还需要再去修改代码.
    //        var user = new User
    //        {
    //            Name = input.Name,
    //            Surname = input.Surname,
    //            EmailAddress = input.EmailAddress,
    //            Password = input.Password
    //        };

    //        //Automatically creating a new User object using the CreateUserInput object
    //        // 一行代码自动就将对象关系 映射过去  真舒服
    //        //示例中的 ObjectMapper 属性在 ApplicationService 基类中属性注入. 在其他地方也可以直接注入 IObjectMapper 接口.

    //        // 注意 ApplicationSevice 父类当中有很多的方法可以使用。。有很多的属性
    //        user = ObjectMapper.Map<CreateUserInput, User>(input);


    //        _userRepository.InsertAsync(user);
    //    }


    //    public async Task UpdateUserAsync(Guid id, UpdateUserInput input)
    //    {
    //        var user = await _userRepository.GetAsync(id);

    //        //Automatically set properties of the user object using the UpdateUserInput
    //        ObjectMapper.Map<UpdateUserInput, User>(input, user);

    //        await _userRepository.UpdateAsync(user);
    //    }
    //}
}
