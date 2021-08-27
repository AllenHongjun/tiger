using AutoMapper;
using Tiger.Basic;
using Tiger.Basic.Categorys;
using Tiger.Basic.Comments;
using Tiger.Basic.Dtos;
using Tiger.Basic.ProductAttributes;
using Tiger.Basic.ProductAttributeTpyes;
using Tiger.Basic.Products;
using Tiger.Books;
using Tiger.Business.Demo;
using Tiger.Marketing;
using Tiger.Marketings.CouponHistorys;
using Tiger.Marketings.Coupons;
using Tiger.SecurityLogs;
using Volo.Abp.AuditLogging;
using Volo.Abp.Identity;
using Volo.Abp.SecurityLog;

namespace Tiger
{
    /// <summary>
    /// AutoMapper提供了多种定义类之间映射的方法. 有关详细信息请参阅AutoMapper的文档.
    ///其中定义一种映射的方法是创建一个Profile 类.例如:
    /// </summary>
    public class TigerApplicationAutoMapperProfile : Profile
    {
        public TigerApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Book, BookDto>();
            CreateMap<CreateUpdateBookDto, Book>();
            CreateMap<Author, AuthorLookupDto>();
            CreateMap<Author, AuthorDto>();

            #region Basic
            CreateMap<Basic.Product, ProductDto>();
            CreateMap<CreateUpdateProductDto, Basic.Product>();

            CreateMap<ProductAttributeType, ProductAttributeTypeDto>();
            CreateMap<CreateUpdateProductAttributeTypeDto, ProductAttributeType>();

            CreateMap<ProductAttribute, ProductAttributeDto>();
            CreateMap<CreateUpdateProductAttributeDto, ProductAttribute>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CreateUpdateCategoryDto, Category>();

            CreateMap<Comment, CommentDto>();
            CreateMap<CreateUpdateCommentDto, Comment>();

            CreateMap<Warehouse, WarehouseDto>();
            CreateMap<WarehouseCreateDto, Warehouse>();
            CreateMap<WarehouseUpdateDto, Warehouse>();

            CreateMap<Supply, SupplyDto>();
            CreateMap<CreateUpdateSupplyDto, Supply>();
            #endregion

            #region Markting
            CreateMap<Coupon, CouponDto>();
            CreateMap<CreateUpdateCouponDto, Coupon>();

            CreateMap<CouponHistory, CouponHistoryDto>();
            CreateMap<CreateUpdateCouponHistoryDto, CouponHistory>(); 
            #endregion





            CreateMap<SecurityLogInfo, SecurityLogDto>();
            CreateMap<AuditLog, AuditLogDto>();
            CreateMap<OrganizationUnit, OrganizationUnitDto>()
                .MapExtraProperties();
        }
    }
}
