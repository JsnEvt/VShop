using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using VShop.IdentityServer.Configuration;
using VShop.IdentityServer.Data;

namespace VShop.IdentityServer.SeedDatabase;

public class DatabaseIdentityServerInitializer : IDatabaseSeedInitializer
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DatabaseIdentityServerInitializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void InitializeSeedRoles()
    {
        //se o perfil Admin nao existir entao cria o perfil
        if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Admin).Result)
        {
            //cria o perfil Admin
            IdentityRole roleAdmin = new IdentityRole();
            roleAdmin.Name = IdentityConfiguration.Admin;
            roleAdmin.NormalizedName = IdentityConfiguration.Admin.ToUpper();
            _roleManager.CreateAsync(roleAdmin).Wait();
        }

        //se o perfil Client nao existir entao cria o perfil
        if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Client).Result)
        {
            //cria o perfil Admin
            IdentityRole roleClient = new IdentityRole();
            roleClient.Name = IdentityConfiguration.Client;
            roleClient.NormalizedName = IdentityConfiguration.Client.ToUpper();
            _roleManager.CreateAsync(roleClient).Wait();
        }
    }

    public void InitializeSeedUsers()
    {
        //se o usuario admin nao existir entao cria o usuario, define a senha e atribui ao perfil
        if (_userManager.FindByEmailAsync("admin1@com.br").Result == null)
        {
            //define os dados do usuario admin
            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "admin1",
                NormalizedUserName = "ADMIN1",
                Email = "admin1@com.br",
                NormalizedEmail = "ADMIN1@COM.BR",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "+55(11)12345-6789",
                FirstName = "Usuario",
                LastName = "Admin1",
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            //cria o usuario Admin e atribui a senha
            IdentityResult resultAdmin = _userManager.CreateAsync(admin, "Numsey#123").Result;
            if (resultAdmin.Succeeded)
            {
                //inclui o usuario admin ao perfil admin
                _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin).Wait();

                //inclui as claims do usuario admin
                var adminClaims = _userManager.AddClaimsAsync(admin, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin),
                }).Result;
            }
        }

        //se o usuario client nao existir entao cria o usuario, define a senha e atribui ao perfil
        if (_userManager.FindByEmailAsync("client1@com.br").Result == null)
        {
            //define os dados do usuario admin
            ApplicationUser client = new ApplicationUser()
            {
                UserName = "client1",
                NormalizedUserName = "CLIENT1",
                Email = "client1@com.br",
                NormalizedEmail = "CLIENT1@COM.BR",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "+55(11)12345-9876",
                FirstName = "Usuario",
                LastName = "Client1",
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            //cria o usuario Admin e atribui a senha
            IdentityResult resultAdmin = _userManager.CreateAsync(client, "Numsey#123").Result;
            if (resultAdmin.Succeeded)
            {
                //inclui o usuario admin ao perfil admin
                _userManager.AddToRoleAsync(client, IdentityConfiguration.Client).Wait();

                //inclui as claims do usuario admin
                var adminClaims = _userManager.AddClaimsAsync(client, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, client.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, client.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin),
                }).Result;
            }
        }

    }
}