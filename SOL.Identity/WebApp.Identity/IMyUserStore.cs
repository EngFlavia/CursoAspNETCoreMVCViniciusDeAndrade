using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Identity
{
    public interface IMyUserStore : IUserStore<MyUser, IdentityResult>
    {
    }
}
