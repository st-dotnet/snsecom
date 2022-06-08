using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SNS.ECommerce.Data;
using SNS.ECommerce.Infrastructure.Interfaces;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Linq;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;

namespace SNS.ECommerce.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationContext _dbContext;
        private readonly IConfiguration _configuration;

        protected static HttpContext HttpContext => ServiceLocator.Resolve<IHttpContextAccessor>().HttpContext;

        public AccountService(ApplicationContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<string> LogIn(string email, string password)
        {
            string response = "";
            string domainName = _configuration.GetSection("MyConfiguration")["DomainName"];
            string LDAP_PATH = $"LDAP://{domainName}/{DomainPath}";

            var groups = new StringCollection();
            try
            {
                using (var context = new PrincipalContext(ContextType.Domain, domainName, email, password))
                {
                    if (context.ValidateCredentials(email, password))
                    {
                        var userName = email.Split("@")[0];
                        using (var de = new DirectoryEntry(LDAP_PATH))
                        using (var ds = new DirectorySearcher(de, "(sAMAccountName=" + userName + ")"))
                        {
                            var res = ds.FindOne();
                            if (null != res)
                            {
                                var obUser = new DirectoryEntry(res.Path);
                                // Invoke Groups method.
                                object obGroups = obUser.Invoke("Groups");
                                foreach (object ob in (IEnumerable)obGroups)
                                {
                                    // Create object for each group.
                                    var obGpEntry = new DirectoryEntry(ob);
                                    groups.Add(obGpEntry.Name);
                                }
                            }
                        }
                    }
                    else
                    {
                        //Failure = "Invalid Credentials.";
                        //return View(this);
                    }
                }

                var allowedGroups = _configuration.GetSection("MyConfiguration")["AllowedGroups"].Split(";").ToList();

                var validUser = false;

                foreach (var allowedGroup in allowedGroups)
                {
                    validUser = groups.Contains(allowedGroup);
                    if (validUser) break;
                }

                if (validUser)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, email),
                        new Claim("Role", "Administrator")
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        // ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    //return Redirect(ReturnUrl ?? "/salesperson");
                    response = "true";
                    return response;
                }
                else
                {
                    //    Failure = "User does not belong to IT and VPN Developers group.";
                    //    return View(this);
                    response = "false";
                    return response;
                }
            }
            catch (Exception ex)
            {
                //Failure = ex.ToString();
                //return View(this);
                response = "false";
                return response;
            }
        }

        public static string DomainPath
        {
            get
            {
                bool bFirst = true;
                string domainName = "SITNSLEEP.COM";
                StringBuilder sbReturn = new StringBuilder(200);
                string[] strlstDc = domainName.Split('.');
                foreach (string strDc in strlstDc)
                {
                    if (bFirst)
                    {
                        sbReturn.Append("DC=");
                        bFirst = false;
                    }
                    else
                        sbReturn.Append(",DC=");

                    sbReturn.Append(strDc);
                }
                return sbReturn.ToString();
            }
        }
    }
}

