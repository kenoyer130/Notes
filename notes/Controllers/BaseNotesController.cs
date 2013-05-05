using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace notes.Controllers
{
    public abstract class BaseNotesController : ApiController
    {
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
