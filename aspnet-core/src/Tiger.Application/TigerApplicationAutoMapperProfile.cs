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
using Tiger.Business.Members;
using Tiger.Business.Members.Dtos;
using Tiger.Business.Stocks;
using Tiger.Business.Stocks.Dtos;
using Tiger.Marketing;
using Tiger.Marketing.Dtos;
using Tiger.Marketings.CouponHistorys;
using Tiger.Marketings.Coupons;
using Tiger.Orders;
using Tiger.Orders.CartItems;
using Tiger.Orders.OrderItems;
using Tiger.Orders.OrderOperateHistorys;
using Tiger.Orders.Orders;
using Tiger.Orders.OrderSettings;
using Tiger.SecurityLogs;
using Tiger.Stock;
using Tiger.Stock.Dtos;
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

            CreateMap<Store, StoreDto>();
            CreateMap<CreateUpdateStoreDto, Store>();
            #endregion

            #region Markting
            CreateMap<Coupon, CouponDto>();
            CreateMap<CreateUpdateCouponDto, Coupon>();

            CreateMap<CouponHistory, CouponHistoryDto>();
            CreateMap<CreateUpdateCouponHistoryDto, CouponHistory>();

            CreateMap<FlashPromotion, FlashPromotionDto>();
            CreateMap<CreateUpdateFlashPromotionDto, FlashPromotion>();

            CreateMap<FlashPromotionSession, FlashPromotionSessionDto>();
            CreateMap<CreateUpdateFlashPromotionSessionDto, FlashPromotionSession>();


            #endregion

            #region Members

            CreateMap<Member, MemberDto>();
            CreateMap<CreateUpdateMemberDto, Member>();

            CreateMap<MemberLevel, MemberLevelDto>();
            CreateMap<CreateUpdateMemberLevelDto, MemberLevel>();

            CreateMap<MemberLoginLog, MemberLoginLogDto>();
            CreateMap<CreateUpdateMemberLoginLogDto, MemberLoginLog>();

            CreateMap<MemberReceiveAddress, MemberReceiveAddressDto>();
            CreateMap<CreateUpdateMemberReceiveAddressDto, MemberReceiveAddress>();

            CreateMap<MemberStatisticInfo, MemberStatisticInfoDto>();
            CreateMap<CreateUpdateMemberStatisticInfoDto, MemberStatisticInfo>();
            #endregion

            #region Orders

            CreateMap<CartItem, CartItemDto>();
            CreateMap<CreateUpdateCartItemDto, CartItem>();

            CreateMap<Business.Orders.Order, OrderDto>();
            CreateMap<CreateUpdateOrderDto, Business.Orders.Order>();

            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<CreateUpdateOrderItemDto, OrderItem>();

            CreateMap<OrderOperateHistory, OrderOperationHistoryDto>();
            CreateMap<CreateUpdateOrderOperationHistoryDto, OrderOperateHistory>();

            CreateMap<OrderSetting, OrderSettingDto>();
            CreateMap<CreateUpdateOrderSettingDto, OrderSetting>();
            #endregion

            #region Stocks

            CreateMap<Inventory, InventoryDto>();
            CreateMap<CreateUpdateInventoryDto, Inventory>();

            CreateMap<InventoryHistory, InventoryHistoryDto>();
            CreateMap<CreateUpdateInventoryHistoryDto, InventoryHistory>();

            CreateMap<CheckHeader, CheckHeaderDto>();
            CreateMap<CreateUpdateCheckHeaderDto, CheckHeader>();

            CreateMap<CheckDetail, CheckDetailDto>();
            CreateMap<CreateUpdateCheckDetailDto, CheckDetail>();

            CreateMap<ReceiptHeader, ReceiptHeaderDto>();
            CreateMap<CreateUpdateReceiptHeaderDto, ReceiptHeader>();

            CreateMap<ReverseDetail, ReverseDetailDto>();
            CreateMap<CreateUpdateReverseDetailDto, ReverseDetail>();

            CreateMap<ShipmentHeader, ShipmentHeaderDto>();
            CreateMap<CreateUpdateShipmentHeaderDto, ShipmentHeader>();

            CreateMap<TransferHeader, TransferHeaderDto>();
            CreateMap<CreateUpdateTransferHeaderDto, TransferHeader>();

            #endregion

            





            CreateMap<SecurityLogInfo, SecurityLogDto>();
            CreateMap<AuditLog, AuditLogDto>();
            CreateMap<OrganizationUnit, OrganizationUnitDto>()
                .MapExtraProperties();
        }
    }
}
