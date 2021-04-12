using ShopBridgeApi.JWT;
using ShopBridgeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ShopBridgeApi.Controllers
{
    public class AuthenticationController : ApiController
    {
        // POST api/authenticate
        [Route("api/authenticate")]
        public IHttpActionResult Post([FromBody]User user)
        {
            try
            {
                if(user != null && user.UserName == "admin" && user.Password =="admin") {
                    var token = TokenManager.GenerateToken(user.UserName);                  
                    return Ok<string>(token);
                }
                else {                 
                    return Content(HttpStatusCode.Unauthorized, "Unauthorized Access");
                }                
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }

        // POST api/validatetoken
        [Route("api/validatetoken")]
        public IHttpActionResult Post([FromBody]AuthRequest authRequest)
        {
            try
            {
                var claimUsername = TokenManager.ValidateToken(authRequest.Token);
                if(claimUsername.ToLower().Equals(authRequest.UserName.ToLower())) {
                    return Ok();
                }
                else {
                    return Content(HttpStatusCode.BadRequest, "Unable to validate token");
                }               
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }
    }    
}
