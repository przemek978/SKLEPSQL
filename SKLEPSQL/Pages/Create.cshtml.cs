using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SKLEPSQL.Models;
using System;
using System.Data.SqlClient;

namespace SKLEPSQL.Pages
{
    public class CreateModel : PageModel
    {
        public IConfiguration _configuration { get; }
        private readonly ILogger<CreateModel> _logger;
        public CreateModel(IConfiguration configuration, ILogger<CreateModel> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        [BindProperty]
        public Product newProduct { get; set; }
        public int id;
        public void OnGet(int ID)
        {
            id = ID;
        }
        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string myCompanyDBcs = _configuration.GetConnectionString("myCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDBcs);
            SqlCommand cmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql = "";
            sql = "Insert into Product(ID,Name,Price) values(@ID,@Name,@Price)";
            cmd = new SqlCommand(sql, con);
            adapter.InsertCommand = cmd;
            cmd.Parameters.AddWithValue("@ID", id+1);
            cmd.Parameters.AddWithValue("@Name", newProduct.name);
            cmd.Parameters.AddWithValue("@Price", newProduct.price);
            con.Open();
            adapter.InsertCommand.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            return RedirectToPage("List");
        }

    }
}
