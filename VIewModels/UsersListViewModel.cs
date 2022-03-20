using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GuniPortal.VIewModels
{
    public class UsersListViewModel
    {
        [Key]
        [Display(Name="User Id")]
        public Guid User_Id { get; set; }
        
        [Display(Name= "User Name")]
        public string User_Name { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "User Name")]
        public string Display_Name { get; set; }

        [Display(Name = "Role Of the User")]
        public List<string> RolesOfUser { get; set; }

    }
}
