using OmniMarket.Application.Features.Product.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmniMarket.Common.Dtos.User;

namespace OmniMarket.Application.Features.Users.Queries
{
    public class GetAllUsersQueryHandler(IProductRepository productRepository, IMapper mapper) 
        : IRequestHandler<GetAllUsersQuery, IReadOnlyList<UserDto>>
    {
        public Task<IReadOnlyList<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
