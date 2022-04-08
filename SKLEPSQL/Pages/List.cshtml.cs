using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SKLEPSQL.DAL;
using SKLEPSQL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SKLEPSQL.Pages
{
    public class ListModel : PageModel
    {
        //[BindProperty(SupportsGet = true)]
        public List<Product> productList { get; set; }
        public Product product;
        public int LastID;
        public IConfiguration _configuration { get; }

        private readonly ILogger<ListModel> _logger;
        public ListModel(IConfiguration configuration, ILogger<ListModel> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public void OnGet()
        {
            productList= new List<Product>();
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
        }

    }
}
