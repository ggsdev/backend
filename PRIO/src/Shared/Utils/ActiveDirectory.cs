using dotenv.net;
using PRIO.src.Shared.Errors;
using System.DirectoryServices.AccountManagement;

namespace PRIO.src.Shared.Utils
{
    public static class ActiveDirectory
    {
        public static bool VerifyCredentialsWithActiveDirectory(string username, string password)
        {
            try
            {
                var envVars = DotEnv.Read();
                var domain = envVars["DOMINIO"];
                var serverAd = envVars["AD"];
                using var context = new PrincipalContext(ContextType.Domain, domain, serverAd);
                return context.ValidateCredentials(username, password);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
    }
}
