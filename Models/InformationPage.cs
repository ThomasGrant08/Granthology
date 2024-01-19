using System.ComponentModel.DataAnnotations;

namespace Granthology.Models
{
    public class InformationPage : RootModel
    {

        #region Properties

        [Required]
        public required string Title { get; set; }
        public virtual List<ContentSection> ContentSections { get; set; } = new List<ContentSection>();
        public string Slug { get; set; }

        #endregion

        #region Ctor

        public InformationPage() : base()
        {
            Slug = Guid.NewGuid().ToString();
        }

        #endregion

    }

    public class PersonPage : InformationPage
    {
        #region Properties

        public virtual Person Person { get; set; }

        #endregion

        #region Ctor

        public PersonPage() : base()
        {

        }

        #endregion
    }

}
