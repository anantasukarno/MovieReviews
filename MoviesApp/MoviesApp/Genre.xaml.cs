using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MoviesApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoviesApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Genre : ContentPage
    {
        List<GenreModel> dataGenre = new List<GenreModel>();
        public Genre()
        {
            InitializeComponent();
            GetJsonAsync();

        }
        
        public async Task GetJsonAsync()
        {
            var uri = new Uri("https://api.themoviedb.org/3/genre/movie/list");
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI4ZDVjMjhiNWVhOGYyNWM2N2U3YWRjMjY4ZmQ0Y2EyYyIsInN1YiI6IjYzZjQ4NTI1Y2FhY2EyMDA4NTc5MTdmOCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.vs5K7jHgdjD0qn_-AN3172rcwbL21VFpxs_fmwHbtLQ");
            var response = await httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                string json = content.ToString();
                var jsonObject = JObject.Parse(json);
                var data = jsonObject["genres"];
                var jsonArray = JArray.Parse(data.ToString());
                foreach(var token in jsonArray)
                {
                    GenreModel g = new GenreModel();
                    string id = (string)token["id"];
                    string name = token["name"].ToString();

                    g.id =int.Parse(id);
                    g.name = name;

                    dataGenre.Add(g);
                }
                
            }
            genreListView.ItemsSource = dataGenre;
        }

        public static string Dump(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        private void Button_Genre_Clicked(object sender, EventArgs e)
        {
            Button button1 = (Button)sender;
            string commandParameter = button1.CommandParameter.ToString();
            Navigation.PushAsync(new MovieList(commandParameter));
        }
    }
}