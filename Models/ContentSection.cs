namespace Granthology.Models
{
    public class ContentSection : RootModel
    {
        #region Properties

        public string? Content { get; set; }
        public int Order { get; set; }
        public virtual InformationPage InformationPage { get; set; }

        #endregion

        #region Ctor
        public ContentSection() : base()
        {

        }
        #endregion

    }
}
