using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SKLEPSQL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SKLEPSQL.Pages
{
    public class CartDelModel : PageModel
    {
        //////////////////////////////////////////////////////////////////////////////////////////
        public IConfiguration _configuration { get; }
        private readonly ILogger<CartDelModel> _logger;
        public CartDelModel(IConfiguration configuration, ILogger<CartDelModel> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        [FromQuery(Name = "id")]
        public int id { get; set; }
        public List<Product> productList;
        public List<Product> productC;
        public Product product { get; set; }
        public int[] ilosci;
        public decimal suma = 0;

        public IActionResult OnGet()
        {
            //LoadDB();
            var cookieValue = Request.Cookies["Cart"];
            string newcook = "";
            productList = DataBase.Read(_configuration);
            ilosci = new int[productList.Count + 1];
            if (cookieValue != null)
            {
                for (int i = 0; i < cookieValue.Length; i++)
                {
                    ilosci[cookieValue[i] - 48]++;
                }
            }
            ilosci[id]--;
            productC = new List<Product>();
            for (int i = 1; i <= productList.Count; i++)
            {
                if (ilosci[i] != 0)
                    productC.Add(productList[i - 1]);
            }
            for (int i = 1; i <=productList.Count; i++)
            {
                for (int j = 0; j < ilosci[i]; j++)
                {
                    newcook += productList[i - 1].id.ToString();
                }
            }
            Response.Cookies.Append("Cart",newcook);
            return RedirectToPage("Cart");
        }
    }
}
