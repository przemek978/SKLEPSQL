using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SKLEPSQL.Models;
using System.Data.SqlClient;

namespace SKLEPSQL.Pages
{
    public class EditModel : PageModel
    {
        //////////////////////////////////////////////////////////////////////////////////////////
        public IConfiguration _configuration { get; }
        private readonly ILogger<EditModel> _logger;
        public EditModel(IConfiguration configuration, ILogger<EditModel> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        [FromQuery(Name="id")]
        public int id { get; set; }
        [BindProperty]
        public Product Product { get; set; }
        public void OnGet()
        {
            Product = DataBase.Read(_configuration)[id-1];
        }
        public IActionResult OnPost(Product Product)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string myCompanyDBcs = _configuration.GetConnectionString("myCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDBcs);
            string sql = "UPDATE Product SET name=@NAME, price=@PRICE WHERE id=@ID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@NAME", Product.name);
            cmd.Parameters.AddWithValue("@PRICE", Product.price);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToPage("List");
        }

    }
}
