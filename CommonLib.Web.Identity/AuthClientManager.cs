using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Web.Identity
{
    public class AuthClientManager
    {
        public static TAuthClient FindClientById(string id)
        {
            using (ExtendedIdentityDbEntities db = new ExtendedIdentityDbEntities())
            {
                TAuthClient client = db.TAuthClients.FirstOrDefault(p => p.ClientId == id);
                return client;
            }
        }
    }
}
