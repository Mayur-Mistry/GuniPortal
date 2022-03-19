using GuniPortal.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuniPortal.Models
{
    [Table("Faculties")]
    public class Faculty
    {
        /// <summary>
        ///     Mapped to the ID column of the Identity User
        /// </summary>
        [Display(Name = "User ID")]
        [Key]
        [ForeignKey(nameof(Faculty.User))]
        public Guid UserId { get; set; }

        [Display(Name = "Type of Faculty")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [MinLength(3, ErrorMessage = "{0} should have more than {1} characters.")]
        [StringLength(25, ErrorMessage = "{0} cannot contain more than {1} characters.")]
        public Faculties FacultyType { get; set; }

        #region Navigation Properties to the Department Model 

        [Display(Name = "Department Id")]
        [Required]
        [ForeignKey(nameof(Faculty.Department))]      
        public int Department_Id { get; set; }           
        public Department Department { get; set; }

        #endregion
        #region Navigational Properties to the MyIdentityUser model (1:1 mapping)

        public MyIdentityUser User { get; set; }

        #endregion

        public ICollection<Submission> submissions { get; set; }

    }
}
