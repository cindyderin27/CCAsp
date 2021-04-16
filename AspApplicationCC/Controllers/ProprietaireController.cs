using AspApplicationCC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AspApplicationCC.Controllers
{
    public class ProprietaireController : Controller
    {
        // GET: Proprietaire
        public async Task<ActionResult> AfficherProprietaire()
        {
            IEnumerable<Proprietaire> model = new List<Proprietaire>();
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:81/ApiCC/api/Proprietaire");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<IEnumerable<Proprietaire>>(json);
                }
            }
            return View(model);
        }


    }
}