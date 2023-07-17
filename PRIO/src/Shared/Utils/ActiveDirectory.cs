using dotenv.net;
using PRIO.src.Shared.Errors;
using System.DirectoryServices.AccountManagement;

namespace PRIO.src.Shared.Utils
{
    public static class ActiveDirectory
    {
        private static readonly IDictionary<string, string> _envVars = DotEnv.Read();
        private static readonly string _domain = _envVars["DOMINIO"];
        private static readonly string _serverAd = _envVars["AD"];

        public static bool VerifyCredentialsWithActiveDirectory(string username, string password)
        {
            try
            {
                using var context = new PrincipalContext(ContextType.Domain, _domain, _serverAd);
                return context.ValidateCredentials(username, password);
            }
            catch (Exception ex)
            {

                throw new BadRequestException(ex.Message, status: "400");
            }
        }

        //public static bool CheckUserExistsInActiveDirectory(string username)
        //{

        //    try
        //    {
        //        using var context = new PrincipalContext(ContextType.Domain, _domain, _serverAd);

        //        var userPrincipal = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, username) ??
        //                 UserPrincipal.FindByIdentity(context, IdentityType.UserPrincipalName, username) ??
        //                 UserPrincipal.FindByIdentity(context, IdentityType.DistinguishedName, username);

        //        return userPrincipal is not null;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        throw new BadRequestException(ex.Message, status: "400");
        //    }
        //}
    }
}
