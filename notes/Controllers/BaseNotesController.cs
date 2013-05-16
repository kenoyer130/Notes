using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace notes.Controllers
{
    public abstract class BaseNotesController : ApiController
    {
        
        public static int getAccountID()
        {           
            return Convert.ToInt32(HttpContext.Current.User.Identity.Name);
        }

        protected HttpResponseMessage execute(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
