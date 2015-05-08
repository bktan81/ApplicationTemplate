using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonLib.Web
{
    public class SessionHelper
    {

        public static bool IsAuthenticated()
        {
            if (System.Web.HttpContext.Current == null || System.Web.HttpContext.Current.Session==null) return false;
            else return System.Web.HttpContext.Current.Request.IsAuthenticated & System.Web.HttpContext.Current.Session["AccessToken"] != null;
        }


        public static string AccessToken
        {
            get
            {
 
                if ((System.Web.HttpContext.Current.Session["AccessToken"] != null))
                {
                    return Convert.ToString(System.Web.HttpContext.Current.Session["AccessToken"]);
                }
                return "";
            }
            set { System.Web.HttpContext.Current.Session["AccessToken"] = value; }
        }


        public static DateTime LoginDateTime
        {
            get
            {
                if ((System.Web.HttpContext.Current.Session["LoginDateTime"] != null))
                {
                    return Convert.ToDateTime(System.Web.HttpContext.Current.Session["LoginDateTime"]);
                }
                return DateTime.Now;
            }
            set { System.Web.HttpContext.Current.Session["LoginDateTime"] = value; }
        }

        public static long UserId
        {
            get
            {
                if ((System.Web.HttpContext.Current.Session["UserId"] != null))
                {
                    return Convert.ToInt64(System.Web.HttpContext.Current.Session["UserId"]);
                }
                return -1;
            }
            set { System.Web.HttpContext.Current.Session["UserId"] = value; }
        }

        public static string UserName
        {
            get
            {
                if ((System.Web.HttpContext.Current.Session["UserName"] != null))
                {
                    return Convert.ToString(System.Web.HttpContext.Current.Session["UserName"]);
                }
                return "";
            }
            set { System.Web.HttpContext.Current.Session["UserName"] = value; }
        }


        public static string UserCurrentSession
        {
            get
            {
                if ((System.Web.HttpContext.Current.Session["UserCurrentSession"] != null))
                {
                    return Convert.ToString(System.Web.HttpContext.Current.Session["UserCurrentSession"]);
                }
                return "";
            }
            set { System.Web.HttpContext.Current.Session["UserCurrentSession"] = value; }
        }


        public static int UserType
        {
            get
            {
                if ((System.Web.HttpContext.Current.Session["UserType"] != null))
                {
                    return Convert.ToInt32(System.Web.HttpContext.Current.Session["UserType"]);
                }
                return -1;
            }
            set { System.Web.HttpContext.Current.Session["UserType"] = value; }
        }

     

        public static void ClearAllSessionAndSignOut()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            if (System.Web.HttpContext.Current.Session != null)
            {
                System.Web.HttpContext.Current.Session.Abandon();
            }

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(System.Web.Security.FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie1);

            // clear session cookie
            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie2);

            // Invalidate the Cache on the Client Side
            System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            System.Web.HttpContext.Current.Response.Cache.SetNoStore();

        }

    }
}