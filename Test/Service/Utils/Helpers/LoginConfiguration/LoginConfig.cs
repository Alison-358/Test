using Domain.Dto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Service.Utils.Helpers.LoginConfiguration
{
    public class LoginConfig
    {
        public Credentials LoginCredentials(UserLoginDto userloginDto, Token _token, Signing _signing)
        {
            var role = "";

            bool credentialsValid;

            credentialsValid = userloginDto != null ? true : false;

            if (credentialsValid)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(userloginDto.Name, "Login"),
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, userloginDto.Name),
                        new Claim("Store", userloginDto.RoleName.ToLower()),
                        new Claim(ClaimTypes.Email, userloginDto.Email),
                        new Claim(ClaimTypes.Role, userloginDto.RoleName),
                        //new Claim(ClaimTypes.Name, credentialsUser.UserName)
                    }
                    //CookieAuthenticationDefaults.AuthenticationScheme
                );

                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate + TimeSpan.FromSeconds(_token.Seconds);

                var handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, createDate, expirationDate, handler, _token, _signing);

                return CredentialsSuccess(createDate, expirationDate, token, identity);
            }
            else
            {
                return CredentialsException();
            }
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler, Token _token, Signing _signing)
        {
            //Configuration Token
            var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Issuer = _token.Issuer,
                Audience = _token.Audience,
                SigningCredentials = _signing.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);

            return token;
        }

        private Credentials CredentialsException()
        {
            return new Credentials
            {
                Authenticated = false,
                Message = "Falha na autenticação"
            };
        }

        private Credentials CredentialsSuccess(DateTime createDate, DateTime expirationDate, string token, ClaimsIdentity identity)
        {
            return new Credentials()
            {
                Authenticated = true,
                Created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                Message = "Ok"
            };
        }
    }
}
