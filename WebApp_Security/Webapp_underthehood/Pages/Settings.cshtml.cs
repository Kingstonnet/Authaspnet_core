using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Webapp_underthehood.Pages
{
    [Authorize(Policy = "admin")]
    public class SettingsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
