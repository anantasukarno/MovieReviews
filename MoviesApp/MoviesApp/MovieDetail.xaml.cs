using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MoviesApp.Models;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoviesApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieDetail : ContentPage
    {
        MovieDetailModel movie = new MovieDetailModel();
        public MovieDetail(string idMovie)
        {
            InitializeComponent();
            GetMovieDetail(idMovie);
        }

        public async Task GetMovieDetail(string idMovie)
        {
            var uri = new Uri($@"https://api.themoviedb.org/3/movie/{idMovie}");
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI4ZDVjMjhiNWVhOGYyNWM2N2U3YWRjMjY4ZmQ0Y2EyYyIsInN1YiI6IjYzZjQ4NTI1Y2FhY2EyMDA4NTc5MTdmOCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.vs5K7jHgdjD0qn_-AN3172rcwbL21VFpxs_fmwHbtLQ");
            var response = await httpClient.GetAsync(uri);
            Debug.WriteLine(response);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                string json = content.ToString();
                var data = JObject.Parse(json);
                movie.id = (int)data["id"];
                movie.backdrop_path = $@"https://image.tmdb.org/t/p/original" + (string)data["backdrop_path"];
                movie.poster_path = $@"https://image.tmdb.org/t/p/original" + (string)data["poster_path"];
                movie.budget = (int)data["budget"];
               
                movie.title= (string)data["title"];
                movie.adult = (bool)data["adult"];
                movie.original_language = (string)data["original_language"];
                movie.original_title= (string)data["original_title"];
                movie.overview = (string)data["overview"];
                movie.popularity = (int)data["popularity"];
                movie.release_date = (string)data["release_date"];
                movie.revenue = (int)data["revenue"];
                movie.runtime = (int)data["runtime"];
                movie.status = (string)data["status"];
                movie.tagline = (string)data["tagline"];
                movie.vote_average = (double)data["vote_average"];
                movie.vote_count = (int)data["vote_count"];

                List<Models.Genre> listGenres = new List<Models.Genre>();
                foreach (var g in data["genres"])
                {
                    Models.Genre genre = new Models.Genre();
                    genre.id = (int)g["id"];
                    genre.name = (string)g["name"];

                    listGenres.Add(genre);

                }

                movie.genres = listGenres;

                List<Models.ProductionCompany> productionCompanies= new List<Models.ProductionCompany>();
                foreach(var prodComp in data["production_companies"])
                {
                    Models.ProductionCompany productionCompany = new Models.ProductionCompany();
                    productionCompany.id= (int)prodComp["id"];
                    productionCompany.logo_path = $@"https://image.tmdb.org/t/p/original" + (string)prodComp["logo_path"];
                    productionCompany.name = (string)prodComp["name"];
                    productionCompany.origin_country = (string)prodComp["origin_country"];

                    productionCompanies.Add(productionCompany);
                }
                movie.production_companies = productionCompanies;

                List<Models.ProductionCountry>productionCountries= new List<Models.ProductionCountry>();
                foreach(var prodCoun in data["production_countries"])
                {
                    Models.ProductionCountry productionCountry = new Models.ProductionCountry();
                    productionCountry.iso_3166_1 = (string)prodCoun["iso_3166_1"];
                    productionCountry.name= (string)prodCoun["name"];

                    productionCountries.Add(productionCountry);
                }
                movie.production_countries = productionCountries;

                List<Models.SpokenLanguage>spokenLanguages = new List<Models.SpokenLanguage>();
                foreach(var spokeLang in data["spoken_languages"])
                {
                    Models.SpokenLanguage spokenLanguage = new Models.SpokenLanguage();
                    spokenLanguage.english_name = (string)spokeLang["english_name"];
                    spokenLanguage.iso_639_1 = (string)spokeLang["iso_639_1"];
                    spokenLanguage.name= (string)spokeLang["name"];

                    spokenLanguages.Add(spokenLanguage);
                }
                movie.spoken_languages = spokenLanguages;
            }
            backdrop.Source = movie.backdrop_path;
            genreList.ItemsSource = movie.genres;
            overview.Text = movie.overview;
            rating.Text = movie.vote_average.ToString();
            /*prodCompany.ItemsSource = movie.production_companies;
            prodCountry.ItemsSource = movie.production_countries;*/
            popularity.Text = movie.popularity.ToString();
            realeseDate.Text = movie.release_date;
            
            revenue.Text = movie.revenue.ToString("C", CultureInfo.CurrentCulture);
            country.ItemsSource = movie.production_countries;
            language.Text = movie.original_language;
            duration.Text = movie.runtime.ToString() + " minutes";

            movieTitle.Text = movie.title;
            poster.Source = movie.poster_path;

            buttonTrailer.CommandParameter = movie.id.ToString();
          }

        private void Button_Trailer_Clicked(object sender, EventArgs e)
        {
            Button button1 = (Button)sender;
            string commandParameter = button1.CommandParameter.ToString();
            Navigation.PushAsync(new Trailer(commandParameter));
        }
    }
}