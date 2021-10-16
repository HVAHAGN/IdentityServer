using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class Configuration
    {
        public static IEnumerable<ApiResource> GetApis() =>
                   new List<ApiResource>()
                   {
                           new ApiResource("ApiOne"),
                           new ApiResource("ApiTwo")
                   };

        public static IEnumerable<Client> GetClients() => new List<Client>() {
        new Client() {
                ClientId="client_Id",
                 ClientSecrets={new Secret("client_secret".ToSha256()) },
                 AllowedGrantTypes=GrantTypes.ClientCredentials,
                 AllowedScopes={"ApiOne"}
                    } ,

        new Client() {
                 ClientId="client_Id_mvc",
                 ClientSecrets={new Secret("client_secret_mvc".ToSha256()) },

                 AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,

                    RedirectUris = { "https://localhost:44366/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:44366/Home/Index" },

                    
                 AllowedScopes={"ApiOne", "ApiTwo", IdentityServerConstants.StandardScopes.OpenId,
                                                    IdentityServerConstants.StandardScopes.Profile }
        } };


    }
}
