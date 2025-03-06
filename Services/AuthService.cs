using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIBSAPI.Data;
using SIBSAPI.DTOs;
using SIBSAPI.Global;
using SIBSAPI.Master;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
//using SIBSAPI.Models;

namespace SIBSAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ApplicationDbContext _context;
        private readonly MasterMethod _masterMethod;

        public AuthService(IJwtTokenService jwtTokenService, ApplicationDbContext context, IConfiguration configuration)
        {
            _jwtTokenService = jwtTokenService;
            _context = context;
            _masterMethod = new MasterMethod(context, configuration);
        }

        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            //var user = new ApplicationUser
            //{
            //    UserName = registerDto.Email,
            //    Email = registerDto.Email,
            //    FirstName = registerDto.FirstName, // Ensure this is assigned
            //    LastName = registerDto.LastName
            //};

            //var result = await _userManager.CreateAsync(user, registerDto.Password);
            
            //if (!result.Succeeded)
            //    return null;

            return "User registered successfully";
        }
        

        public async Task<LoginResponse> LoginAsync(TLogin login)
        {
            try
            {
                var existingUser = await _context.Login
                    .Where(u => u.Login_Name.ToLower() == login.Login_Name.ToLower())
                    .Select(u => new
                    {
                        u.Login_Name,
                        u.Login_Pwd,
                        //u.Activity_Name
                    })
                    .FirstOrDefaultAsync();

                if (existingUser == null)
                {
                    return new LoginResponse
                    {
                        StatusCode = 404,
                        Message = "Invalid Username",
                        Header = "failed",
                        //Activity_Name = null
                    };
                }

                if (existingUser.Login_Pwd.ToLower() != login.Login_Pwd.ToLower())
                {
                    return new LoginResponse
                    {
                        StatusCode = 401,
                        Message = "Invalid Password",
                        Header = "failed",
                        //Activity_Name = null
                    };
                }

                return new LoginResponse
                {
                    StatusCode = 200,
                    Message = "Login Successful",
                    Header = "succeed",
                    //Activity_Name = existingUser.Activity_Name
                };
            }
            catch (Exception ex)
            {
                return new LoginResponse
                {
                    StatusCode = 500,
                    Header = "failed",
                    Message = ((Microsoft.Data.SqlClient.SqlException)ex).Number == 823 ? "Complete a full database consistency check (DBCC CHECKDB or connection string)." : "An error occurred while processing the request",
                    // Activity_Name = null
                };
            }
        }








    }
}
