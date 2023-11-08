using AutoMapper;
using Business.Business;
using Entities.Entities;
using Facade.DTOs;
using Facade.Mappers;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Facade.Logic
{

    public class UserFacade
    {
        private IMapper _mapper;
        private UsuariosBusiness _userBusiness;
        private Jwt _jwt;
        private LoggerFacade _logger;

        public UserFacade(SettingsHelper settings, Jwt jwt)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            if (string.IsNullOrEmpty(settings.Connection))
                throw new ArgumentNullException(nameof(settings.Connection));

            _userBusiness = new UsuariosBusiness(settings);
            _jwt = jwt;

            _mapper = Mapping.configMapper();
            _logger = new LoggerFacade(settings);
        }

        public TokenDTO Login(LoginDTO login)
        {
            try
            {
                TokenDTO tokenresult = new TokenDTO();
                var usuario = _userBusiness.Get(login.Username);
                if (usuario == null)
                    throw new ArgumentException(nameof(login));

                if (string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
                {
                    tokenresult.UserValid = false;
                    tokenresult.Token = string.Empty;
                    return tokenresult;
                }

                if (Security.Validat(usuario.UserPsw, login.Password))
                {

                    var claims = new List<Claim>()
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,_jwt.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToString()),
                        new Claim("rol",usuario.RolId.ToString())
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        _jwt.Issuer,
                        _jwt.Audience,
                        claims,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signIn);

                    tokenresult.UserValid = true;
                    tokenresult.Token = new JwtSecurityTokenHandler().WriteToken(token);
                }
                else
                {
                    tokenresult.Token = string.Empty;
                    tokenresult.UserValid = false;
                }
                _logger.Log(Entidades.Users, Acciones.Login, (int)usuario.UserId, JsonConvert.SerializeObject(new { Fullname = usuario.FullName, username = usuario.UserName }));
                return tokenresult;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool AddUser(UserDTO entity)
        {
            try
            {
                if (string.IsNullOrEmpty(entity.UserPsw))
                {
                    return false;
                }
                else
                {
                    var hashPsw = Security.Hash(entity.UserPsw);
                    if (hashPsw != null) { entity.UserPsw = hashPsw; }
                }
                _logger.Log(Entidades.Users, Acciones.Create, JsonConvert.SerializeObject(new { Fullname = entity.FullName, username = entity.UserName }));

                return _userBusiness.Add(_mapper.Map<User>(entity));
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
