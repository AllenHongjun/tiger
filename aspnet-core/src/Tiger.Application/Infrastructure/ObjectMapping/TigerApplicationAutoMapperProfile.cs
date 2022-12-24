using AutoMapper;
using Tiger.Books;
using Tiger.Business.Demo;

namespace Tiger.Infrastructure.ObjectMapping
{
    /// <summary>
    /// AutoMapper提供了多种定义类之间映射的方法. 有关详细信息请参阅AutoMapper的文档.
    ///其中定义一种映射的方法是创建一个Profile 类.例如:
    /// </summary>
    public class TigerApplicationAutoMapperProfile : Profile
    {
        public TigerApplicationAutoMapperProfile()
        {
            #region BookStore
            /* You can configure your AutoMapper mapping configuration here.
                 * Alternatively, you can split your mapping configurations
                 * into multiple profile classes for a better organization. */
            CreateMap<Book, BookDto>();
            CreateMap<CreateUpdateBookDto, Book>();
            CreateMap<Author, AuthorLookupDto>();
            CreateMap<Author, AuthorDto>(); 
            #endregion

            

            #region Volo系统设置
            

            

            
            #endregion
        }
    }
}
