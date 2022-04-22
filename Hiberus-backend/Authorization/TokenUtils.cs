using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Hiberus.Model.Models.HiberusEntity;
using HiberusBackend.OpenAPI;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HiberusBackend.Authorization
{
	public static class TokenUtils
	{
		private static readonly string SecretKey = Environment.GetEnvironmentVariable("SECRETKEY") ; 
		private static readonly SecurityKey securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(SecretKey != null ? SecretKey : "SecretKeyMinValu"));
		private static readonly string ExpireToken = Environment.GetEnvironmentVariable("TOKEN_DURATION_HOURS");
		private static readonly string AudieceToken = Environment.GetEnvironmentVariable("WEBSITE"); 
		private static readonly string IssuerToken = Environment.GetEnvironmentVariable("WEBSITE");

		public static TokenValidationParameters tokenValidationParameters => new TokenValidationParameters
		{
			ValidAudience = AudieceToken != null ? AudieceToken : "http://localhost:7071", 
			ValidIssuer = IssuerToken != null ? IssuerToken : "http://localhost:7071",
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			// DELEGADO PERSONALIZADO PARA COMPROBAR LA CADUCIDAD EL TOKEN.
			LifetimeValidator = LifetimeValidator,
			IssuerSigningKey = securityKey
		};

		/// <summary>
		/// Crear un Token Segun el modelo Enviado, Configurado solo para modelo UsuarioDto por ahora 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="model"></param>
		/// <returns>Devuelve el token en string o nulo en caso de no estar el modelo modificado</returns>
		public static Task<string> BuildTokenForUsers<T>(T model)
		{
			var type = typeof(T);
			string jwtTokenString = string.Empty;
			var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(SecretKey));
			var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
			var claims = new List<Claim>();
			if (typeof(string) == type)
			{ 
				string Email = model as string;
				claims.Add(new Claim(ClaimTypes.Email , Email));
			}
			if (claims.Count > 0)
			{
				ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);
				JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
				JwtSecurityToken jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
					audience: AudieceToken,
					issuer: IssuerToken ?? "https://localhost:44314",
					subject: claimsIdentity,
					notBefore: DateTime.UtcNow,
					expires: Int32.TryParse(ExpireToken, out int expiretoken) ? DateTime.UtcNow.AddMinutes(expiretoken) : DateTime.UtcNow.AddMinutes(Convert.ToInt32(2400)),
					signingCredentials: signingCredentials);
				jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
			}
			return Task.FromResult(jwtTokenString);
		}
		
		private static bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
		{
			var valid = false;
			if ((expires.HasValue && DateTime.UtcNow < expires)
				&& (notBefore.HasValue && DateTime.UtcNow > notBefore))
			{ valid = true; }
			return valid;
		}
		 
	}
}
