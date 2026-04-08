using Microsoft.AspNetCore.Identity;

namespace FurnitureStore.Models
    

{
    

    namespace UserRoles.Models
    {
        public class Users : IdentityUser
        {
            public string FullName { get; set; }
        }
    }
}
