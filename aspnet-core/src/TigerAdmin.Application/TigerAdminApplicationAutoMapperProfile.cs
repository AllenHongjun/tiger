using AutoMapper;

using TigerAdmin.Books;
using TigerAdmin.SecurityLogs;
using Volo.Abp.AuditLogging;
using Volo.Abp.Identity;
using Volo.Abp.SecurityLog;

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
            CreateMap<SecurityLogInfo, SecurityLogDto>();
            CreateMap<AuditLog, AuditLogDto>();

            CreateMap<OrganizationUnit, OrganizationUnitDto>()
                .MapExtraProperties();
        }
    }
}
