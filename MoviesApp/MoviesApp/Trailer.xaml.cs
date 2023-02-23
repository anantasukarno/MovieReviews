using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class Trailer : ContentPage
    {
        TrailerModel dataTrailer = new TrailerModel();
        List<ReviewModel> reviews = new List<ReviewModel>();
        public Trailer(string idMovie)
        {
            InitializeComponent();
            GetTrailer(idMovie);
            GetReview(idMovie);
        }

        public async Task GetTrailer(string idMovie)
        {
            var uri = new Uri($@"https://api.themoviedb.org/3/movie/{idMovie}/videos");
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI4ZDVjMjhiNWVhOGYyNWM2N2U3YWRjMjY4ZmQ0Y2EyYyIsInN1YiI6IjYzZjQ4NTI1Y2FhY2EyMDA4NTc5MTdmOCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.vs5K7jHgdjD0qn_-AN3172rcwbL21VFpxs_fmwHbtLQ");
            var response = await httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                string json = content.ToString();
                var jsonObject = JObject.Parse(json);
                var data = jsonObject["results"];
                var jsonArray = JArray.Parse(data.ToString());
                foreach (var token in jsonArray)
                {
                    int count = 0;
                    if ((string)token["type"] == "Trailer")
                    {
                        TrailerModel g = new TrailerModel();
                        string id = (string)token["id"];
                        string name = token["name"].ToString();
                        string key = (string)token["key"];

                        g.id = id;
                        g.name = name;
                        g.key = key;
                        dataTrailer = g;

                        Debug.WriteLine(g.name);

                        count++;
                        
                    }
                    if (count == 1)
                    {
                        break;
                    }

                }


            }
            
            trailerVideo.Source = "https://www.youtube.com/embed/" + dataTrailer.key;
            trailerTitle.Text = dataTrailer.name;

        }
        public async Task GetReview(string idMovie)
        {
            var uriReviews = new Uri($@"https://api.themoviedb.org/3/movie/{idMovie}/reviews");
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI4ZDVjMjhiNWVhOGYyNWM2N2U3YWRjMjY4ZmQ0Y2EyYyIsInN1YiI6IjYzZjQ4NTI1Y2FhY2EyMDA4NTc5MTdmOCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.vs5K7jHgdjD0qn_-AN3172rcwbL21VFpxs_fmwHbtLQ");
            var responseReviews = await httpClient.GetAsync(uriReviews);

            if (responseReviews.IsSuccessStatusCode)
            {
                var contentReviews = await responseReviews.Content.ReadAsStringAsync();
                string json = contentReviews.ToString();
                var jsonObjectreviews = JObject.Parse(json);
                var dataReviews = jsonObjectreviews["results"];
                var jsonArrayReviews = JArray.Parse(dataReviews.ToString());
                foreach (var tokenR in jsonArrayReviews)
                {
                    ReviewModel review = new ReviewModel();
                    AuthorDetails authorDetails = new AuthorDetails();

                    review.id = (string)tokenR["id"];

                    string author_d = tokenR["author_details"].ToString();
                    var author_d_Obj = JObject.Parse(author_d);
                    Debug.WriteLine(author_d_Obj["name"]);

                    authorDetails.name = (string)author_d_Obj["name"];
                    authorDetails.username = (string)author_d_Obj["username"];
                    authorDetails.avatar_path = @"https://image.tmdb.org/t/p/original" + (string)author_d_Obj["avatar_path"];
                    /*authorDetails.rating = (double)author_d_Obj?["rating"];*/

                    review.author_details = authorDetails;
                    review.author = (string)tokenR["author"];
                    review.content = (string)tokenR["content"];
                    review.created_at = (DateTime)tokenR["created_at"];
                    review.updated_at = (DateTime)tokenR["updated_at"];

                    reviews.Add(review);
                }


            }
            reviewsList.ItemsSource = reviews;


        }
    }
}