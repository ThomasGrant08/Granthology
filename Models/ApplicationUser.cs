using Microsoft.AspNetCore.Identity;

namespace Granthology.Models
{
    public class ApplicationUser : IdentityUser
    {
        #region Properties

        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public byte[]? ProfilePicture { get; set; }

        #endregion

        #region Ctor

        public ApplicationUser()
        {


        }

        #endregion

    }
}
