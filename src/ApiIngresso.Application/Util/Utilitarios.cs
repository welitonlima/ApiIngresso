using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ApiIngresso.Application.Util
{
    public class Utilitarios
    {
        public static string GenerateToken(int Id, string Role, int IdEmpresa, string Nome)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Constantes.AUTHORIZE_HASH);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, Nome),
                        new Claim(Constantes.JWT_ID_USUARIO, Id.ToString()),
                        new Claim(ClaimTypes.Role, Role),
                        new Claim(Constantes.JWT_USER_PERFIL, Role),
                        new Claim(Constantes.JWT_ID_EMPRESA, IdEmpresa.ToString()),
                    }
                 ),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public static string jwtGetValue(HttpContext context, string Field)
        {
            try
            {
                return context.User.Claims.FirstOrDefault(x => x.Type == Field).Value;
            }
            catch
            {
                return "";
            }
        }

        public static string byteArrayToString(byte[] inputArray)
        {
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < inputArray.Length; i++)
            {
                output.Append(inputArray[i].ToString("X2"));
            }
            return output.ToString();
        }

        public static string getHashSha256(string text)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(text));
            return byteArrayToString(hashedDataBytes);
        }
    }
}
