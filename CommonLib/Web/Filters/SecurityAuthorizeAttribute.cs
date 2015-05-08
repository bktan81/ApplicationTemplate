using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Principal;
using System.Web.Routing;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Security.Claims;
using System.Net.Http.Formatting;
using System.Globalization;
using System.Threading;
using System.Net.Http.Headers;
using System.Net.Http;
using CommonLib.Web.Identity;




namespace CommonLib.Web.Filters
{
    public class SecurityAuthorizeAttribute : AuthorizeAttribute
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            log.Debug("HandleUnauthorizedRequest");

            base.HandleUnauthorizedRequest(actionContext);
        }

        public override void OnAuthorization(HttpActionContext filterContext)
        {
           

            var RouteValue = filterContext.RequestContext.RouteData.Values;
            string strController = RouteValue["controller"] == null ? "" : RouteValue["controller"].ToString();
            string strAction = RouteValue["action"] == null ? "" : RouteValue["action"].ToString();

            HttpContext.Current.Items["ControllerActionPrefix"] = strController + " " + strAction;


            base.OnAuthorization(filterContext);

            if (filterContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()) //  || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
            {
                return;
            }


            if (filterContext == null)
            {
                HandleUnauthorizedRequest(filterContext);
                return;
            }

            if (!IsAuthorized(filterContext))
            {
                log.Debug("Not Authorized");
                HandleUnauthorizedRequest(filterContext);
                return;
            }


            ClaimsPrincipal objPrincipal = filterContext.RequestContext.Principal as ClaimsPrincipal;
            string claim_UserId = objPrincipal.Identity.Name;
            string currentSession = objPrincipal.FindFirst("CurrentSession").Value;


            bool HasSession = true;
            bool IsUserSessionValid = true;
            string strErrorCode = "";

            if (currentSession == "")
            {
                HasSession = false;
                strErrorCode = ReturnStatus.SessionLost;
            }


            HttpContext.Current.Items["UserSessionContext"] = null;

            TUserSession objUserSession = null;
            if (HasSession)
            {
                objUserSession = UserSessionManager.GetSessionBySession(currentSession);

                if (objUserSession == null)
                {
                    IsUserSessionValid = false;
                    strErrorCode = ReturnStatus.SessionLost;
                }
                else if (objUserSession.CurrentSession == "UserIsBlockedOrFreezed")
                {
                    IsUserSessionValid = false;
                    strErrorCode = ReturnStatus.UserIsBlockedOrFreezed;
                }
                else if (objUserSession.CurrentSession != currentSession)
                {

                    IsUserSessionValid = false;
                    strErrorCode = ReturnStatus.LoginFromOtherPlace;
                }
                else
                {
                    //No expired for OAuth token
                    //if (objUserSession.LastAccessDateTime.AddMinutes(60) < DateTime.Now) //UserCurrentSession Expired. Temporary hardcode to 60 minutes
                    //{
                    //    IsUserSessionValid = false;
                    //    strErrorCode = AppReturnCode.SessionLost;
                    //}
                }

                if (objUserSession != null)
                    HttpContext.Current.Items["UserSessionContext"] = objUserSession;

                //update last access date time
                if (IsUserSessionValid)
                {
                    objUserSession.LastAccessDateTime = DateTime.Now;
                    bool blnSuccess = UserSessionManager.UpdateUserCurrentSession(objUserSession);
                    if (blnSuccess == false)
                        IsUserSessionValid = false;
                }
            }

            if (HasSession == false || IsUserSessionValid == false)
            {
                HttpResponseMessage objResponse = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
                objResponse.Content = new ObjectContent<StatusMessage>(new StatusMessage(strErrorCode), new JsonMediaTypeFormatter(), "application/json");
                filterContext.Response = objResponse;

                return;
            }


            IdentityManager idManager = new IdentityManager();
            //if everything no problem, store object HttpContext
            long userID = 0;
            if (!string.IsNullOrEmpty(claim_UserId) && long.TryParse(claim_UserId, out userID))
            {
                TUser objUser = idManager.FindUserByUserId(userID);
                if (objUser != null && objUser.Status == 0)
                {
                    HttpContext.Current.Items["UserContext"] = objUser;
                }
                else
                {
                    HttpContext.Current.Items["UserSessionContext"] = null;
                    HttpContext.Current.Items["UserContext"] = null;
                    HandleUnauthorizedRequest(filterContext);
                    return;
                }
               
            }
            else
            {
                HttpContext.Current.Items["UserContext"] = null;
            }


        }




    }
}
