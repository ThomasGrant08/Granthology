using Granthology.Data.Migrations;
using Granthology.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Granthology.Extensions
{
    public static class PersonExtensions
    {
        public static List<Person> GetParents(this Person currentPerson)
        {
            return currentPerson.Relationships
                .Where(r => r.RelationshipType == Enums.RelationshipType.ParentChild)
                .Select(r => r.PersonA)
                .ToList();
        }

        public static List<Person> GetChildren(this Person currentPerson)
        {
            return currentPerson.Relationships
                .Where(r => r.RelationshipType == Enums.RelationshipType.ParentChild)
                .Select(r => r.PersonB)
                .ToList();
        }

        public static List<Person> GetPartners(this Person currentPerson)
        {
            return currentPerson.Relationships
                .Where(r => r.RelationshipType == Enums.RelationshipType.Partner)
                .Select(r => r.PersonA.Id == currentPerson.Id ? r.PersonB : r.PersonA)
                .ToList();
        }

        public static List<Person> GetSiblings(this Person currentPerson)
        {
            return currentPerson.GetParents()
                .SelectMany(p => p.GetChildren())
                .Where(c => c.Id != currentPerson.Id)
                .ToList();
        }

    }
}
