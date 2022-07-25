using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LERproject.Class;
using LERproject.Models;
using Microsoft.Extensions.Options;

namespace LERproject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        #region variables 
        private WeatherConfig _weathercon;

        [BindProperty]
        public List<Weather> WeatherBroadcast { get; set; }

        [BindProperty (SupportsGet = true)]
        public string zipCode { get; set; }

        #endregion variables



        public IndexModel(ILogger<IndexModel> logger, IOptions<WeatherConfig> weatherConfig)
        {
            _logger = logger;

            _weathercon = weatherConfig.Value;

            //this.
        }

        public void OnGet()
        {
            if (!string.IsNullOrWhiteSpace(zipCode))
            {
                var webApiClient = new WebApiClient("http://api.weatherunlocked.com/api/forecast/us.", zipCode, _weathercon);

                WeatherBroadcast = webApiClient.queryApi();
            }
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid == false)
            {
                return RedirectToPage("/Index");
            }

            return RedirectToPage("/Index", new { zipCode } );
        }
    }
}
