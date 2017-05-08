using System.Collections.Generic;
using IdentityServer4.Models;

namespace Aromato.Auth
{
    public class Config
    {

        public static IEnumerable<Client> AuthClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "sysad",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "inventory.read", "employee.read", "inventory.write", "employee.write" },
                    AllowedCorsOrigins = { "http://localhost/"}
                },
                new Client
                {
                    ClientId = "wscholar",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "inventory.read", "inventory.write", "employee.read" },
                    AllowedCorsOrigins = { "http://localhost/"}
                }
            };
        }

        public static IEnumerable<ApiResource> ApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "aromato",

                    // secret for introspection endpoint
                    ApiSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // API has multiple scopes
                    Scopes =
                    {
                        new Scope
                        {
                            Name = "inventory.read",
                            DisplayName = "Read only access to the inventory.",
                            UserClaims = {"inventory.read"}
                        },
                        new Scope
                        {
                            Name = "inventory.write",
                            DisplayName = "Full access to the inventory.",
                            Emphasize = true,
                            UserClaims = {"inventory.write"}
                        },
                        new Scope
                        {
                            Name = "employee.read",
                            DisplayName = "Read only access to employee personal information.",
                            UserClaims = {"employee.read"}
                        },
                        new Scope
                        {
                            Name = "employee.write",
                            DisplayName = "Full access to employee personal information, including modifying, " +
                                          "adding, and deleting employee records.",
                            Emphasize = true,
                            UserClaims = {"employee.write"}
                        }
                    }
                }
            };
        }
    }
}