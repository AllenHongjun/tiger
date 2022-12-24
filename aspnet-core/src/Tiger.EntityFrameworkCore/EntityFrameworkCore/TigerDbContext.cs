using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq.Expressions;
using Tiger.Books;
using Tiger.Business.Demo;
using Tiger.CoreModule.DataFiltiering;
using Tiger.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using Volo.Abp.Users.EntityFrameworkCore;

namespace Tiger.EntityFrameworkCore
{

    /// <summary>
    /// 自定义的DbContext
    /// </summary>
    /// 
    /// 

    // 配置连接字符串选择
    [ConnectionStringName("Default")]  
    public class TigerDbContext : AbpDbContext<TigerDbContext>
    {

        /* This is your actual DbContext used on runtime.
         * It includes only your entities.
         * It does not include entities of the used modules, because each module has already
         * its own DbContext class. If you want to share some database tables with the used modules,
         * just create a structure like done for AppUser.
         *
         * Don't use this DbContext for database migrations since it does not contain tables of the
         * used modules (as explained above). See TigerMigrationsDbContext for migrations.
         * 
         * 模块通常使用 ConnectionStringName attribute 为 DbContext 类关联一个唯一的连接字符串名称. 示例:
         */

        public DbSet<AppUser> Users { get; set; }

        #region Demo
        //3.EF Core需要你将实体和 DbContext 建立关联.最简单的做法是在Acme.BookStore.EntityFrameworkCore项目的BookStoreDbContext类中添加DbSet属性.如下所示:
        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; } 
        #endregion


        

        public TigerDbContext(DbContextOptions<TigerDbContext> options)
            : base(options)
        {

        }

        /// <summary>
        /// https://docs.microsoft.com/zh-cn/ef/core/dbcontext-configuration/#other-dbcontext-configuration
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        /// <summary>
        /// 重写 OnModelCreating 方法并且做一些配置
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Always call the base method
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

            builder.Entity<AppUser>(b =>
            {
                b.ToTable(AbpIdentityDbProperties.DbTablePrefix + "Users"); //Sharing the same table "AbpUsers" with the IdentityUser
                
                b.ConfigureByConvention();
                b.ConfigureAbpUser();

                /* Configure mappings for your additional properties
                 * Also see the TigerEfCoreEntityExtensionMappings class
                 */
            });

            /* Configure your own tables/entities inside the ConfigureTiger method */

            builder.ConfigureTiger();
        }


        #region 自定义数据过滤
        /// <summary>
        /// 检查是否启用了 IsActive . 内部使用了之前介绍到的 IDataFilter 服务.
        /// </summary>
        protected bool IsActiveFilterEnabled => DataFilter?.IsEnabled<IsActive>() ?? false;


        /// <summary>
        /// EF全局过滤系统 https://learn.microsoft.com/en-us/ef/core/querying/filters
        /// 实现自定义过滤的最佳方法是为重写你的 DbContext 的 ShouldFilterEntity 和 CreateFilterExpression 方法
        /// 重写 ShouldFilterEntity 和 CreateFilterExpression 方法检查给定实体是否实现 IsActive 接口
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entityType"></param>
        /// <returns></returns>
        protected override bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType)
        {
            if (typeof(IsActive).IsAssignableFrom(typeof(TEntity)))
            {
                return true;
            }

            return base.ShouldFilterEntity<TEntity>(entityType);
        }

        protected override Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
        {
            var expression = base.CreateFilterExpression<TEntity>();

            if (typeof(IsActive).IsAssignableFrom(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> isActiveFilter =
                    e => !IsActiveFilterEnabled || EF.Property<bool>(e, "IsActive");
                expression = expression == null
                    ? isActiveFilter
                    : CombineExpressions(expression, isActiveFilter);
            }
            return expression;
        } 
        #endregion

    }
}
