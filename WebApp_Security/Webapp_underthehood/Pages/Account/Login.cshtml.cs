using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Webapp_underthehood.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credntal Credential { get; set; }=new Credntal();
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
                };

                var identity= new ClaimsIdentity(claims,"MycookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

               await HttpContext.SignInAsync("MycookieAuth", claimsPrincipal);

                return RedirectToPage("/Index");

            }

            return Page();
        }

    }


    public class Credntal
    {
        [Required]
        [Display(Name ="User Name")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }


    }
}
