using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SKLEPSQL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKLEPSQL.Pages
{
    public class IndexModel : PageModel
    {
        //////////////////////////////////////////////////////////////////////////////////////////
        public IConfiguration _configuration { get; }
        private readonly ILogger<IndexModel> _logger;
        public IndexModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        public List<Product> productList { get; set; }
        public Product product { get; set; }
        public IActionResult OnGet()
        {
            return RedirectToPage("List");
        }
        
    }
}
