using AutoMapper;

using Tiger.Books;
using Tiger.SecurityLogs;
using Volo.Abp.AuditLogging;
using Volo.Abp.Identity;
using Volo.Abp.SecurityLog;

namespace Tiger
{
    public class TigerApplicationAutoMapperProfile : Profile
    {
        public TigerApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Book, BookDto>();
            CreateMap<CreateUpdateBookDto, Book>();
            CreateMap<SecurityLogInfo, SecurityLogDto>();
            CreateMap<AuditLog, AuditLogDto>();

            CreateMap<OrganizationUnit, OrganizationUnitDto>()
                .MapExtraProperties();
        }
    }
}
