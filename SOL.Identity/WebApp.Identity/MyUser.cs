
using Microsoft.AspNet.Identity;

namespace WebApp.Identity
{
    public class MyUser /*: IUser*/
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
