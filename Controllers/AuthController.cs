using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SIBSAPI.Data;
using SIBSAPI.DTOs;
using SIBSAPI.Global;
using SIBSAPI.Master;
using SIBSAPI.Services;
using System.Data;
using System.Reflection.PortableExecutable;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SIBSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ApplicationDbContext _context;
        private readonly MasterMethod _masterMethod;




        public AuthController(IAuthService authService, ApplicationDbContext context, IConfiguration configuration)
        {
            _authService = authService;
            _context = context;
            _masterMethod = new MasterMethod(context, configuration);
        }



        [HttpPost("Test_login")]
        public async Task<IActionResult> TT_Login([FromBody] TLogin login)
        {
            var FinalData = new Dictionary<string, List<Dictionary<string, object>>> ();
            var result = await _authService.LoginAsync(login);
            if (result.Header == "succeed")
            {
                string Tast_Query = @"
                                            BEGIN
                                                -- First Query (Divisions)
                                                SELECT p.PROFCEN_CD, p.[DESC], p.company_name
                                                FROM Profcen p
                                                JOIN Login l ON (',' + l.DIVISION + ',' LIKE '%,' + CAST(p.PROFCEN_CD AS VARCHAR) + ',%' OR l.DIVISION = 'C')
                                                WHERE l.login_name = @loginName 
                                                AND p.INUSE_FLAG = 'Y';

                                                -- Second Query (Modules)
                                                SELECT * FROM SIBS;

                                                -- Fourth Query (Department Table)
                                                SELECT * FROM Dept;

                                                -- Fifth Query (Designation Table)
                                                SELECT * FROM DESIG_MST;
                                            END";
                var parameters = new Dictionary<string, object>
                    {
                        { "@loginName", login.Login_Name }
                    };
                var resultSetNames = new List<string> { "Divisions", "Modules", "Departments", "Designation" };
                
                FinalData = await _masterMethod.TExecuteMultipleQueriesAsync(Tast_Query, parameters, resultSetNames);

            }

            return Ok(new { message = result.Message, status = result.Header, JsonData = FinalData });

        }
































        //working
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] TLogin login)
        {
            var result = await _authService.LoginAsync(login);
            return Ok(new { message = result.Message, status = result.Header });

        }

        [NonAction]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);
            if (result == null)
                return BadRequest("Registration failed.");
            return Ok(result);
        }

        [NonAction]
        [HttpGet("check-login/{loginName}")]
        public async Task<string> CheckLoginExists(string loginName)
        {
            bool exists = await _context.Login.AnyAsync(l => l.Login_Name == loginName);

            if (exists)
                return "Login name already exists.";
            return "Login name is available.";
            //    return ok(new { exists = true, message = "Login name already exists." });

            //return Ok(new { exists = false, message = "Login name is available." });


        }

        [NonAction]
        [HttpGet("login-data")]
        public async Task<IActionResult> GetLoginData()
        {
            var dataTable = await GetLoginTableData();
            var jsonResult = _masterMethod.ConvertDataTableToJson(dataTable);
            return Ok(jsonResult); // ✅ Now it returns JSON correctly!
        }

        [NonAction]
        public async Task<DataTable> GetLoginTableData()
        {
            using (var connection = _context.Database.GetDbConnection()) // Get database connection
            {
                await connection.OpenAsync(); // Open connection
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Login"; // Fetch all columns

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader); // Load SQL data into DataTable
                        return dataTable;
                    }
                }
            }
        }
    }
}
