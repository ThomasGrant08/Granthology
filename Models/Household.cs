namespace Granthology.Models
{
    public class Household : RootModel
    {
        public string Name { get; set; }

        public virtual List<Person> Members { get; set; } = new List<Person>();

        public virtual required Address CurrentAddress { get; set; }

        public Household() : base()
        {

        }
    }
}
