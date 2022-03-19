using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuniPortal.Models
{
    [Table("Assignments")]
    public class Assignment
    {
        [Display(Name = "Assignment Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Assignment_Id { get; set; }

        [Display(Name = "Student ID / Author")]
        [Key]
        [Required]
        [ForeignKey(nameof(Assignment.student))]
        public Guid Student_Id { get; set; }        
        
        [Display(Name = "Assignment Title")]
        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [MinLength(5, ErrorMessage = "{0} should have more than {1} characters.")]
        [StringLength(50, ErrorMessage = "{0} cannot contain more than {1} characters.")]
        public string Assignment_Title { get; set; }

        [Display(Name = "Assignment Description")]        
        [MaxLength]
        [MinLength(20, ErrorMessage = "{0} should have more than {1} characters.")]
        [Column(TypeName = "varchar")]
        public string Assignment_Discription { get; set; }

        [Display(Name = "Document")]
        [MaxLength]
        [Required(ErrorMessage = "{0} cannot be empty")]
        [Column(TypeName = "varchar")]
        public string Document { get; set; }

      
        #region Navigation Properties to the Department Model 

        [Display(Name = "Department Name")]
        [Required]
        [ForeignKey(nameof(Assignment.Department))]      
        public int Department_Id { get; set; }           

        public Department Department { get; set; }

        #endregion


        #region Navigational Properties to the Student model (1:1 mapping)

        public Student student{ get; set; }

        #endregion


        #region Navigation Properties to the Submission Model
        public ICollection<Submission> submissions { get; set; }
        #endregion
    }
}