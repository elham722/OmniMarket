using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OmniMarket.Identity.Models;

namespace OmniMarket.Identity.Context
{
  public class OmniMarketIdentityDbContext:IdentityDbContext<ApplicationUser>
    {
    }
}
