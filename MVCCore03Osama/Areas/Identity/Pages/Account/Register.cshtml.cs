using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using MVCCore03Osama.Email;
using MVCCore03Osama.Models;

namespace MVCCore03Osama.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment webHostEnvironment;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IWebHostEnvironment _webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            this.webHostEnvironment = _webHostEnvironment;
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

            [Required(ErrorMessage = "FName name is required")]
            [StringLength(20, MinimumLength = 3,
            ErrorMessage = "FName should be minimum 3 characters and a maximum of 20 characters")]
            [DataType(DataType.Text)]
            public string Fname { get; set; }


            [Required(ErrorMessage = "Role name is required")]
            public Role UserRole { set; get; }

            [Required(ErrorMessage = "LName name is required")]
            [StringLength(20, MinimumLength = 3,
            ErrorMessage = "LName should be minimum 3 characters and a maximum of 20 characters")]
            [DataType(DataType.Text)]
            public string Lname { get; set; }

            [DataType(DataType.Text)]
            public string Bio { set; get; }

            [DisplayName("Image Name")]
            public string ImgName { set; get; }

            [NotMapped]
            [DisplayName("Upload File")]
            public IFormFile ImageFile { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [DataType(DataType.Text)]


            //[Required(ErrorMessage = "Degree name is required")]
            //[StringLength(100, MinimumLength = 3,
            //ErrorMessage = "Degree name should be minimum 3 characters and a maximum of 100 characters")]
            //[DataType(DataType.Text)]

            public string Degree { get; set; }

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                string wwwrootPath = webHostEnvironment.WebRootPath;
                string email_path = Path.Combine(wwwrootPath + "/Images/", Input.Email);
                if (!Directory.Exists(email_path))
                {
                    Directory.CreateDirectory(email_path);
                }
                string fileName = "def.jfif";
                if (Input.ImageFile != null)
                {
                    fileName = Path.GetFileNameWithoutExtension(Input.ImageFile.FileName);
                    string extention = Path.GetExtension(Input.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                    string path = Path.Combine(wwwrootPath + "/Images/" + Input.Email + "/" + fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await Input.ImageFile.CopyToAsync(fileStream);
                    }
                }
                
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Fname = Input.Fname,
                    Lname = Input.Lname,
                    Bio = Input.Bio,
                    ImgName = fileName,
                    UserRole = Input.UserRole,
                    status=Status.Pendding
                };
                var result = await _userManager.CreateAsync(user, Input.Password);

                

                //if (result.Succeeded)
                //{
                //    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //    var confirmationLink = Url.Action("/Account/ConfirmEmail", "Email", new { token, email = user.Email }, Request.Scheme);
                //    EmailHelper emailHelper = new EmailHelper();
                //    bool emailResponse = emailHelper.SendEmail(user.Email, confirmationLink);

                //    if (emailResponse)
                //    {

                //    }
                //        //return RedirectToAction("Index");
                //    else
                //    {
                //        // log email failed 
                //    }
                //}


                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    
                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        EmailHelper emailHelper = new EmailHelper();
                        bool emailResponse = emailHelper.SendEmail(user.Email, $"Please confirm your account by clicking here {callbackUrl} .");

                        //return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
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
