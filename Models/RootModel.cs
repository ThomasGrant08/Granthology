namespace Granthology.Models
{
    public class RootModel
    {
        #region Properties
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        #endregion

        #region Ctor

        public RootModel()
        {
            CreatedAt = DateTime.Now;
            LastUpdatedAt = DateTime.Now;
        }

        #endregion

    }
}
