using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmniMarket.Application.DTOs.User;

namespace OmniMarket.Application.Features.Users.Queries
{
   public class GetAllUsersQuery : IRequest<IReadOnlyList<UserDto>>
    {
    }
}
