using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Web.Identity
{
    public class IdentityManager
    {
        private ApplicationDbContext db;
        private UserManager<TUser, long> um;

        public IdentityManager()
        {
            db = new ApplicationDbContext();
            um = new UserManager<TUser, long>(
                new UserStore<TUser, TRole, long, TUserLogin, TUserRole, TUserClaim>(db));
        }

        public bool RoleExists(string name)
        {
            var rm = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(db));
            return rm.RoleExists(name);
        }





        public bool CreateRole(string name)
        {
            var rm = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(db));
            var idResult = rm.Create(new IdentityRole(name));
            return idResult.Succeeded;
        }

      


        public bool CreateUser(TUser user, string password)
        {

            var idResult = um.Create(user, password);
            return idResult.Succeeded;
        }


        public bool UserExists(string userName)
        {
            TUser user = um.FindByName(userName);
            return user != null;
        }


        public TUser FindUserByUserName(string userName)
        {
            TUser user = um.FindByName(userName);
            return user;
        }

        public TUser FindUserByUserId(long userId)
        {
            TUser user = um.FindById(userId);
            return user;
        }


        public bool ValidateUser(string userName, string password)
        {
            TUser user = um.FindByName(userName);
            if (user != null)
            {
                PasswordVerificationResult result = um.PasswordHasher.VerifyHashedPassword(user.PasswordHash, password);
                return (result == PasswordVerificationResult.Success);
            }
            return false;
        }


        public bool DeleteUser(long userId)
        {

            var user = um.FindById(userId);
            var idResult = um.Delete<TUser, long>(user);

            return idResult.Succeeded;
        }


        public bool AddUserToRole(long userId, string roleName)
        {

            var idResult = um.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }


        public void ClearUserRoles(long userId)
        {

            var user = um.FindById(userId);
            var currentRoles = new List<TUserRole>();
            currentRoles.AddRange(user.Roles);
            foreach (var role in currentRoles)
            {
                //um.RemoveFromRole<TUser,long>()
                //um.RemoveFromRole(userId, role.RoleId);
            }
        }


    }
}
