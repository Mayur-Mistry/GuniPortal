using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuniPortal.Models
{
    [Table("Departments")]
    public class Department
    {
        [Key]
        [Display(Name = "Department Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Department_Id { get; set; }

        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [StringLength(20, ErrorMessage = "{0} cannot contain more than {1} characters.")]
        public string Department_Name { get; set; }

        #region Navigation Properties to the Assignments Model
        public ICollection<Assignment> Assignments{ get; set; }
        #endregion
        #region Navigation Properties to the Assignments Model
        public ICollection<Faculty> Faculty { get; set; }
        #endregion


    }
}
