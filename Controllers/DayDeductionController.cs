using Microsoft.AspNetCore.Mvc;
using SIBSAPI.Data;
using SIBSAPI.Master;
using SIBSAPI.Models;

namespace SIBSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DayDeductionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly MasterMethod _masterMethod;

        public DayDeductionController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _masterMethod = new MasterMethod(context, configuration);
        }


        [HttpPost("add-DayDeduction")]
        public async Task<IActionResult> AddDayDeduction([FromBody] Deduction Deduction)
        {
            try
            {
                if (Deduction == null)
                    return BadRequest("Invalid Application data.");


                string query = $@"select emp_no from Deduction where emp_no = '{Deduction.emp_no}'  And period='{Deduction.period}'";
                var dataTable = await _masterMethod.GetLoginTableData(query);

                if (dataTable.Rows.Count > 0)
                {
                    _context.Deduction.Update(Deduction);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Deduction Updated successfully!", Deduction = Deduction.emp_no });
                }
                else
                {
                    _context.Deduction.Add(Deduction);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Deduction added successfully!", Deduction = Deduction.emp_no });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Errormessage = "*** On Deduction Saving ***" + ex.Message });
            }
        }


        [HttpGet("Fetch-DayDeduction-Employees")]
        public async Task<IActionResult> FetchEmployeesDrodown(string Period, string profcen_cd)
        {
            try
            {
                string year = Period.Substring(2, 4);  // Extract last 4 digits

                string query = $@"SELECT 
                                                P.emp_no, 
                                                P.fname, 
                                                P.mname, 
                                                P.lname, 
                                                P.dept_code, 
                                                P.desig_code,
                                                P.grade,
                                                P.[status],
                                                P.[profcen_cd]
                                            FROM Personal_MST P
                                            WHERE NOT EXISTS (
                                                SELECT 1 
                                                FROM DEDUCTION D 
                                                WHERE P.emp_no = D.emp_no 
                                                AND D.period = '{Period}' 
                                                AND D.profcen_cd = '1'
                                                )
                                            AND P.[status] <> 'L' 
                                            AND P.profcen_cd = '{profcen_cd}';
                                            ";



                var dataTable = await _masterMethod.GetLoginTableData(query);
                var jsonResult = _masterMethod.ConvertDataTableToJson(dataTable);

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Employees DayDeduction Help retrieved successfully",
                    Data = jsonResult
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An error occurred while fetching Employees DayDeduction help",
                    Error = ex.Message
                });
            }
        }
    }
}
