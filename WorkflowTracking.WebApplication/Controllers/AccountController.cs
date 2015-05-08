using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CommonLib.Web;
using CommonLib.Web.Identity;

namespace WorkflowTracking.WebApplication.Controllers
{
    public class AccountController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Auth(string userName, string token, string session)
        {
            IdentityManager identityManager = new IdentityManager();
            TUser user = identityManager.FindUserByUserName(userName);
            if (user != null)
            {
                TUserSession userSession = UserSessionManager.GetUserSession(AppConfig.ClientId, user.Id);
                //User have to login within 40 seconds
                if (userSession != null && DateTime.Now < userSession.LoginDateTime.AddSeconds(40))
                {

                    FormsAuthentication.Initialize();
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                                                            1,
                                                            userName,
                                                            DateTime.Now,
                                                            DateTime.Now.Add(FormsAuthentication.Timeout),
                                                            false,
                                                            userName,
                                                            FormsAuthentication.FormsCookiePath
                                                            );
                    string encTicket = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    Response.Cookies.Add(cookie);
     
                    
                    SessionHelper.AccessToken = token;
                    SessionHelper.UserId = user.Id;
                    SessionHelper.UserName = userName;
                    SessionHelper.LoginDateTime = userSession.LoginDateTime;
                    SessionHelper.UserCurrentSession = userSession.CurrentSession;
                    bool isAuthenticated = System.Web.HttpContext.Current.Request.IsAuthenticated;

                    return (Json(new { status = ReturnStatus.Success }, JsonRequestBehavior.DenyGet));
                }
            }

            return (Json(new { status = ReturnStatus.Failed }, JsonRequestBehavior.DenyGet));


        }
    }
}