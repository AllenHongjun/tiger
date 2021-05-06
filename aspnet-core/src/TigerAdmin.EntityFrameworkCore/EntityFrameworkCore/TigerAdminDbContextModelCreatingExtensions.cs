using Microsoft.EntityFrameworkCore;
using TigerAdmin.Books;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace TigerAdmin.EntityFrameworkCore
{
    public static class TigerAdminDbContextModelCreatingExtensions
    {
        public static void ConfigureTigerAdmin(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(TigerAdminConsts.DbTablePrefix + "YourEntities", TigerAdminConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});

            builder.Entity<Book>(b =>
            {
                b.ToTable(TigerAdminConsts.DbTablePrefix + "Books",
                          TigerAdminConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            });
        }
    }
}