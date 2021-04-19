using AspApplicationCC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AspApplicationCC.Controllers
{
    public class ProprietaireController : Controller
    {
        // GET: Proprietaire
        //public async Task<ActionResult> AfficherProprietaire()
        //{
        //    IEnumerable<Proprietaire> model = new List<Proprietaire>();
        //    using (HttpClient client = new HttpClient())
        //    {
        //        var response = await client.GetAsync("http://localhost:81/ApiCC/api/Proprietaire");
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var json = await response.Content.ReadAsStringAsync();
        //            model = JsonConvert.DeserializeObject<IEnumerable<Proprietaire>>(json);
        //        }
        //    }
        //    return View(model);
        //}

        [HttpGet]
        public async Task<ActionResult> AfficherProprietaire()
        {
            IEnumerable<Proprietaire> model = new List<Proprietaire>();
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync
                    (
                    "http://localhost:81/ApiCC/api/Proprietaire"
                    );
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<IEnumerable<Proprietaire>>(json);
                }
            }
            return View(model);
        }


        public ActionResult Create()
        {
            Proprietaire model = new Proprietaire();
            return View(model);
        }





        public async Task<ActionResult> Edit(int id)
        {
            Proprietaire model = new Proprietaire();
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync
                    (
                    "http://localhost:81/ApiCC/api/Proprietaire?id=" + id
                    );
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<Proprietaire>(json);
                }
               
            }
            return View("Edit", model);
        }




        [HttpPost]
        public async Task<ActionResult> Edit(Proprietaire model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var json = JsonConvert.SerializeObject(model);
                    StringContent content = new StringContent
                        (
                        json,
                        Encoding.UTF8,
                        "application/json"
                        );
                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response;
                        if (model.Id == 0)
                        {
                            response = await client.PostAsync
                                (
                                "http://localhost:81/ApiCC/api/Proprietaire",
                                content
                                );
                        }
                        else
                        {
                            response = await client.PutAsync
                                (
                                "http://localhost:81/ApiCC/api/Proprietaire",
                                content
                                );
                        }
                    }
                    return RedirectToAction("AfficherProprietaire");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(model);
        }






        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.DeleteAsync
                    (
                    "http://localhost:81/ApiCC/api/Proprietaire?id=" + id
                    );
            }
            return RedirectToAction("AfficherProprietaire");
        }
    }
}
