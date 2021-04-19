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
    public class BienController : Controller
    {
        // GET: Bien
        [HttpGet]
       public async Task<ActionResult> AfficherProprietaire()
        {
            IEnumerable<Bien> model = new List<Bien>();
            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync
                    (
                    "http://localhost:81/ApiCC/api/Bien"
                    );
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<IEnumerable<Bien>>(json);
                }
            }
            return View(model);
        }


        public async Task<ActionResult> Create()
        {
           Bien model = new Bien();
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync
                    (
                    "http://localhost:81/ApiCC/api/Proprietaire"
                    );
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var proprietaires = JsonConvert.DeserializeObject<IEnumerable<Proprietaire>>(json);
                 model.Proprietaires=proprietaires.Select
                        (
                        x =>
                                    new SelectListItem
                                    {
                                        Text = x.Nom,
                                        Value = x.Id.ToString()

                                    }
                                    );
                }
            }
            return View(model);
        }

        
           


        public async Task<ActionResult> Edit(int id)
        {
            Bien model = new Bien();
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync
                    (
                    "http://localhost:81/ApiCC/api/Bien?id=" + id
                    );
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<Bien>(json);
                }
                response = await client.GetAsync
                   (
                    "http://localhost:81/ApiCC/api/Proprietaire"
                   );

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var proprietaires = JsonConvert.DeserializeObject<IEnumerable<Proprietaire>>(json);
                    model.Proprietaires = proprietaires.Select
                           (
                        x =>
                        new SelectListItem
                        {
                            Text = x.Nom,
                            Value = x.Id.ToString(),
                           Selected = x.Id == model.Proprietaire.Id
                        }
                        );
                }

            }
            return View("Edit",model);
        }




        [HttpPost]
        public async Task<ActionResult> Edit(Bien model)
        {
            try
            {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync
                    (
                    "http://localhost:81/ApiCC/api/Proprietaire"
                    );
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var proprietaires= JsonConvert.DeserializeObject<IEnumerable<Proprietaire>>(json);
                    model.Proprietaires = proprietaires.Select
                        (
                        x =>
                        new SelectListItem
                        {
                            Text = x.Nom,
                            Value=x.Id.ToString(),
                            Selected = model.IdProprietaire == x.Id

                        }
                        ); 
                }
            }


                if (ModelState.IsValid)
                {
                    MultipartFormDataContent multipart = new MultipartFormDataContent();
                    var json = JsonConvert.SerializeObject(model);
                    StringContent content = new StringContent
                        (
                        json,
                        Encoding.UTF8,
                        "application/json"
                        );
                    multipart.Add(content, "data");
                    if(model.Image.ContentLength > 0)
                    {
                        byte[] picture = new byte[model.Image.ContentLength];
                        model.Image.InputStream.Read(picture,0,picture.Length);
                        ByteArrayContent byteContent = new ByteArrayContent(picture);
                        multipart.Add(byteContent, "picture", model.Image.FileName);
                    }
                   
                    using(HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response;
                        if (model.Id == 0)
                        {
                            response = await client.PostAsync
                                (
                                "http://localhost:81/ApiCC/api/Bien",
                                multipart
                                );
                        }
                        else
                        {
                            response = await client.PutAsync
                                (
                                "http://localhost:81/ApiCC/api/Bien",
                                multipart
                                );
                        }
                    }
                    return RedirectToAction("AfficherProprietaire");
                }
            }
            catch(Exception ex)
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
                    "http://localhost:81/ApiCC/api/Bien?id=" + id
                    );
            }
            return RedirectToAction("AfficherProprietaire");
        }
    }
}
