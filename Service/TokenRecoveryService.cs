

using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;


namespace LHDTV.Service
{
    public class TokenRecoveryService : ITokenRecoveryService
    {


        public TokenRecoveryService()
        {


        }

        private string GetClaim(string token, string claim)
        {

            var tokenHandler = new JwtSecurityTokenHandler().ReadJwtToken(token);

            return tokenHandler.Claims.FirstOrDefault(c => c.Type == claim).Value;

        }

        public int RecoveryId(string token)
        {

            if (token == null)
            {

                return -1;//notfounexception
            }
            try
            {

                return int.Parse(GetClaim(token, ClaimTypes.Name));

            }
            catch (FormatException)
            {

                return -1;
            }


        }

        public string RecoveryToken(HttpContext context)
        {
            var regExp = new Regex("Bearer (?<token>.*)");
            StringValues tokens;
            var found = context.Request.Headers.TryGetValue("Authorization", out tokens);
            var myToken = "";

            if (found)
            {
                foreach (var t in tokens)
                {
                    var match = regExp.Match(t);
                    if (match.Success)
                    {
                        myToken = match.Groups["token"].Value;
                        return myToken;

                    }


                }
            }
            return null;
        }
    }
}