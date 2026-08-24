using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventPlus.WebAPI.Controller
{

    //controller responsavel pela autentico de users via JWT (JSON Web Token)
    //
    // como funciona JWT?
    // user envia email e senha via post /api/Login
    // a API valida as credenciais no banco (email e hash BCrypt)
    // 

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuario _usuario;
        private readonly IConfiguration _configuration;

        public LoginController(IUsuario usuario, IConfiguration configuration)
        {
            _usuario = usuario;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            // 1 passo: busca user pelo email e valida a senha com o BCrypt
            var usuarioEncontrado = await _usuario.BuscarEmailSenha(dto.Email, dto.Senha);

            // 2 passo: se as credenciais forem invalidas, retorna status code 401 unauthorized 
            if (usuarioEncontrado == null)
            {
                return Unauthorized("Email ou senha invalidos");
            }

            // 3 passo: criar lista de claims(infos q ficam dentro do toke)
            // claims: sao como "afirmacoes" sobre o user q ficam codificadas no token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuarioEncontrado.IdUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuarioEncontrado.Email),
                new Claim("nome", usuarioEncontrado.Nome),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // 4 passo: criar chave de seguranca com base na chave secreta definida
            var chaveSecreta = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
                );
            //Encoding.UTF8 : Definir o padrao de codificacao de caracteres
            //GetBytes : pega a string e devolve um array de bytes

            // 5 passo: definir o algoritmo de assinatura (HMACSHA256 é o padrao)
            var credenciais =  new SigningCredentials(chaveSecreta, SecurityAlgorithms.HmacSha256);

            // 6 passo: montar o token jwt com as infos 
            var token = new JwtSecurityToken(
                issuer: "EventPlus.WebAPI",
                audience: "EventPlus.WebAPI",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: credenciais
                );

            // 7 passo: converter o token para string e retornar cliente
            string tokenString = new JwtSecurityTokenHandler().WriteToken( token );

            return Ok(new
            {
                Token = tokenString,
                Expiracao = token.ValidTo,
                Usuario = new
                {
                    usuarioEncontrado.IdUsuario,
                    usuarioEncontrado.Nome,
                    usuarioEncontrado.Email
                }
            }

                );
        }
    }
}
