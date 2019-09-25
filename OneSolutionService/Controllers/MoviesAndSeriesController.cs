using AllInOneService.Models;
using Newtonsoft.Json;
using OneSolutionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using WebApi.OutputCache.V2;

namespace AllInOneService.Controllers
{
    [CacheOutput(ClientTimeSpan = 3600, ServerTimeSpan = 3600)]
    public class MoviesAndSeriesController : ApiController
    {
        ServiceUserAuthentication serviceAuth = new ServiceUserAuthentication();

        HttpClient client = new HttpClient();

        [System.Web.Http.HttpGet]
        public async Task<HttpResponseMessage> AllMoviesInfo()
        {
            #region Test APIs
            //https://reqres.in/api/users?page=2
            //https://swapi.co/api/films/    - Currently being used
            //https://jsonplaceholder.typicode.com/posts
            #endregion

            /*
            // TODO: REMOVE HARD-CODING !!
            var usernameObj = await Request.Content.ReadAsStringAsync();
            var passwordObj = await Request.Content.ReadAsStringAsync();

            // TODO: Create a class object for de-serializing the below!!
            string username = JsonConvert.DeserializeObject<dynamic>(usernameObj).Username; // <-------------
            string password = JsonConvert.DeserializeObject<dynamic>(usernameObj).Password; // <------------- 
            */
            //Check service authentication first
            var tokenAuth = await serviceAuth.Authenticate
                                                    ("test@gmail.com"
                                                    , "Test@12");

            string clientServiceCallToken = tokenAuth.Access_Token;

            // User was not authenticated!
            if(clientServiceCallToken == "")
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Could not login username: " + tokenAuth.Username +
                                                                            ". Token has not been provided");
            }

            var moviesInfo = new MoviesAndSeriesModalView();

            try
            {
                client.BaseAddress = new Uri(@"https://swapi.co/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/films/");

                var serializedMoviesInfo = await response.Content.ReadAsStringAsync();

                moviesInfo = JsonConvert.DeserializeObject<MoviesAndSeriesModalView>(serializedMoviesInfo);

                Request.CreateResponse(HttpStatusCode.OK, Request.RequestUri);

                moviesInfo.ErrorDescription = ActionResponse.Success;

                return Request.CreateResponse(HttpStatusCode.OK, moviesInfo);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.OK, ex);
            }
        }

        [System.Web.Http.HttpGet]
        public async Task<HttpResponseMessage> SingleMoviesInfo(ulong id)
        {
            /*
            // TODO: REMOVE HARD-CODING !!
            var usernameObj = await Request.Content.ReadAsStringAsync();
            var passwordObj = await Request.Content.ReadAsStringAsync();

            // TODO: Create a class object for de-serializing the below!!
            string username = JsonConvert.DeserializeObject<dynamic>(usernameObj).Username; // <-------------
            string password = JsonConvert.DeserializeObject<dynamic>(usernameObj).Password; // <------------- 
            */
            //Check service authentication first
            var tokenAuth = await serviceAuth.Authenticate
                                                    ("test@gmail.com"
                                                    , "Test@12");


            string clientServiceCallToken = tokenAuth.Access_Token;

            // User was not authenticated!
            if (clientServiceCallToken == "")
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Could not login username: " + tokenAuth.Username +
                                                                            ". Token has not been provided");
            }

            var singleMoviesInfo = new MovieDetail();

            try
            {
                client.BaseAddress = new Uri(@"https://swapi.co/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($@"api/films/{id}");

                var serializedMoviesInfo = await response.Content.ReadAsStringAsync();

                singleMoviesInfo = JsonConvert.DeserializeObject<MovieDetail>(serializedMoviesInfo);

                return Request.CreateResponse(HttpStatusCode.OK, singleMoviesInfo);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
