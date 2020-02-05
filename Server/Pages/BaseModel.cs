using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Remotely.Server.Pages
{
    public class BaseModel : PageModel
    {
        public string DefaultPrompt { get; set; }
    }
}