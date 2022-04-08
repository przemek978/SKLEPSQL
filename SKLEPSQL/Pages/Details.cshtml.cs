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
    public class DetalisModel :PageModel
    {
        public IConfiguration _configuration { get; }
        private readonly ILogger<DetalisModel> _logger;
        public DetalisModel(IConfiguration configuration, ILogger<DetalisModel> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        [FromQuery(Name="id")]
        public int id { get; set; } 
        [BindProperty]
        public Product product { get; set; }
        public List<Product> productList;
        public void OnGet()
        {
            productList=DataBase.Read(_configuration);
            product = productList[id - 1];
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
            product = DataBase.Read(_configuration)[id - 1];
            return RedirectToPage("List");
        }
        /*public List<Product> Read()
        {
            int LastID;
            ///ODCZYT BAZY/////////////////////////////////////////////////////////////
            productList = new List<Product>();
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
            return productList;
            //////////////////////////////////////////////////////////////////////////////
        }*/

    }
}
