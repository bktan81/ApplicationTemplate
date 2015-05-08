using CommonLib.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WorkflowTracking.WebAPI.Controllers
{
    public class AccountController : ApiController
    {
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        [System.Web.Http.HttpGet]
        public object LogOff()
        {
            SessionHelper.ClearAllSessionAndSignOut();
            return Json(new StatusMessage(null, ReturnStatus.Success));
        }
    }
}
