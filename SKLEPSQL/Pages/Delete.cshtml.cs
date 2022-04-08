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
    public class DeleteModel : PageModel
    {
        ////////////////////////////////////////////////////////////////////////////////////////
        public IConfiguration _configuration { get; }
        private ILogger<DeleteModel> _logger;
        public DeleteModel(IConfiguration configuration, ILogger<DeleteModel> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        ////////////////////////////////////////////////////////////////////////////////////////
        [FromQuery(Name = "id")]
        public int id { get; set; }
        public int LastID { get; set; }
        [BindProperty]
        public Product Product { get; set; }
        public List<Product> productList;
        public IActionResult OnGet(int id)
        {
            string myCompanyDBcs = _configuration.GetConnectionString("myCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDBcs);
            SqlCommand cmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql;
            sql = "Delete from Product where id= @ID";
            cmd = new SqlCommand(sql, con);
            adapter.DeleteCommand = cmd;
            cmd.Parameters.AddWithValue("@ID", id);
            con.Open();
            adapter.DeleteCommand.ExecuteNonQuery();
            cmd.Dispose();
            //Lastid
            sql = "SELECT COUNT(ID) FROM PRODUCT";
            cmd = new SqlCommand(sql, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                LastID = Int32.Parse(reader[0].ToString());
            }
            reader.Close();
            //Edit id
            for (int i = id+1; i <= LastID+1; i++)
            {
                sql = "UPDATE Product SET id=@ID where id=@i";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@i", i);
                cmd.Parameters.AddWithValue("@ID", i-1);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            updatecook(id);
            return RedirectToPage("List");
        }
        public void updatecook(int id)
        {
            var cookieValue = Request.Cookies["Cart"];
            string newcook = "";
            productList = DataBase.Read(_configuration); 
            
            var ilosci = new int[productList.Count + 2];
            var pr = productList;
            int i =0,j=0;
            if (cookieValue != null)
            {
                for (i = 0; i < cookieValue.Length; i++)
                {
                    ilosci[cookieValue[i] - 48]++;
                }
            }
            for(i=id; i<pr.Count+1;i++)
            {
                ilosci[i] = ilosci[i + 1];
            }
            ilosci[i] = 0;
            for (i = 1; i <= pr.Count; i++)
            {
                for ( j=0; j<ilosci[i]; j++)
                {
                    newcook += pr[i-1].id.ToString();
                }
            }
            Response.Cookies.Append("Cart", newcook);
        }
    }
}
