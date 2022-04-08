using Microsoft.AspNetCore.Http;
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
    public class CartModel : PageModel
    {
        public IConfiguration _configuration { get; }
        private ILogger<CartModel> _logger;
        public CartModel(IConfiguration configuration, ILogger<CartModel> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public List<Product> productC;
        public List<Product> productList;
        public int[] ilosci;
        public decimal suma=0;
        public void OnGet()
        {

            var cookieValue = Request.Cookies["Cart"];
            int LastID;
            Product product;
            ///ODCZYT BAZY/////////////////////////////////////////////////////////////
            string newcook = "";
            var productList = new List<Product>();
            string myCompanyDBcs = _configuration.GetConnectionString("myCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDBcs);
            string sql = "SELECT * FROM Product";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                product = new Product(Int32.Parse(reader["Id"].ToString()), reader.GetString(1), Decimal.Parse(reader["Price"].ToString()));
                productList.Add(product);
                LastID = Int32.Parse(reader["Id"].ToString());
            }
            reader.Close();
            con.Close();
            //////////////////////////////////////////////////////////////////////////////
            ilosci = new int[productList.Count + 1];
            if (cookieValue != null)
            {
                for (int i = 0; i < cookieValue.Length; i++)
                {
                    ilosci[cookieValue[i] - 48]++;
                }
            }
            productC = new List<Product>();
            for (int i = 1; i <= productList.Count; i++)
            {
                if (ilosci[i] != 0)
                    productC.Add(productList[i - 1]);
            }
            foreach (var p in productC)
            {
                suma += p.price * ilosci[p.id];
            }
        }
        public IActionResult OnPost()
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            Response.Cookies.Append("Cart", "", cookieOptions);
            return RedirectToPage("Cart");
        }

    }
}
