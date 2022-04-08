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
        public IConfiguration _configuration { get; }
        private readonly ILogger<EditModel> _logger;
        public EditModel(IConfiguration configuration, ILogger<EditModel> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        [FromQuery(Name="id")]
        public int id { get; set; }
        [BindProperty]
        public Product prod { get; set; }
        public Product Product { get; set; }
        public IActionResult OnPost(Product p)
        {
            string myCompanyDBcs = _configuration.GetConnectionString("myCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDBcs);
            string sql = "UPDATE Product SET name=@NAME, price=@PRICE WHERE id=@ID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@ID", p.id);
            cmd.Parameters.AddWithValue("@NAME", p.name);
            cmd.Parameters.AddWithValue("@PRICE", p.price);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToPage("List");
        }

    }
}
