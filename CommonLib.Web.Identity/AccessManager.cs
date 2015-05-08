using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Web.Identity
{
    public class AccessManager
    {
        public static bool CreateAccess(string accessName)
        {
            using (ExtendedIdentityDbEntities db = new ExtendedIdentityDbEntities())
            {
                TAccessRight accessRight = db.TAccessRights.FirstOrDefault(p => p.Name == accessName);
                if (accessRight == null)
                {
                    accessRight = new TAccessRight();
                    accessRight.Name = accessName;
                    accessRight.AccessId = Guid.NewGuid();
                    accessRight.IsActive = true;
                    db.TAccessRights.Add(accessRight);
                    return db.SaveChanges()>0;
                    
                }

                return false;
            }
        }

        public static bool AssignRoleAccess(long roleId, string accessName)
        {
            using (ExtendedIdentityDbEntities db = new ExtendedIdentityDbEntities())
            {
                TAccessRight accessRight = db.TAccessRights.FirstOrDefault(p => p.Name == accessName);
                if (accessRight != null && db.TRoleAccesses.Count(p=>p.AccessId==accessRight.AccessId && p.RoleId==roleId)==0)
                {
                    TRoleAccess roleAccess = new TRoleAccess();
                    roleAccess.AccessId = accessRight.AccessId;
                    roleAccess.RoleId = roleId;
                    roleAccess.IsActive = true;
                    db.TRoleAccesses.Add(roleAccess);
                    return db.SaveChanges() > 0;
                }
                return false;
            }
        }


        public static bool HasRoleAccess(long roleId, string accessName)
        {
            using (ExtendedIdentityDbEntities db = new ExtendedIdentityDbEntities())
            {
                TAccessRight accessRight = db.TAccessRights.FirstOrDefault(p => p.Name == accessName);
                if (accessRight != null)
                {
                    TRoleAccess roleAccess = db.TRoleAccesses.FirstOrDefault(p => p.AccessId == accessRight.AccessId && p.RoleId == roleId && p.IsActive);
                    return roleAccess!=null;
                }

                return false;
            }
        }


        public static bool HasUserAccess(long userId, string accessName)
        {
            using (ExtendedIdentityDbEntities db = new ExtendedIdentityDbEntities())
            {
                IdentityManager idManager = new IdentityManager();
                TUser user=idManager.FindUserByUserId(userId);
                if (user != null)
                {
                    if (user.Roles != null && user.Roles.Count > 0)
                    {
                        TAccessRight accessRight = db.TAccessRights.FirstOrDefault(p => p.Name == accessName);
                        if (accessRight != null)
                        {
                            long[] uroles=user.Roles.Select(z => z.RoleId).ToArray();
                            int count = db.TRoleAccesses.Count(p => p.AccessId == accessRight.AccessId && uroles.Contains(p.RoleId) && p.IsActive);
                            return count > 0;
                        }
                    }
                }

                return false;
            }
        }
    }
}
