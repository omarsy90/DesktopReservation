﻿using DeskReservationAPI.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace DeskReservationAPI.Utility
{
    public interface ITokenManager
    {
        SymmetricSecurityKey GetSymmetricSecurityKey();
        string GetToken(Dictionary<string, string> keyValuePairs);

        public string GetClaimByKey(string token, string key);


    }

    public class TokenManager : ITokenManager
    {


        private JWTSetting _jwtSetting;

        private TokenValidationParameters _tokenValidationParameters;
        private SymmetricSecurityKey _symmetricSecurityKey;
        private SigningCredentials _signingCredentials;
        public TokenManager(JWTSetting jwtSetting)
        {

            _jwtSetting = jwtSetting;
            _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSetting.Key));
            _signingCredentials = new SigningCredentials(_symmetricSecurityKey, jwtSetting.SecurityAlgorithm);

            _tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = jwtSetting.ValidateIssuerSigningKey,
                IssuerSigningKey = _symmetricSecurityKey,
                ValidateIssuer = jwtSetting.ValidateIssuer,
                ValidateAudience = jwtSetting.ValidateAudience,
                ClockSkew = jwtSetting.ClockSkew,
                ValidIssuer = _jwtSetting.Issuer,
                ValidAudience = _jwtSetting.Audience,

            };


        }


        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return _symmetricSecurityKey;
        }
        public string GetToken(Dictionary<string, string> keyValuePairs)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSetting.Key);
            List<Claim> claims = new List<Claim>();
            foreach (var keyValue in keyValuePairs)
            {
                Claim claim = new Claim(keyValue.Key, keyValue.Value);
                claims.Add(claim);
            }

            var tokenDescriptor = new SecurityTokenDescriptor

            {
                Audience = _jwtSetting.Audience,
                Issuer = _jwtSetting.Issuer,

                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(_jwtSetting.DurationOnDays),
                SigningCredentials = _signingCredentials
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public string GetClaimByKey(string token , string key)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            Dictionary<string, string> claimDic = new Dictionary<string, string>();
            foreach (var item in jwtToken.Claims)
            {
                claimDic.Add(item.Type, item.Value);
            }

            claimDic.TryGetValue(key, out string userID);
            return userID;
        }
      
    }


    public class JWTSetting
    {
        public string Key { get; set; }
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int DurationOnDays { get; set; }


       public bool  ValidateIssuerSigningKey { set; get; }
        public bool ValidateIssuer { set; get; }

        public bool ValidateAudience { set; get; }
        public TimeSpan ClockSkew { get; set; }

        public string SecurityAlgorithm { get; set; }



    }
 
}
