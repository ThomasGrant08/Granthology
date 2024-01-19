using System.ComponentModel.DataAnnotations;

namespace Granthology.Models
{
    public class Address : RootModel
    {
        [Required]
        public required string Line1 { get; set; }

        public string? Line2 { get; set; }

        [Required]
        public required string Town { get; set; }

        [Required]
        public required string County { get; set; }

        [Required]
        public required string Postcode { get; set; }

        public string? Instructions { get; set; }

        public Address() : base()
        {

        }

    }
}
