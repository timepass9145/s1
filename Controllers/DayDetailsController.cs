using Microsoft.AspNetCore.Mvc;
using SIBSAPI.Data;
using SIBSAPI.Master;
using SIBSAPI.Models;

namespace SIBSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DayDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly MasterMethod _masterMethod;
        public DayDetailsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _masterMethod = new MasterMethod(context, configuration);
        }


        [HttpPost("add-DayDetails")]
        public async Task<IActionResult> AddDayDetails([FromBody] DAYS_MST DAYS_MST)
        {

            try
            {
                if (DAYS_MST == null)
                    return BadRequest("Invalid Application data.");


                string query = $@"select emp_no from DAYS_MST where emp_no = '{DAYS_MST.emp_no}'  And period='{DAYS_MST.period}'";
                var dataTable = await _masterMethod.GetLoginTableData(query);

                if (dataTable.Rows.Count > 0)
                {
                    _context.DAYS_MST.Update(DAYS_MST);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "DAYS_MST Updated successfully!", Application_profecd = DAYS_MST.emp_no });
                }
                else
                {
                    _context.DAYS_MST.Add(DAYS_MST);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "DAYS_MST added successfully!", Application_profecd = DAYS_MST.emp_no });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Errormessage = "*** On DAYS_MST Saving ***" + ex.Message });
            }
        }

        //employees DaysDetail
        [HttpGet("Fetch-DayDetails-Employees")]
        public async Task<IActionResult> FetchEmployeesDrodown(string Period, string profcen_cd)
        {
            try
            {
                string year = Period.Substring(2, 4);  // Extract last 4 digits
                //string month = Period.Substring(0, 2);  // first 2 digits



                string query = $@"SELECT 
                                                P.emp_no, 
                                                P.fname, 
                                                P.mname, 
                                                P.lname, 
                                                P.dept_code, 
                                                P.desig_code,
                                                P.grade,
                                                P.[status],
                                                P.[profcen_cd],L.CLBAL as Casual_Balance, L.SLBAL as SickLeave , L.PLBAL as Paid_leave 
                                            FROM Personal_MST P


                                            LEFT JOIN (  
                                            SELECT EMP_NO,(OP_CL+CL)-(CL_TAKEN+ENCASH_CL+ADVANCE_CL+CL_TEMP) AS CLBAL,
                                            (OP_SL+SL)-(SL_TAKEN+ENCASH_SL+ADVANCE_SL+SL_TEMP) AS SLBAL,
                                            (OP_PL+PL)-(PL_TAKEN+ENCASH_PL+ADVANCE_PL+PL_TEMP) AS PLBAL  
                                            FROM LEAVE_MST WHERE PEROID='{year}' 

                                            ) L
                                            ON P.emp_no = L.emp_no




                                            WHERE NOT EXISTS (
                                                SELECT 1 
                                                FROM DAYS_MST D 
                                                WHERE P.emp_no = D.emp_no 
                                                AND D.period = '{Period}' AND D.profcen_cd = '1'     )
                                            AND P.[status] <> 'L' 
                                            AND P.profcen_cd = '{profcen_cd}';
                                            ";



                var dataTable = await _masterMethod.GetLoginTableData(query);
                var jsonResult = _masterMethod.ConvertDataTableToJson(dataTable);

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Employees DayDetails Help retrieved successfully",
                    Data = jsonResult
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An error occurred while fetching Employees DayDetails help",
                    Error = ex.Message
                });
            }
        }


        [HttpGet("Fetch-PublicHoliday-Count")]
        public async Task<IActionResult> FetchPublicHoliday(string Period)
        {
            try
            {
                string year = Period.Substring(2, 4);  // Extract last 4 digits
                string month = Period.Substring(0, 2);  // first 2 digits

                //// Get Public Holiday 
                string PHolidayQuery = $@"SELECT COUNT(*) AS PublicHoliday
                                FROM Holiday_master
                                WHERE YEAR(holiday_date) = '{year}'
                                AND MONTH(holiday_date) = '{month}'
                                AND [ph_flag] = 'Y';";
                var GetPublicHoliday = await _masterMethod.GetLoginTableData(PHolidayQuery);
                var GetPublicHolidayJson = _masterMethod.ConvertDataTableToJson(GetPublicHoliday);
                
                
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Public Holiday Count retrieved successfully",
                    Data = GetPublicHolidayJson
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An error occurred while fetching Public Holiday help",
                    Error = ex.Message
                });
            }

        }

        [HttpGet("Fetch-weekoff-Daydetails")]
        public async Task<IActionResult> FetchWeekOff(string Period, string EmpNo)
        {
            try
            {
                string year = Period.Substring(2, 4);  // Extract last 4 digits
                string month = Period.Substring(0, 2);  // first 2 digits

                string query = $@"  DECLARE @Year INT = {year};
                                DECLARE @Month INT = {month};
                                DECLARE @WeekOff1 VARCHAR(20);
                                DECLARE @WeekOff2 VARCHAR(20);

                                -- Get the week off days for the given employee
                                SELECT @WeekOff1 = week_off, @WeekOff2 = week_off1 
                                FROM PERSONAL_MST 
                                WHERE emp_no = '{EmpNo}';

                                -- Count occurrences of those week-off days in the given month
                                WITH Dates AS (
                                    SELECT DATEFROMPARTS(@Year, @Month, 1) AS HolidayDate
                                    UNION ALL
                                    SELECT DATEADD(DAY, 1, HolidayDate)
                                    FROM Dates
                                    WHERE MONTH(DATEADD(DAY, 1, HolidayDate)) = @Month
                                )
                                SELECT COUNT(*) AS TotalWeekOffs
                                FROM Dates
                                WHERE 
                                    (DATENAME(WEEKDAY, HolidayDate) = @WeekOff1 OR 
                                     DATENAME(WEEKDAY, HolidayDate) = @WeekOff2)
                                OPTION (MAXRECURSION 31);";

                var GetWeekOff = await _masterMethod.GetLoginTableData(query);
                var WeekOffJson = _masterMethod.ConvertDataTableToJson(GetWeekOff);

                return Accepted(new
                {
                    StatusCode = 200,
                    Message = "Public Holiday Count retrieved successfully",
                    WeekOffJson
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An error occurred while fetching Employees DayDetails help",
                    Error = ex.Message
                });
            }
            
        }






    }
}



