using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SKLEPSQL.Models
{
    public static class DataBase
    {
        public static List<Product> Read(IConfiguration configuration)
        {
            int LastID;
            ///ODCZYT BAZY/////////////////////////////////////////////////////////////
            var productList = new List<Product>();
            Product product;
            string myCompanyDBcs = configuration.GetConnectionString("myCompanyDB");
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
        }
    }
}
