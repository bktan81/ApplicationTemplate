using CommonLib.Web.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Web.OAuthServerProvider
{
    public class ApplicationOAuthServerProvider
        : OAuthAuthorizationServerProvider
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            log.Debug("ValidateClientAuthentication " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string clientId = string.Empty;
            string clientSecret = string.Empty;
            TAuthClient client = null;


            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }


            if (context.ClientId == null)
            {
                //Remove the comments from the below line context.SetError, and invalidate context 
                //if you want to force sending clientId/secrects once obtain access tokens. 
                context.Validated();
                //context.SetError("invalid_clientId", "ClientId should be sent.");
                return Task.FromResult<object>(null);
            }


            client = AuthClientManager.FindClientById(context.ClientId);


            if (client == null)
            {
                log.Debug(string.Format("Client '{0}' is not registered in the system.", context.ClientId));
                context.SetError(ReturnStatus.InvalidClient);
                return Task.FromResult<object>(null);
            }

            if (client.ApplicationType == ApplicationTypes.NativeConfidential)
            {
                if (string.IsNullOrWhiteSpace(clientSecret))
                {
                    log.Debug("Client secret should be sent.");
                    context.SetError(ReturnStatus.InvalidClient);
                    return Task.FromResult<object>(null);
                }
                else
                {
                    if (client.Secret != CommonHelper.GetHash(clientSecret))
                    {
                        log.Debug("Client secret is invalid.");
                        context.SetError(ReturnStatus.InvalidClient);
                        return Task.FromResult<object>(null);
                    }
                }
            }

            if (!client.Active)
            {
                log.Debug("Client is inactive.");
                context.SetError(ReturnStatus.InvalidClient);
                return Task.FromResult<object>(null);
            }

            context.OwinContext.Set<string>("as:clientAllowedOrigin", client.AllowedOrigin);
            //log.Debug("client.AllowedOrigin=" + client.AllowedOrigin);
            context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

            context.Validated();
            return Task.FromResult<object>(null);


        }



        public override async Task GrantResourceOwnerCredentials(
            OAuthGrantResourceOwnerCredentialsContext context)
        {
            // Retrieve user from database:
            //var store = new UserStore(new ApplicationDbContext());
            //var user = await store.FindByEmailAsync(context.UserName);
            IdentityManager idManager = new IdentityManager();
            TUser user = idManager.FindUserByUserName(context.UserName);
            // Validate user/password:
            if (user != null && idManager.ValidateUser(context.UserName, context.Password) && user.Status==0)
            {
                /*
                // Add claims associated with this user to the ClaimsIdentity object:
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                foreach (var userClaim in user.Claims)
                {
                    identity.AddClaim(new Claim(userClaim.ClaimType, userClaim.ClaimValue));
                }

                context.Validated(identity);
                 * */
                string strClientId = SiteConfig.ClientId;
                var data = await context.Request.ReadFormAsync();

                if (!string.IsNullOrEmpty(context.ClientId)) {
                    strClientId = context.ClientId;
                }
                else if (data != null && !string.IsNullOrEmpty(data["client_id"]))
                {
                    strClientId = data["client_id"];
                }

                
                string CurrentSession = Guid.NewGuid().ToString("n") + DateTime.Now.ToString("yyyyMMddHHmm");
                UserSessionManager.CreateSession(strClientId, user.Id, CurrentSession);

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Id.ToString()));
                identity.AddClaim(new Claim("CurrentSession", CurrentSession));

                var props = new AuthenticationProperties(new Dictionary<string, string>
                    {
                        { 
                            "client_id", strClientId
                        }
                        ,{ 
                            "token2", CurrentSession
                        }
                        ,{ 
                            "status", ""
                        }
                    });
                var ticket = new AuthenticationTicket(identity, props);
                context.Validated(ticket);

            }
            else
            {
                context.SetError(
                   "invalid_grant", "The user name or password is incorrect.");
                context.Rejected();
                return;
            }


        }

      
    }
}
