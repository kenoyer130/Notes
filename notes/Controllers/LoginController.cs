using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.Messaging;
using NotesInterfaces.Repository;
using NotesModel;

namespace notes.Controllers
{
    public class LoginController : Controller
    {
        private IAccountRepository accountRepository;
        public LoginController()
        {
            accountRepository = new AccountGoogleRepository();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOn()
        {
            var openid = new OpenIdRelyingParty();
            IAuthenticationResponse response = openid.GetResponse();

            if (response != null)
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        Session["accountID"] = accountRepository.Login(response.ClaimedIdentifier);                        
                        FormsAuthentication.SetAuthCookie(Session["accountID"].ToString(), true);
                        return new RedirectResult("/", false);
                    case AuthenticationStatus.Canceled:
                        ModelState.AddModelError("loginIdentifier",
                            "Login was cancelled at the provider");
                        break;
                    case AuthenticationStatus.Failed:
                        ModelState.AddModelError("loginIdentifier",
                            "Login failed using the provided OpenID identifier");
                        break;
                }
            }

            return View();
        }

        [System.Web.Mvc.AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LogOn(string loginIdentifier)
        {
            if (!Identifier.IsValid(loginIdentifier))
            {
                ModelState.AddModelError("loginIdentifier",
                            "The specified login identifier is invalid");
                return View();
            }
            else
            {
                var openid = new OpenIdRelyingParty();
                IAuthenticationRequest request = openid.CreateRequest(Identifier.Parse(loginIdentifier));
                return request.RedirectingResponse.AsActionResult();
            }
        }
    }
}
