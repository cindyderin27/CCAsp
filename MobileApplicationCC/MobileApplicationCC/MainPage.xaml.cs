using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileApplicationCC
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
          
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            IEnumerable<Models.Bien> model = new List<Models.Bien>();
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                var response = await client.GetAsync("http://10.0.2.2:81/ApiCC/api/Bien");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    model = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Models.Bien>>(json);
                }
            }
            collection.ItemsSource = model;
        }

        private void btn_Create(object sender, EventArgs e)
        {

        }
    }
}

