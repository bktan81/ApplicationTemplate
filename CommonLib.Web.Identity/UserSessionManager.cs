using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib;

namespace CommonLib.Web.Identity
{
    public class UserSessionManager
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static bool CreateSession(string clientId,long userID, string currentSession)
        {
            using (ExtendedIdentityDbEntities db = new ExtendedIdentityDbEntities())
            {
                TUserSession session = db.TUserSessions.FirstOrDefault(p => p.UserId == userID && p.ClientId == clientId);
                if (session != null)
                {
                    //Remove if have existing session
                    db.TUserSessions.Remove(session);
                    db.SaveChanges();
                }

                TUserSession userSession = new TUserSession();
                userSession.ClientId = clientId;
                userSession.UserId = userID;
                userSession.CurrentSession = currentSession;
                userSession.LastAccessDateTime = DateTime.Now;
                userSession.LoginDateTime = DateTime.Now;
                db.TUserSessions.Add(userSession);
                db.SaveChanges();
                return true;
            }

        }
       

        public static TUserSession GetUserSession(string clientId, long userID)
        {
            using (ExtendedIdentityDbEntities db = new ExtendedIdentityDbEntities())
            {
                TUserSession session = db.TUserSessions.FirstOrDefault(p => p.UserId == userID && p.ClientId == clientId);
                return session;
            }

        }

        public static TUserSession GetSessionBySession(string sessionId)
        {
            using (ExtendedIdentityDbEntities db = new ExtendedIdentityDbEntities())
            {
                TUserSession session = db.TUserSessions.FirstOrDefault(p => p.CurrentSession == sessionId);
                return session;
            }

        }

        public static bool UpdateUserCurrentSession(TUserSession objItem)
        {
            bool blnSuccess = true;
            using (ExtendedIdentityDbEntities context = new ExtendedIdentityDbEntities())
            {
                try
                {
                    context.TUserSessions.Attach(objItem); // Entity is in Unchanged state
                    context.Entry(objItem).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    //log.Error(DBHelper.GetEntityValidationExceptionMessage(ex) + " \n" + ex.StackTrace);
                    blnSuccess = false;
                }
                catch (DbUpdateException ex)
                {
                   // log.Error(DBHelper.GetSqlExceptionMessage(ex) + " \n" + ex.StackTrace);
                    blnSuccess = false;
                }
                catch (Exception ex)
                {
                   // log.Error(ex.Message + " " + ex.StackTrace);
                    blnSuccess = false;
                }
            }
            return blnSuccess;
        }
    }
}
