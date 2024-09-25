using BackEnd.Interfaces;
using BackEnd.Models;
using BackEnd.Services;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Logic
{
  

    public class UserLogic : IUserLogic
    {
        private readonly IUserService _userService;
        private readonly string _jwtKey = "unaClaveMuySeguraQueDebeTenerAlMenos32Caracteres"; 

        public UserLogic(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<LoginResponse> Login(string email, string password)
        {
            try
            {
                var user = await _userService.GetUserByEmail(email);
                if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    return null; 
                }

                var claims = new[]
                {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: creds);

                return new LoginResponse
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    UserId = user.Id.ToString()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Login: {ex.Message}");
                throw new Exception("Error al iniciar sesión.");
            }
        }


        public async Task<User> Register(string firstName, string lastName, string email, string password)
        {
            try
            {
                var existingUser = await _userService.GetUserByEmail(email);
                if (existingUser != null)
                {
                    throw new Exception("El email ya está registrado.");
                }

                var user = new User
                {
                    Id = Guid.NewGuid(), 
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Password = BCrypt.Net.BCrypt.HashPassword(password),  
                    RegistrationDate = DateTime.UtcNow  
                };
                return await _userService.AddUser(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Register: {ex.Message}");
                throw new Exception("Error al registrar usuario.");
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                return await _userService.GetAllUsers();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetAllUsers: {ex.Message}");
                throw new Exception("Error al obtener la lista de usuarios.");
            }
        }

        public async Task<User> GetUserById(Guid id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                if (user == null)
                {
                    throw new Exception("Usuario no encontrado.");
                }
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetUserById: {ex.Message}");
                throw new Exception("Error al obtener el usuario.");
            }
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            try
            {
                var userDeleted = await _userService.DeleteUser(id);
                if (!userDeleted)
                {
                    throw new Exception("No se pudo eliminar el usuario.");
                }
                return userDeleted;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en DeleteUser: {ex.Message}");
                throw new Exception("Error al eliminar el usuario.");
            }
        }
    }
}
