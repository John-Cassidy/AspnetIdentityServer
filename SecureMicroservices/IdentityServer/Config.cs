using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityServerHost.Quickstart.UI;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;

namespace IdentityServer
{
    public class Config {
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                   //new Client
                   //{
                   //     ClientId = "movieClient",
                   //     AllowedGrantTypes = GrantTypes.ClientCredentials,
                   //     ClientSecrets =
                   //     {
                   //         new Secret("secret".Sha256())
                   //     },
                   //     AllowedScopes = { "movieAPI" }
                   //},
                   new Client
                   {
                       ClientId = "movies_mvc_client",
                       ClientName = "Movies MVC Web App",
                       AllowedGrantTypes = GrantTypes.Hybrid,
                       RequirePkce = false,
                       AllowRememberConsent = false,
                       RedirectUris = new List<string>()
                       {
                           "https://localhost:5002/signin-oidc"
                       },
                       PostLogoutRedirectUris = new List<string>()
                       {
                           "https://localhost:5002/signout-callback-oidc"
                       },
                       ClientSecrets = new List<Secret>
                       {
                           new Secret("secret".Sha256())
                       },
                       AllowedScopes = new List<string>
                       {
                           IdentityServerConstants.StandardScopes.OpenId,
                           IdentityServerConstants.StandardScopes.Profile,
                           IdentityServerConstants.StandardScopes.Address,
                           IdentityServerConstants.StandardScopes.Email,
                           "movieAPI",
                           "roles"
                       }
                   }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
           new ApiScope[]
           {
               new ApiScope("movieAPI", "Movie API")
           };

        public static IEnumerable<ApiResource> ApiResources =>
          new ApiResource[]
          {
               //new ApiResource("movieAPI", "Movie API")
          };

        public static IEnumerable<IdentityResource> IdentityResources =>
          new IdentityResource[]
          {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              new IdentityResources.Address(),
              new IdentityResources.Email(),
              new IdentityResource(
                    "roles",
                    "Your role(s)",
                    new List<string>() { "role" })
          };

        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                    Username = "john",
                    Password = "jpc",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Name, "John Cassidy"),
                        new Claim(JwtClaimTypes.GivenName, "john"),
                        new Claim(JwtClaimTypes.FamilyName, "cassidy"),
                        new Claim(JwtClaimTypes.Email, "candiawoods@gmail.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(new {
                                                                                        street_addres = "55 Spring Street",
                                                                                        locality = "Cambridge",
                                                                                        postal_code = "02138",
                                                                                        country = "United States"
                                                                                    }), IdentityServerConstants.ClaimValueTypes.Json),
                         new Claim(JwtClaimTypes.Role, "user"),
                         new Claim(JwtClaimTypes.Role, "admin")
                    }
                },
                new TestUser
                    {
                        SubjectId = "88421113",
                        Username = "bob",
                        Password = "bbs",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Bob Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Bob"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(new
                                                                                        {
                                                                                            street_address = "One Hacker Way",
                                                                                            locality = "Heidelberg",
                                                                                            postal_code = 69118,
                                                                                            country = "Germany"
                                                                                        }), IdentityServerConstants.ClaimValueTypes.Json),
                            new Claim(JwtClaimTypes.Role, "admin")
                        }
                    }
            };       
    }

}
