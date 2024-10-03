using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace VShop.IdentityServer.Configuration
{
    public class IdentityConfiguration
    {
        //definindo os recursos, os escopos e os clientes do IdentityServer
        public const string Admin = "Admin";
        public const string Client = "Client";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                //vshop e a aplicacao web que vai acessar
                //o IdentityServer para obter o token
                new ApiScope("vshop", "VShop Server"),
                new ApiScope(name: "read", "Read data."),
                new ApiScope(name: "write", "Write data."),
                new ApiScope(name: "delete", "Delete data.")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                //cliente generico
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("tata#tata".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials, //precisa das credenciais do usuario
                    AllowedScopes = { "read", "write", "profile" }
                },
                new Client
                {
                    ClientId = "vshop",
                    ClientSecrets = { new Secret("tata#tata".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code, //via codigo
                    RedirectUris = { "https://locahost:7165/signin-oidc" }, //login
                    PostLogoutRedirectUris = { "https://localhost:7165/signout-callback-oidc" }, //logout
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "vshop"
                    }
                }
            };
            }
}