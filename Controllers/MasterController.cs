using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SIBSAPI.Data;
using SIBSAPI.Master;
using SIBSAPI.Services;
using System.Data;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SIBSAPI.Controllers
{
    public class MasterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly MasterMethod _masterMethod;


        public MasterController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _masterMethod = new MasterMethod(context, configuration);

        }














        //[HttpGet("GetDepartment")]
        //public async Task<IActionResult> GetDepartmentsAsync()
        //{
        //    try
        //    {
        //        var departments = await _context.Database
        //            .SqlQueryRaw<(string dept_name, string dept_code)>("SELECT [DESC] AS dept_name, DEPT_CODE FROM DEPT")
        //            .ToListAsync();

        //        if (departments == null || !departments.Any()) // ✅ Prevents error
        //        {
        //            return NotFound(new { message = "No departments found" });
        //        }

        //        return Ok(new
        //        {
        //            StatusCode = 200,
        //            Message = "Departments retrieved successfully",
        //            Data = departments
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new
        //        {
        //            Message = "An error occurred while fetching departments",
        //            Error = ex.Message
        //        });
        //    }
        //}


        [HttpGet("Fetch-Department")]
        public async Task<IActionResult> FetchDepartment()
        {
            try
            {
                string query = "SELECT [DESC] AS dept_name, DEPT_CODE FROM DEPT";
                var dataTable = await _masterMethod.GetLoginTableData(query);
                var jsonResult = _masterMethod.ConvertDataTableToJson(dataTable);
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Departments retrieved successfully",
                    Data = jsonResult
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An error occurred while fetching Departments",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("Fetch-Grade")]
        public async Task<IActionResult> FetchGrade()
        {
            try
            {
                string query = "SELECT [grade_desc],[grade] FROM GRADE_mst";
                var dataTable = await _masterMethod.GetLoginTableData(query);
                var jsonResult = _masterMethod.ConvertDataTableToJson(dataTable);
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Grade retrieved successfully",
                    Data = jsonResult
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An error occurred while fetching Grade",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("Fetch-Designation")]
        public async Task<IActionResult> FetchDesignation()
        {
            try
            {
                string query = "SELECT [desig_desc],[desig_code] FROM DESIG_MST";
                var dataTable = await _masterMethod.GetLoginTableData(query);
                var jsonResult = _masterMethod.ConvertDataTableToJson(dataTable);
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Designation retrieved successfully",
                    Data = jsonResult
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An error occurred while fetching Designation",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("Fetch-ReferByReporting")]
        public async Task<IActionResult> FetchReferByAndReport(string NameORCode)
        {
            try
            {
                //string query = "SELECT [emp_no],[fname] +'  '+ [mname]+'  '+[lname] as Name FROM Personal_MST";               

                string query = @"SELECT emp_no, fname + ' ' + mname + ' ' + lname AS Name
                                 FROM Personal_MST
                                 WHERE emp_no LIKE '%" + NameORCode + @"%'
                                    OR fname LIKE '%" + NameORCode + @"%'
                                    OR mname LIKE '%" + NameORCode + @"%'
                                    OR lname LIKE '%" + NameORCode + @"%'";


                var dataTable = await _masterMethod.GetLoginTableData(query);
                var jsonResult = _masterMethod.ConvertDataTableToJson(dataTable);
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Employees retrieved successfully",
                    Data = jsonResult
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An error occurred while fetching Employees",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("Fetch-Location")]
        public async Task<IActionResult> FetchLocation()
        {
            try
            {
                string query = "SELECT [loc_code],[Description] FROM Location_mst";
                var dataTable = await _masterMethod.GetLoginTableData(query);
                var jsonResult = _masterMethod.ConvertDataTableToJson(dataTable);
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Location retrieved successfully",
                    Data = jsonResult
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An error occurred while fetching Location",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("Fetch-Project")]
        public async Task<IActionResult> FetchProject()
        {
            try
            {
                string query = "SELECT [Desc],[PROJ_CODE] from project";
                var dataTable = await _masterMethod.GetLoginTableData(query);
                var jsonResult = _masterMethod.ConvertDataTableToJson(dataTable);
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Project retrieved successfully",
                    Data = jsonResult
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An error occurred while fetching Project",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("Fetch-Union")]
        public async Task<IActionResult> FetchUnion()
        {
            try
            {
                string query = "SELECT [Union_desc],[Union_Code] from Union_Mst";
                var dataTable = await _masterMethod.GetLoginTableData(query);
                var jsonResult = _masterMethod.ConvertDataTableToJson(dataTable);
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Union retrieved successfully",
                    Data = jsonResult
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An error occurred while fetching Union",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("Fetch-State")]
        public async Task<IActionResult> FetchState()
        {
            try
            {
                string query = "SELECT [State],[state_code],[state_cd] from STATE_MST";
                var dataTable = await _masterMethod.GetLoginTableData(query);
                var jsonResult = _masterMethod.ConvertDataTableToJson(dataTable);
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "State retrieved successfully",
                    Data = jsonResult
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An error occurred while fetching State",
                    Error = ex.Message
                });
            }
        }


        //employees 
        [HttpGet("Fetch-Employees-dropdown")]
        public async Task<IActionResult> FetchEmployeesDrodown()
        {
            try
            {
                string query = @"SELECT 
    emp_no, 
    fname, 
    mname, 
    lname, 
    dept_code, 
	desig_code,
	grade,
	[status],
	[profcen_cd]
	FROM Personal_MST 
	where status <> 'L'";


                var dataTable = await _masterMethod.GetLoginTableData(query);
                var jsonResult = _masterMethod.ConvertDataTableToJson(dataTable);
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Employees Drp retrieved successfully",
                    Data = jsonResult
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "An error occurred while fetching Employees drp",
                    Error = ex.Message
                });
            }
        }


        [HttpGet("Fetch-Period")]
        public async Task<IActionResult> FetchPeriodAsync(int Profcen_cd)
        {
            //var data = await _masterRepository.FetchPeriodAsync(Profcen_cd);
            string query = $"select top 1 PERIOD from PAY_CLOSE where profcen_cd='{Profcen_cd}' Order by DATE DESC";

            var data = await _masterMethod.GetLoginTableData(query);
            var jsonResult = _masterMethod.ConvertDataTableToJson(data);

            var firstItem = jsonResult?.FirstOrDefault();
            int PeriodV = 0; // Default value
            int daysInMonth = 0; // Get days of period

            //increment period
            if (firstItem != null && firstItem.TryGetValue("PERIOD", out var periodValue) && int.TryParse(periodValue?.ToString(), out int period))
            {
                PeriodV = period;

                // Extract month and year
                int month = PeriodV / 10000;  // First two digits represent the month
                int year = PeriodV % 10000;   // Last four digits represent the year

                // Increment month
                month++;

                // If month exceeds 12, reset to 1 and increment the year
                if (month > 12)
                {
                    month = 1;
                    year++;
                }

                // Construct the new PeriodV
                PeriodV = (month * 10000) + year;
                daysInMonth = DateTime.DaysInMonth(year, month);
            }

            //calculate period days


            return Ok(new { StatusCode = 200, Message = "Period retrieved succesfully", Period = PeriodV, Days = daysInMonth });
        }




    }
}
