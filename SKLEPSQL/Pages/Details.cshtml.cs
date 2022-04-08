using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SKLEPSQL.Models;
using System.Collections.Generic;

namespace SKLEPSQL.Pages
{
    public class DetalisModel :MyPageModel
    {
        public DetalisModel(IConfiguration configuration, ILogger<MyPageModel> logger) : base(configuration, logger)
        {
        }

        [FromQuery(Name="id")]
        public int id { get; set; } 
        public Product prod { get; set; }
        [BindProperty]
        public Product Product { get; set; }    
        public void OnGet()
        {
            LoadDB();
            prod = productDB.List()[id-1];
            //SaveDB();
        }
        public void OnPost()
        {
            AddCart(id);
        }
        public IActionResult AddCart(int id)
        {
            if (Request.Cookies["Cart"] == null)
            {
                Response.Cookies.Append("Cart",id.ToString());
            }
            else
            {
                var cookieValue = Request.Cookies["Cart"];
                cookieValue += id.ToString();
                Response.Cookies.Append("Cart", cookieValue);
            }
            LoadDB();
            prod = productDB.List()[id - 1];
            return RedirectToPage("List");
        }

    }
}
