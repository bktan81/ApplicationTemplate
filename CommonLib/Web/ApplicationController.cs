using CommonLib.Web.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace CommonLib.Web
{
    public class ApplicationController: ApiController
    {
        

        public bool HasAccess(string accessName)
        {
            if (ContextHelper.IsAuthenticated() && AccessManager.HasUserAccess(ContextHelper.UserId, accessName))
            {
                return true;
            }
            return false;
        }

        public StatusMessage CheckAccess(string accessName, Func<StatusMessage> success, Func<StatusMessage> error =null)
        {
            if (HasAccess(accessName))
            {
                if (success == null) throw new Exception("Has to specify success operation");
                return success();
            }
            else
            {
                if (error == null)
                {
                    return new StatusMessage(ReturnStatus.NoPermission);
                }
                else
                {
                    return error();
                }
            }
        }

        public JsonResult<StatusMessage> Json(StatusMessage statusMessage)
        {
            return Json<StatusMessage>(statusMessage);
        }
    }
}
