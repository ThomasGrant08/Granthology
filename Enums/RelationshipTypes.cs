using System.ComponentModel.DataAnnotations;

namespace Granthology.Enums
{
    public enum RelationshipType
    {
        [Display(Name = "Parent - Child")]
        ParentChild,
        [Display(Name = "Partner")]
        Partner
    }
}
