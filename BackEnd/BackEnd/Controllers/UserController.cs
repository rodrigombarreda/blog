using BackEnd.Interfaces;
using BackEnd.Logic;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserLogic _userLogic;

        public UserController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        // Registro de usuario
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel registerModel)
        {
            try
            {
                var result = await _userLogic.Register(registerModel.FirstName, registerModel.LastName, registerModel.Email, registerModel.Password);
                if (result != null)
                    return Ok(result);
                return BadRequest("Error registrando el usuario.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error: {ex.Message}");
            }
        } 

        // Loguear usuario
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] UserLoginModel loginModel)
        {
            try
            {
                var loginResponse = await _userLogic.Login(loginModel.Email, loginModel.Password);

                if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token))
                    return Unauthorized("Credenciales inválidas.");

                
                return Ok(loginResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error: {ex.Message}");
            }
        }

        // Obtener todos los usuarios
        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _userLogic.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error: {ex.Message}");
            }
        }

        // Obtener usuario por ID
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var user = await _userLogic.GetUserById(id);
                if (user != null)
                    return Ok(user);
                return NotFound("Usuario no encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error: {ex.Message}");
            }
        }

        // Borrar usuario por ID 
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var success = await _userLogic.DeleteUser(id);
                if (success)
                    return Ok();
                return NotFound("Usuario no encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error: {ex.Message}");
            }
        }
    }

    public class UserLoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserRegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
