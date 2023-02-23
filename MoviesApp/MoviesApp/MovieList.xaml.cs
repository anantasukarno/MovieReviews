using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MoviesApp.Models;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using static System.Net.WebRequestMethods;

namespace MoviesApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MovieList : ContentPage
	{
        public ObservableCollection<MovieModel> movieList = new ObservableCollection<MovieModel>();
        public int totalPages;
        public int page = 1;
        public string currentId;
        public MovieList (string id)
		{
			InitializeComponent ();
			GetMovieList(id);
            currentId = id;

            movieListView.ItemsSource = movieList;
            movieListView.RemainingItemsThresholdReached += MovieListView_RemainingItemsThresholdReached;
            movieListView.RemainingItemsThresholdReachedCommandParameter = page;
        }

        private void MovieListView_RemainingItemsThresholdReached(object sender, EventArgs e)
        {
            Xamarin.Forms.CollectionView coll = (Xamarin.Forms.CollectionView)sender;
            string commandParameter = coll.RemainingItemsThresholdReachedCommandParameter.ToString();

            GetMoreMovieList(currentId, commandParameter);

        }

        public async Task GetMovieList(string idGenre)
        {
            var uri = new Uri($@"https://api.themoviedb.org/3/discover/movie?with_genres={idGenre}");
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI4ZDVjMjhiNWVhOGYyNWM2N2U3YWRjMjY4ZmQ0Y2EyYyIsInN1YiI6IjYzZjQ4NTI1Y2FhY2EyMDA4NTc5MTdmOCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.vs5K7jHgdjD0qn_-AN3172rcwbL21VFpxs_fmwHbtLQ");
            var response = await httpClient.GetAsync(uri);
            Debug.WriteLine(response);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                string json = content.ToString();
                var jsonObject = JObject.Parse(json);
                var data = jsonObject["results"];
                movieListView.RemainingItemsThreshold = (int)jsonObject["total_pages"] - (int)jsonObject["page"];
                var jsonArray = JArray.Parse(data.ToString());
                Debug.WriteLine(jsonArray);
                foreach (var token in jsonArray)
                {
                    MovieModel g = new MovieModel();
                    string id = (string)token["id"];
                    bool adult = (bool)token["adult"];
                    string backdrop_path = (string)token["backdrop_path"];
                    string original_language = (string)token["original_language"];
                    string original_title = (string)token["original_title"];
                    string overview = (string)token["overview"];
                    double popularity = (double)token["popularity"];
                    string poster_path = "https://image.tmdb.org/t/p/original" + (string)token["poster_path"];
                    string release_date = (string)token["release_date"];
                    string title = (string)token["title"];
                    bool video = (bool)token["video"];
                    double vote_average = (double)token["vote_average"];
                    int vote_count = (int)token["vote_count"];

                    g.id = int.Parse(id);
                    g.adult = adult;
                    g.backdrop_path = backdrop_path;
                    g.overview = overview;
                    g.original_language = original_language;
                    g.original_title = original_title;
                    g.popularity= popularity;
                    g.poster_path = poster_path;    
                    g.release_date = release_date;
                    g.title = title;
                    g.video = video;
                    g.vote_average = vote_average;
                    g.vote_count = vote_count;

                    movieList.Add(g);
                }

            }
            page += 1;
        }

        public async Task GetMoreMovieList(string idGenre, string currentPage)
        {
            var uri = new Uri($@"https://api.themoviedb.org/3/discover/movie?with_genres={idGenre}&page={page}");
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI4ZDVjMjhiNWVhOGYyNWM2N2U3YWRjMjY4ZmQ0Y2EyYyIsInN1YiI6IjYzZjQ4NTI1Y2FhY2EyMDA4NTc5MTdmOCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.vs5K7jHgdjD0qn_-AN3172rcwbL21VFpxs_fmwHbtLQ");
            var response = await httpClient.GetAsync(uri);
            Debug.WriteLine(response);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                string json = content.ToString();
                var jsonObject = JObject.Parse(json);
                var data = jsonObject["results"];
                movieListView.RemainingItemsThreshold = (int)jsonObject["total_pages"] - (int)jsonObject["page"];
                var jsonArray = JArray.Parse(data.ToString());
                Debug.WriteLine(jsonArray);
                foreach (var token in jsonArray)
                {
                    MovieModel g = new MovieModel();
                    string id = (string)token["id"];
                    bool adult = (bool)token["adult"];
                    string backdrop_path = (string)token["backdrop_path"];
                    string original_language = (string)token["original_language"];
                    string original_title = (string)token["original_title"];
                    string overview = (string)token["overview"];
                    double popularity = (double)token["popularity"];
                    string poster_path = "https://image.tmdb.org/t/p/original" + (string)token["poster_path"];
                    string release_date = (string)token["release_date"];
                    string title = (string)token["title"];
                    bool video = (bool)token["video"];
                    double vote_average = (double)token["vote_average"];
                    int vote_count = (int)token["vote_count"];

                    g.id = int.Parse(id);
                    g.adult = adult;
                    g.backdrop_path = backdrop_path;
                    g.overview = overview;
                    g.original_language = original_language;
                    g.original_title = original_title;
                    g.popularity = popularity;
                    g.poster_path = poster_path;
                    g.release_date = release_date;
                    g.title = title;
                    g.video = video;
                    g.vote_average = vote_average;
                    g.vote_count = vote_count;

                    movieList.Add(g);
                }

            }
            page += 1;

        }

        private void Button_Preview_Clicked(object sender, EventArgs e)
        {
            Xamarin.Forms.Button button1 = (Xamarin.Forms.Button)sender;
            string commandParameter = button1.CommandParameter.ToString();
            Navigation.PushAsync(new MovieDetail(commandParameter));
        }
    }
}