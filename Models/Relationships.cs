using Granthology.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Granthology.Models
{
    public class Relationship : RootModel
    {
        [Required]
        public virtual Person PersonA { get; set; }
        public int PersonAId { get; set; }

        [Required]
        public virtual Person PersonB { get; set; }
        public int PersonBId { get; set; }

        public string RelationshipTypeDiscriminator { get; set; } // Discriminator property

        [NotMapped]
        public RelationshipType RelationshipType
        {
            get
            {
                if (Enum.TryParse(RelationshipTypeDiscriminator, out RelationshipType result))
                {
                    return result;
                }
                else
                {
                    return default(RelationshipType);
                }
            }
            set => RelationshipTypeDiscriminator = value.ToString();
        }

    }

    public class ParentChildRelationship : Relationship
    {
        public bool IsBiological { get; set; }

    }

    public class  PartnerRelationship : Relationship
    {
        public bool IsCurrent { get; set; }
    }
}
