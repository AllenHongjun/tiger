using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.IdentityServer.Clients
{
    public class ClientGetByPagedDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
