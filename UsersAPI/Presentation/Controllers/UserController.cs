using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using UsersAPI.Domain.Entities;
using UsersAPI.Domain.Validations;

namespace UsersAPI.Presentation.Controllers
{
    [ApiController]
    [Route("Usuarios")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost("logar-usuario")]
        public async Task<IActionResult> Logar(LoginValidation loginUser)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginUser.Username);

                if (user != null && await _userManager.CheckPasswordAsync(user, loginUser.Password))
                {
                    var identity = new ClaimsIdentity("cookies");
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

                    await HttpContext.SignInAsync("cookies", new ClaimsPrincipal(identity));

                    return Ok(IdentityResult.Success);
                }
                else
                {
                    ModelState.AddModelError("", "Usuário ou senha invalida");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("registrar-usuario")]
        public async Task<IActionResult> RegisterUser(RegisterValidation user)
        {
            if (ModelState.IsValid)
            {
                var userData = await _userManager.FindByNameAsync(user.UserName);

                if (userData == null)
                {
                    userData = new User()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = user.UserName
                    };
                }
                else
                {
                    return BadRequest("Usuário já Existente");
                }

                var result = await _userManager.CreateAsync(
                    userData, user.Password);

                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("buscar-usuario-pelo-username")]
        public async Task<IActionResult> UserSearch(string userName)
        {
            var userData = await _userManager.FindByNameAsync(userName);

            if (userData != null)
            {
                return Ok(userData);
            }
            else
            {
                return NotFound(userName);
            }
        }
        [HttpPut("atualizar-usuario-cadastrado")]
        public async Task<IActionResult> UserUpdate(string userName, LoginValidation user)
        {
            var userData = await _userManager.FindByNameAsync(userName);

            if (userData != null)
            {
                userData.UserName = user.Username;
                userData.NormalizedUserName = _userManager.NormalizeName(user.Username);
                userData.PasswordHash = _userManager.PasswordHasher.HashPassword(userData, user.Password);

                await _userManager.UpdateAsync(userData);

                return Ok(userData);
            }
            else
            {
                return NotFound(userName);
            }

        }

        [HttpDelete("deletar-usuario")]
        public async Task<IActionResult> UserDelete(string userName)
        {
            var userData = await _userManager.FindByNameAsync(userName);

            if (userData != null)
            {
                await _userManager.DeleteAsync(userData);

                return Ok(userData);
            }
            else
            {
                return NotFound(userName);
            }
        }
        [HttpGet("deslogar-usuario")]
        public async Task<IActionResult> UserLogout(string userName)
        {
            var userData = HttpContext.User.Identity;
            if (userData.Name == userName)
            {
                await HttpContext.SignOutAsync("cookies");
                return Ok(IdentityResult.Success);
            }
            else
            {
                return BadRequest("Usuário não está Logado.");
            }


        }
    }
}
