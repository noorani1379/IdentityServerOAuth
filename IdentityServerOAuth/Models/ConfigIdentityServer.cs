using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace IdentityServerOAuth.Models
{
    //this class is for seed data as DataBase
    public static class ConfigIdentityServer
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>()
            {
                  new IdentityServer4.Test.TestUser
                    {
                         SubjectId="1",
                         Username="eb.babaei",
                         IsActive=true,
                         Password="123456",
                         Claims=new List<Claim>()
                         {
                              new Claim(ClaimTypes.Email,"eb.babaei@gmail.com"),
                              new Claim(ClaimTypes.MobilePhone,"09128698172"),
                              new Claim("name","ehsan babaei"),
                              new Claim("website","https://bugeto.net"),
                         }
            }
            };
        }
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {        new IdentityResources.OpenId(),
                     new IdentityResources.Email(),
                     new IdentityResources.Profile(),
                     new IdentityResources.Address(),
                     new IdentityResources.Phone(),
            };
        }
        public static List<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("ApiHava","سرویس هواشناسی"),
        };
        }
        public static List<Client> GetClients()
        {
            return new List<Client>()
            {
                 new Client {
                        ClientId="bugetoclientid",
                        ClientSecrets=new List<Secret> { new Secret("123456".Sha256()) },
                        AllowedGrantTypes=GrantTypes.Implicit,
                        RedirectUris={"https://localhost:44300/signin-oidc" },
                        PostLogoutRedirectUris={"https://localhost:44300/signout-callback-oidc"},
                        AllowedScopes =new List<string>
                        {
                             IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                             IdentityServer4.IdentityServerConstants.StandardScopes.Profile,
                             IdentityServer4.IdentityServerConstants.StandardScopes.Email,
                             IdentityServer4.IdentityServerConstants.StandardScopes.Phone,
                        },
                        RequireConsent=true,
                     },

                 new Client
                 {
                    ClientId="ApiHavaShenasi",
                    ClientSecrets=new List<Secret> { new Secret("123456".Sha256()) },
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    AllowedScopes=new []{ "ApiHava" }
                 }
            };
        }
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>()
        {
            new ApiScope("ApiHava","سرویس هواشناسی")
            };

        }
    }
}
