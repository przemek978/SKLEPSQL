using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SKLEPSQL.Pages
{
    public class AddCartModel : PageModel
    {
        [FromQuery(Name = "id")]
        public int id { get; set; }
        public IActionResult OnGet()
        {
            if (Request.Cookies["Cart"] == null)
            {
                Response.Cookies.Append("Cart", id.ToString());
            }
            else
            {
                var cookieValue = Request.Cookies["Cart"];
                cookieValue += id.ToString();
                Response.Cookies.Append("Cart", cookieValue);
            }
            return RedirectToPage("List");
        }
    }
}
