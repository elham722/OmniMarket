using OmniMarket.Application.DTOs.Base;
using OmniMarket.Application.DTOs.User;

namespace OmniMarket.Application.Features.User.Queries
{
    public class GetUsersQuery : IRequest<PagedResponse<UserDto>>
    {
        public PagingRequest PagingRequest { get; set; }

        public GetUsersQuery(PagingRequest pagingRequest)
        {
            PagingRequest = pagingRequest;
        }
    }
}