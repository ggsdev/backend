using Microsoft.IdentityModel.Tokens;
using PRIO.Data;
using PRIO.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PRIO.Services
{
    public class TokenServices
    {

        private readonly DataContext _context;

        public TokenServices(DataContext context)
        {
            _context = context;
        }

        private static string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> CreateSessionAndToken(User user, string userHttpAgent)
        {
            if (user.Session is not null && (user.Session.ExpiresIn > DateTime.UtcNow.ToLocalTime() && user.Session.UserHttpAgent == userHttpAgent))
                return user.Session.Token;

            var token = GenerateToken(user);

            if (user.Session is null)
            {
                var session = new Session
                {
                    Token = token,
                    User = user,
                    UserHttpAgent = userHttpAgent
                };

                await _context.Sessions.AddAsync(session);
                await _context.SaveChangesAsync();

                return token;
            }

            if (user.Session.ExpiresIn < DateTime.UtcNow.ToLocalTime() || user.Session.UserHttpAgent != userHttpAgent)
            {
                user.Session = new Session
                {
                    Token = token,
                    ExpiresIn = DateTime.UtcNow.AddDays(5).ToLocalTime(),
                    UserHttpAgent = userHttpAgent
                };

                _context.Sessions.Update(user.Session);
                await _context.SaveChangesAsync();

                return user.Session.Token;

            }

            return token;
        }
    }
}
