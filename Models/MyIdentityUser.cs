using GuniPortal.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// ◘  This Model Interact with General User data 
/// -- Act as A parent Model Of Faculty and Student Model..
/// 
/// </summary>
namespace GuniPortal.Models
{
    public class MyIdentityUser
        :IdentityUser<Guid>
    {
        [Display(Name = "Display Name")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [MinLength(2, ErrorMessage = "{0} should have at least {1} characters.")]
        [StringLength(60, ErrorMessage = "{0} cannot have more than {1} characters.")]
        public string DisplayName { get; set; }      

       
        [Display(Name = "Mobile Number")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [Phone]
        [StringLength(10)]
        public string Mobile_no { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [Required]
        [PersonalData]
        [Column(TypeName = "smalldatetime")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Gender")]
        [PersonalData]
        public Genders Gender { get; set; }

        [Display(Name = "Is Admin User?")]
        [Required]
        public bool IsAdminUser { get; set; }=false;

        #region Navigational Properties to the Student Model (1:0 mapping)

        public Student Student { get; set; }

        #endregion


        #region Navigational Properties to the Faculty Model (1:0 mapping)

        public Faculty Faculty { get; set; }

        #endregion

    }
}