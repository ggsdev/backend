using dotenv.net;
using PRIO.src.Shared.Errors;
using System.DirectoryServices.AccountManagement;

namespace PRIO.src.Shared.Utils
{
    public static class ActiveDirectory
    {
        public static bool VerifyCredentialsWithActiveDirectory(string email, string password)
        {
            try
            {
                var envVars = DotEnv.Read();
                var domain = envVars["DOMINIO"];
                var serverAd = envVars["ACTIVEDIRECTORY"];
                using var context = new PrincipalContext(ContextType.Domain, serverAd, domain);
                return context.ValidateCredentials(email, password);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
    }
}
