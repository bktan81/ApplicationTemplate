using CommonLib.Web.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowTracking.Core;

namespace TestCase
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestAccess();
            TestMobile();
            Console.ReadLine();
        }

        private static async void TestMobile()
        {

            SimpleOAuthClient oAuthClient = new SimpleOAuthClient("http://localhost:8086/api/token","admin", "admin123");
           Token token= await oAuthClient.GetToken();
           if (token != null)
           {
               OutputResult("Token", token.access_token);
               Console.WriteLine("--------------------------------");
               oAuthClient.AccessToken = token.access_token;
               string productContent=await oAuthClient.PostAsync("http://localhost:8086/api/products/getproducts");
               
               OutputResult("Content", productContent);
           }
        }

        private static void TestAccess()
        {
            long roleId = 1;
            long userId = 1;

            CreateAccessAndAssignAccessToRole(WorkflowTrackingAccessNames.CreateProduct, roleId, userId);
            CreateAccessAndAssignAccessToRole(WorkflowTrackingAccessNames.FindProduct, roleId, userId);
            CreateAccessAndAssignAccessToRole(WorkflowTrackingAccessNames.GetProduct, roleId, userId);
            CreateAccessAndAssignAccessToRole(WorkflowTrackingAccessNames.GetProducts, roleId, userId);
        }

        private static void CreateAccessAndAssignAccessToRole(string accessName, long roleId, long userId)
        {
            Console.WriteLine("----------" + accessName + "-----------");
            OutputResult("CreateAccess for " + accessName, AccessManager.CreateAccess(accessName));
            OutputResult("AssignRoleAccess for " + roleId.ToString(), AccessManager.AssignRoleAccess(roleId, accessName));
            OutputResult("HasRoleAccess for role " + roleId.ToString(), AccessManager.HasRoleAccess(roleId, accessName));
            OutputResult("HasUserAccess for user " + userId.ToString(), AccessManager.HasUserAccess(userId, accessName));
            Console.WriteLine("----------------------------------");
        }

        private static void CreateAdmin()
        {
            IdentityManager im = new IdentityManager();

            TUser userNew = new TUser();
            userNew.FirstName = "Administrator";
            userNew.LastName = "Administrator";
            userNew.Status = 0;
            userNew.Email = "admin@admin.com";
            userNew.EmailConfirmed = false;
            userNew.PhoneNumberConfirmed = false;
            userNew.TwoFactorEnabled = false;
            userNew.LockoutEnabled = false;
            userNew.AccessFailedCount = 0;
            userNew.UserName = "admin";

            Console.WriteLine(im.CreateUser(userNew, "admin123"));
        }

        private static void OutputResult(string name, object output)
        {
            string text = (output == null ? "" : output.ToString());
            Console.WriteLine(name + ":" + text);
        }
    }
}
