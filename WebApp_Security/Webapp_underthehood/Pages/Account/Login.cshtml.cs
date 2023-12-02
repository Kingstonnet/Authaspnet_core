using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Webapp_underthehood.Authorization;

namespace Webapp_underthehood.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credntial Credential { get; set; }=new Credntial();
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            if(Credential.UserName=="admin" && Credential.Password=="as")// verify credentials
            {
                //create the security context;
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name,"admin"),
                    new Claim(ClaimTypes.Email,"admin@a.com"),      //generate security context
                    new Claim("Dept","HR"),
                    new Claim("admin","true"),
                    new Claim("manager","true"),
                    new Claim("empdate","2021-05-01")

                };

                var identity= new ClaimsIdentity(claims,"MycookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = Credential.RememberMe
                };

                await HttpContext.SignInAsync("MycookieAuth", claimsPrincipal, authProperties);

                return RedirectToPage("/Index");

            }

            return Page();
        }

    }


    //public class Credntial
    //{
    //    [Required]
    //    [Display(Name ="User Name")]
    //    public string UserName { get; set; } = string.Empty;

    //    [Required]
    //    [DataType(DataType.Password)]
    //    public string Password { get; set; } = string.Empty;

    //    public bool RememberMe { get; set; }


    //}
}
