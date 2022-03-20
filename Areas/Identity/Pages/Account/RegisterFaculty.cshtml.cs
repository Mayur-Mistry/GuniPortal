using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using GuniPortal.Data;
using GuniPortal.Models;
using GuniPortal.Models.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;



namespace GuniPortal.Areas.Identity.Pages.Account
{
    [Authorize(Roles ="Administrator")]
    public class RegisterFacultyModel : PageModel
    {
        private readonly SignInManager<MyIdentityUser> _signInManager;
        private readonly UserManager<MyIdentityUser> _userManager;
        private readonly ILogger<RegisterFacultyModel> _logger;
        private readonly IEmailSender _emailSender;
        public readonly ApplicationDbContext _context;


        public RegisterFacultyModel(
            UserManager<MyIdentityUser> userManager,
            SignInManager<MyIdentityUser> signInManager,
            ApplicationDbContext context,
            ILogger<RegisterFacultyModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _logger = logger;
            _emailSender = emailSender;
        }


        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }


            [Display(Name = "Display Name")]
            [Required(ErrorMessage = "{0} cannot be empty.")]
            [MinLength(3, ErrorMessage = "{0} should have at least {1} characters.")]
            [StringLength(60, ErrorMessage = "{0} cannot have more than {1} characters.")]
            public string DisplayName { get; set; }


            [Display(Name = "Mobile Number")]
            [Required(ErrorMessage = "{0} cannot be empty.")]
            [Phone]
            [StringLength(10)]
            public string Mobile_no { get; set; }

            [Display(Name = "Date of Birth")]
            [Required]
            [DataType(DataType.Date)]
            [PersonalData]
            [Column(TypeName = "smalldatetime")]
            public DateTime DateOfBirth { get; set; }

            [Required]
            [Display(Name = "Gender")]
            [PersonalData]
            public Genders Gender { get; set; }

            

            [Display(Name = "Year Of Experience")]
            [Required(ErrorMessage = "{0} cannot be empty.")]
            public int Experience { get; set; }

            [Display(Name = "Is Admin User?")]
            [Required]
            public bool IsAdminUser { get; set; } = false;
        }

     


 
        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }
        
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var Password = Input.DisplayName + "@1234";
                var user = new MyIdentityUser { 
                    UserName = Input.Email,
                    Email = Input.Email,
                    DisplayName= Input.DisplayName,
                    DateOfBirth = Input.DateOfBirth,
                    Gender = Input.Gender,
                    Mobile_no = Input.Mobile_no,
                    IsAdminUser=Input.IsAdminUser

                };
                var result = await _userManager.CreateAsync(user, Password);
                var faculty = new Faculty
                {
                    UserId = user.Id,
                    Experience=Input.Experience
                    
                };

                _context.Add(faculty);
                if (result.Succeeded )
                {
                    
                    await _userManager.AddToRolesAsync(user, new string[] { 
                    MyIdentityRoleNames.Faculty.ToString()
                    });
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
