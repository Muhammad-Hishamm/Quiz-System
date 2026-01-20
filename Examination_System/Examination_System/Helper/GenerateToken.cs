using Examination_System.Data;

namespace Examination_System.Helper
{
    public class GenerateToken
    {
        public static string Generate( string userId, string Name)
        {
            var key = System.Text.Encoding.ASCII.GetBytes(Constants.SecretKey);
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, Name),
                    new System.Security.Claims.Claim("ID", userId)
                }),
                Expires = System.DateTime.Now.AddHours(1),
                SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                    new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                    Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature),
                Issuer = "",
                Audience = ""
            };  
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
            //return System.Guid.NewGuid().ToString();
        }
    }
}
