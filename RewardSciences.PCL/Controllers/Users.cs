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
    public partial class Users: BaseController
    {
        #region Singleton Pattern

        //private static variables for the singleton pattern
        private static object syncObject = new object();
        private static Users instance = null;

        /// <summary>
        /// Singleton pattern implementation
        /// </summary>
        internal static Users Instance
        {
            get
            {
                lock (syncObject)
                {
                    if (null == instance)
                    {
                        instance = new Users();
                    }
                }
                return instance;
            }
        }

        #endregion Singleton Pattern

        /// <summary>
        /// This endpoint lets retrieve a user's details.
        /// </summary>
        /// <param name="userId">Required parameter: The ID of the user to be retrieved.</param>
        /// <return>Returns the dynamic response from the API call</return>
        public dynamic Show(int userId)
        {
            Task<dynamic> t = ShowAsync(userId);
            Task.WaitAll(t);
            return t.Result;
        }

        /// <summary>
        /// This endpoint lets retrieve a user's details.
        /// </summary>
        /// <param name="userId">Required parameter: The ID of the user to be retrieved.</param>
        /// <return>Returns the dynamic response from the API call</return>
        public async Task<dynamic> ShowAsync(int userId)
        {
            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/users/{user_id}");

            //process optional template parameters
            APIHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "user_id", userId }
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

        /// <summary>
        /// This endpoint lets you tie a user with his/her activities. You’ll want to identify a user with any relevant information as soon as they log-in or sign-up.
        /// </summary>
        /// <param name="email">Required parameter: The user's email address</param>
        /// <param name="firstName">Optional parameter: The user's first name</param>
        /// <param name="lastName">Optional parameter: The user's last name</param>
        /// <return>Returns the dynamic response from the API call</return>
        public dynamic Identify(string email, string firstName = null, string lastName = null)
        {
            Task<dynamic> t = IdentifyAsync(email, firstName, lastName);
            Task.WaitAll(t);
            return t.Result;
        }

        /// <summary>
        /// This endpoint lets you tie a user with his/her activities. You’ll want to identify a user with any relevant information as soon as they log-in or sign-up.
        /// </summary>
        /// <param name="email">Required parameter: The user's email address</param>
        /// <param name="firstName">Optional parameter: The user's first name</param>
        /// <param name="lastName">Optional parameter: The user's last name</param>
        /// <return>Returns the dynamic response from the API call</return>
        public async Task<dynamic> IdentifyAsync(string email, string firstName = null, string lastName = null)
        {
            //validating required parameters
            if (null == email)
                throw new ArgumentNullException("email", "The parameter \"email\" is a required parameter and cannot be null.");

            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/users");

            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "email", email },
                { "first_name", firstName },
                { "last_name", lastName }
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
            HttpRequest _request = ClientInstance.Post(_queryUrl, _headers, null);

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