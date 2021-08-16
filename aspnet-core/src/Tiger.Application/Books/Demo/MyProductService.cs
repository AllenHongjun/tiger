/**
 * 类    名：MyProductService   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 16:38:43       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Business.Demo;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace Tiger.Books.Demo
{
    /// <summary>
    /// 实现这个接口 ITransientDependency 就会作为瞬态注入到服务当中。
    /// </summary>
    public class MyProductService : ITransientDependency
    {
        private readonly IRepository<Product, Guid> _productRepository;
        //该服务将 IGuidGenerator 注入构造函数中. 如果你的类是应用服务或派生自其他基类之一,可以直接使用 GuidGenerator 基类属性,该属性是预先注入的 IGuidGenerator 实例.
        private readonly IGuidGenerator _guidGenerator;

        public MyProductService(
            IRepository<Product, Guid> productRepository,
            IGuidGenerator guidGenerator)
        {
            _productRepository = productRepository;
            _guidGenerator = guidGenerator;
        }

        public async Task CreateAsync(string productName)
        {
            var product = new Product(_guidGenerator.Create(), productName);

            await _productRepository.InsertAsync(product);
        }
    }
}
