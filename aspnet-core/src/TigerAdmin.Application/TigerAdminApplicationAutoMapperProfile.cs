using AutoMapper;
using TigerAdmin.Books;

namespace TigerAdmin
{
    public class TigerAdminApplicationAutoMapperProfile : Profile
    {
        public TigerAdminApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Book, BookDto>();
            CreateMap<CreateUpdateBookDto, Book>();
        }
    }
}
