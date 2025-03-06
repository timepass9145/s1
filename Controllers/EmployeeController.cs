using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SIBSAPI.Data;
using SIBSAPI.DTOs;
using SIBSAPI.Master;
using SIBSAPI.Models;
using SIBSAPI.Services;

namespace SIBSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        
        private readonly ApplicationDbContext _context; private readonly MasterMethod _masterMethod;


        public EmployeeController(ApplicationDbContext context, IConfiguration configuration)
        {
           
            _context = context;
            _masterMethod = new MasterMethod(context, configuration);

        }

        [HttpPost("add-employee")]
        public async Task<IActionResult> AddEmployee([FromBody] PersonalMst employee)
        {
            try
            {
                if (employee == null)
                    return BadRequest("Invalid employee data.");

                _context.PersonalMst.Add(employee);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Employee added successfully!", EmpNo = employee.emp_no });

            }
            catch (Exception ex)
            {
                return Ok(new { Errormessage = "*** On Employee Saving ***" + ex.Message  });                
            }
        }




      





    }
}
