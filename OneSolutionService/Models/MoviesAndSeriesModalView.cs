using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AllInOneService.Models
{
    public class MoviesAndSeriesModalView
    {
        public List<MovieDetail> Results = new List<MovieDetail>();

        public string ErrorDescription { get; set; }
    }

    public class MovieDetail
    {
        public string Episode_Id { get; set; }
        public string Title { get; set; }
        public string Opening_Crawl { get; set; }
        public string Director { get; set; }
        public string Producer { get; set; }
        public string Created { get; set; }
        public string Url { get; set; }
    }

    public class ActionResponse
    {
        public const string Error = "Error occured. Action not executed successfully!";
        public const string Success = "Action executed successfully!";
    }
}