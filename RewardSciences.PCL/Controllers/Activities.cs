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
    public partial class Activities: BaseController
    {
        #region Singleton Pattern

        //private static variables for the singleton pattern
        private static object syncObject = new object();
        private static Activities instance = null;

        /// <summary>
        /// Singleton pattern implementation
        /// </summary>
        internal static Activities Instance
        {
            get
            {
                lock (syncObject)
                {
                    if (null == instance)
                    {
                        instance = new Activities();
                    }
                }
                return instance;
            }
        }

        #endregion Singleton Pattern

        /// <summary>
        /// This endpoint lets you track the activities your users perform.
        /// </summary>
        /// <param name="userId">Required parameter: The ID of the user who is performing the activity.</param>
        /// <param name="activityType">Required parameter: The type of activity the user is performing. Example: 'purchased-a-product'</param>
        /// <param name="price">Optional parameter: The price related to the activity, if any. Expressed in USD</param>
        /// <param name="recordId">Optional parameter: The ID for the record associated with the activity in your database.</param>
        /// <return>Returns the dynamic response from the API call</return>
        public dynamic Track(
                int userId,
                string activityType,
                int? price = null,
                string recordId = null)
        {
            Task<dynamic> t = TrackAsync(userId, activityType, price, recordId);
            Task.WaitAll(t);
            return t.Result;
        }

        /// <summary>
        /// This endpoint lets you track the activities your users perform.
        /// </summary>
        /// <param name="userId">Required parameter: The ID of the user who is performing the activity.</param>
        /// <param name="activityType">Required parameter: The type of activity the user is performing. Example: 'purchased-a-product'</param>
        /// <param name="price">Optional parameter: The price related to the activity, if any. Expressed in USD</param>
        /// <param name="recordId">Optional parameter: The ID for the record associated with the activity in your database.</param>
        /// <return>Returns the dynamic response from the API call</return>
        public async Task<dynamic> TrackAsync(
                int userId,
                string activityType,
                int? price = null,
                string recordId = null)
        {
            //validating required parameters
            if (null == activityType)
                throw new ArgumentNullException("activityType", "The parameter \"activityType\" is a required parameter and cannot be null.");

            //the base uri for api requestss
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/activities");

            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "user_id", userId },
                { "activity_type", activityType },
                { "price", price },
                { "record_id", recordId }
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