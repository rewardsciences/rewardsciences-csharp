/*
 * RewardSciences.PCL
 *
 * This file was automatically generated by APIMATIC v2.0 ( https://apimatic.io ) on 09/27/2016
 */
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RewardSciences.PCL;
using RewardSciences.PCL.Http.Request;
using RewardSciences.PCL.Http.Response;
using RewardSciences.PCL.Http.Client;
using RewardSciences.PCL.Exceptions;

namespace RewardSciences.PCL.Controllers
{
    public partial class RewardCategories: BaseController
    {
        #region Singleton Pattern

        //private static variables for the singleton pattern
        private static object syncObject = new object();
        private static RewardCategories instance = null;

        /// <summary>
        /// Singleton pattern implementation
        /// </summary>
        internal static RewardCategories Instance
        {
            get
            {
                lock (syncObject)
                {
                    if (null == instance)
                    {
                        instance = new RewardCategories();
                    }
                }
                return instance;
            }
        }

        #endregion Singleton Pattern

        /// <summary>
        /// List all the available reward categories.
        /// </summary>
        /// <param name="limit">Optional parameter: The number of reward categories you want to be retrieved.</param>
        /// <param name="offset">Optional parameter: The number of reward categories you want to skip before starting the retrieval.</param>
        /// <return>Returns the dynamic response from the API call</return>
        public dynamic List(int? limit = 25, int? offset = 0)
        {
            Task<dynamic> t = ListAsync(limit, offset);
            Task.WaitAll(t);
            return t.Result;
        }

        /// <summary>
        /// List all the available reward categories.
        /// </summary>
        /// <param name="limit">Optional parameter: The number of reward categories you want to be retrieved.</param>
        /// <param name="offset">Optional parameter: The number of reward categories you want to skip before starting the retrieval.</param>
        /// <return>Returns the dynamic response from the API call</return>
        public async Task<dynamic> ListAsync(int? limit = 25, int? offset = 0)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/reward_categories");

            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "limit", (null != limit) ? limit : 25 },
                { "offset", (null != offset) ? offset : 0 }
            });


            //validate and preprocess url
            string _queryUrl = APIHelper.CleanUrl(_queryBuilder);

            //append request with appropriate headers and parameters
            var _headers = new Dictionary<string,string>()
            {
                { "user-agent", "APIMATIC 2.0" }
            };
            _headers.Add("Authorization", string.Format("Bearer {0}", Configuration.OAuthAccessToken));

            //prepare the API call request to fetch the response
            HttpRequest _request = ClientInstance.Get(_queryUrl,_headers);

            //invoke request and get response
            HttpStringResponse _response = (HttpStringResponse) await ClientInstance.ExecuteAsStringAsync(_request);
            HttpContext _context = new HttpContext(_request,_response);
            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<dynamic>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

    }
} 