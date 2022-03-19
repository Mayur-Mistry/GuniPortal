    using GuniPortal.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuniPortal.Models
{
    [Table("Submissions")]
    public class Submission
    {
        [Display(Name = "Submission Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Submission_Id { get; set; }


        [Display(Name = "Research Start Date")]
        [Required(ErrorMessage = "{0} cannot be empty")]
        public DateTime StartingDate { get; set; }

        [Display(Name = "Research Start Date")]
        [Required(ErrorMessage = "{0} cannot be empty")]
        public DateTime EndingDate { get; set; }

        [Display(Name = "Status")]
        [Required]
        [MaxLength(1)]
        // Reject = 0, Pending=1,Approved=2
        public SubmissionStatus Status { get; set; } = SubmissionStatus.Pending;
        //------------F1

       

        [Required]
        [ForeignKey(nameof(Submission.Faculty))]
        public Guid Faculty_Id { get; set; }
        public Faculty Faculty{ get; set; }

        [Required]
        [ForeignKey(nameof(Submission.Assignment))]
        public int Assignment_Id { get; set; }
        public Assignment Assignment { get; set; }



    }
}