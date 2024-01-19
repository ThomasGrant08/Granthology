using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;

namespace Granthology.Models
{
    public class Person : RootModel
    {
        #region Properties

        #region Name

        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Display(Name = "Middle Name(s)")]
        public string? MiddleName { get; set; }

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        #endregion

        #region Birth and Death

        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Date of Death")]
        public DateTime? DateOfDeath { get; set; }

        [Display(Name = "Place of Birth")]
        public string? PlaceOfBirth { get; set; }

        [Display(Name = "Place of Death")]
        public string? PlaceOfDeath { get; set; }

        [Display(Name = "Is Deceased")]
        public bool IsDeceased { get; set; }

        #endregion

        #region Relationships

        public virtual List<Relationship> Relationships { get; set; } = new List<Relationship>();
        //public virtual Household? Household { get; set; }

        #endregion

        #region Content

        //public virtual PersonPage? Page { get; set; }

        #endregion

        //public virtual ApplicationUser? User { get; set; }

        #endregion

        #region Ctor

        public Person()
        {

        }


        #endregion

        #region Methods


        /// <summary>
        /// Retrieves the full name formatted as "FirstName MiddleName LastName."
        /// </summary>
        /// <returns>The formatted full name string.</returns>
        public string GetName()
        {
            return $"{FirstName} {MiddleName} {LastName}";
        }

        /// <summary>
        /// Retrieves a formatted name based on the specified format string.
        /// Placeholder tokens in the format string are replaced with actual name values.
        /// Supported placeholders: {FirstName}, {MiddleName}, {LastName}, {FirstInitial}, {MiddleInitial}, {LastInitial}.
        /// </summary>
        /// <param name="format">The format string with placeholders for name values.</param>
        /// <returns>The formatted name string.</returns>
        public string GetName(string format)
        {
            string result = format.Replace("{FirstName}", FirstName ?? "")
                .Replace("{MiddleName}", MiddleName ?? "")
                .Replace("{LastName}", LastName ?? "")
                .Replace("{FirstInitial}", FirstName?.Length > 0 ? FirstName[0].ToString() : "")
                .Replace("{MiddleInitial}", MiddleName?.Length > 0 ? MiddleName[0].ToString() : "")
                .Replace("{LastInitial}", LastName?.Length > 0 ? LastName[0].ToString() : "");

            return result;
        }


        #endregion

    }
}
