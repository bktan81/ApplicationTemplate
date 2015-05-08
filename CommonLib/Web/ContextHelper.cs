using CommonLib.Web.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CommonLib.Web
{
    public class ContextHelper
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);



        public static bool IsAuthenticated()
        {
            return UserContext != null;
        }

        public static TUser UserContext
        {
            //Call this after init in AuthorizeAttribute Filter
            get
            {
                TUser objUser = (TUser)HttpContext.Current.Items["UserContext"];
                return objUser;
            }
        }

        public static TUserSession UserSessionContext
        {
            //Call this after init in AuthorizeAttribute Filter
            get
            {
                TUserSession objUser = (TUserSession)HttpContext.Current.Items["UserSessionContext"];
                return objUser;
            }

        }

        public static long UserId
        {
            //Call this after init in AuthorizeAttribute Filter
            get
            {
                if (UserContext != null)
                    return UserContext.Id;

                return -1;
            }
        }

       


        public static string CurrentSession
        {
            //Call this after init in AuthorizeAttribute Filter
            get
            {
                if (UserSessionContext != null)
                    return UserSessionContext.CurrentSession;

                return "";
            }
        }



    }
}
