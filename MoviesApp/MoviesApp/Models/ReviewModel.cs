using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApp.Models
{
    public class AuthorDetails
    {
        public string name { get; set; }
        public string username { get; set; }
        public string avatar_path { get; set; }
        public double? rating { get; set; }
    }

    public class ReviewModel
    {
        public string author { get; set; }
        public AuthorDetails author_details { get; set; }
        public string content { get; set; }
        public DateTime created_at { get; set; }
        public string id { get; set; }
        public DateTime updated_at { get; set; }
        public string url { get; set; }
    }
}
