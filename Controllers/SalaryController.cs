using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SIBSAPI.Data;
using SIBSAPI.Master;
using SIBSAPI.Models;
using System.Data;
using System.Text.Json;
using System.Xml;

namespace SIBSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly MasterMethod _masterMethod;
        private readonly string _connectionString;



        public SalaryController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _masterMethod = new MasterMethod(context, configuration);

            _connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new Exception("Database connection string is missing or not configured.");
            }

        }


        [HttpGet("SalaryDetails-Pagination")]
        public async Task<IActionResult> GetSalaryDetailsPagination()
        {
            try
            {
                string query = @"SELECT  top 10  fname,mname,lname,gender FROM Personal_MST WHERE Salary_Det = 'Y' And Status <> 'L'";


                var dataTable = await _masterMethod.GetLoginTableData(query);
                var jsonResult = _masterMethod.ConvertDataTableToJson(dataTable);
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Paginaton of Salary Wages retrieved successfully",
                    Data = jsonResult
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 200,
                    Message = "Error By Pagination of Salary Wages Api",
                    Error = "###  " + ex.Message + "  ###"
                });

            }

        }

        [HttpGet("FetchLabels-SalaryDetails")]
        public async Task<IActionResult> GetFetchLabels_SalaryDetails(int Profcen_Code)
        {
            try
            {
                string query = $@"SELECT TOP 1 * FROM Applicable_Incomes WHERE Profcen_cd = {Profcen_Code}";
                var dataTable = await _masterMethod.GetLoginTableData(query);

                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "No data found for the given Profcen_Code."
                    });
                }

                // Convert DataTable row into a Dictionary with column names as keys
                var resultData = dataTable.Columns.Cast<DataColumn>()
                                .Where(col => col.DataType != typeof(Type))  // Exclude System.Type fields
                                .ToDictionary(col => col.ColumnName, col => dataTable.Rows[0][col])
                                .Where(kvp => !(kvp.Value is string str && string.IsNullOrWhiteSpace(str)) &&
                                              !(kvp.Value is bool boolVal && boolVal == false) // Remove false values

                                ) // Remove empty string values
                                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Labels of Salary Wages retrieved successfully",
                    Data = resultData
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Message = "Error fetching Labels of Salary Wages API",
                    Error = $"### {ex.Message} ###"
                });
            }
        }

        //Which is Y in personal_mst with Slip and voucher
        [HttpGet("FetchYEmployees-SalaryDetails")]
        public async Task<IActionResult> FetchYEmp_SalaryWages()
        {
            try
            {
//                string query = @"SELECT 
                                    //    P.emp_no, 
                                    //    P.fname, 
                                    //    P.mname, 
                                    //    P.lname, 
                                    //    P.dept_code, 
                                    //	P.desig_code,
                                    //	P.grade,
                                    //	P.[status],
                                    //	P.[profcen_cd],
                                    //    D.[DESC] as Department_Name,
                                    //	Ds.desig_desc as Designation_Name,
                                    //    Dr.grade_desc as Grade_Name
                                    //FROM Personal_MST P
                                    //LEFT JOIN DEPT D ON D.DEPT_CODE = P.dept_code
                                    //LEFT JOIN DESIG_MST Ds ON Ds.DESIG_CODE = P.desig_code
                                    //LEFT JOIN GRADE_MST Dr ON Dr.grade = P.Grade
                                    //WHERE P.Salary_Det <> 'Y' And P.Status <> 'L';";

                string query = @"SELECT    P.emp_no,     P.fname,     P.mname,     P.lname,     P.dept_code, 	P.desig_code,	P.grade,	P.[status],	P.[profcen_cd],    D.[DESC] as Department_Name,
	                                Ds.desig_desc as Designation_Name,    Dr.grade_desc as Grade_Name,

                                    PY.basic,PY.fix_basic,PY.DA,PY.var_DA,PY.conv_allow,PY.LTA,PY.medical_allow,PY.child_edu,PY.uniform,PY.HRA,PY.misc1,PY.misc2,PY.misc3,PY.misc4,PY.magazine,
                                    PY.canteen,PY.driver,PY.guest,PY.soft_furnishing,PY.soft_furnishing
	                                
                                FROM Personal_MST P
                                LEFT JOIN DEPT D ON D.DEPT_CODE = P.dept_code
                                LEFT JOIN DESIG_MST Ds ON Ds.DESIG_CODE = P.desig_code
                                LEFT JOIN GRADE_MST Dr ON Dr.grade = P.Grade
                                LEFT JOIN PAY_MASTER PY ON PY.grade = P.Grade
                                WHERE P.Salary_Det <> 'Y' And P.Status <> 'L';";
                
              
                var dataTable = await _masterMethod.GetLoginTableData(query);
                var jsonResult = _masterMethod.ConvertDataTableToJson(dataTable);
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Y Emp with Pay Master retrieved successfully",
                    Data = jsonResult
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 200,
                    Message = "Error By FetchYEmployees Api",
                    Error = "###  "+ ex.Message+ "  ###"
                });

            }

        }


        //[HttpGet("FetchLabels-SalaryDetails")]
        //public async Task<IActionResult> GetFetchLabels_SalaryDetails(int Profcen_Code)
        //{
        //    try
        //    {
        //        string query = @"SELECT Top 1 *  FROM Applicable_Incomes where Profcen_cd='" + Profcen_Code + "'";
        //        var dataTable = await _masterMethod.GetLoginTableData(query);





        //        var filteredData = dataTable?.GetType().GetProperties()
        //                          .Where(p => p.GetValue(dataTable)?.ToString() != "")
        //                          .ToDictionary(p => p.Name, p => p.GetValue(dataTable));

        //        DataTable dt = new DataTable();
        //        dt.Columns.Add("Column", typeof(string));
        //        dt.Columns.Add("Value", typeof(object));
        //        foreach (var kvp in filteredData)
        //        {
        //            dt.Rows.Add(kvp.Key, kvp.Value);
        //        }



        //        var d1 = _masterMethod.ConvertDataTableToJson(dt);
        //        var jsonResult = _masterMethod.ConvertDataTableToJson(dataTable);
        //        return Ok(new
        //        {
        //            StatusCode = 200,
        //            Message = "Labels of Salary Wages retrieved successfully",
        //            Data = jsonResult
        //        });

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new
        //        {
        //            StatusCode = 200,
        //            Message = "Error By Labels of Salary Wages Api",
        //            Error = "###  " + ex.Message + "  ###"
        //        });

        //    }

        //}

        [HttpPost("add-Salary-Details")]
        public async Task<IActionResult> AddSalary([FromBody] EmpMst salaryDetails)
        {
            string Employee_sel_det = "";

            try
            {
                if (salaryDetails == null)
                    return BadRequest("*** Invalid SalaryDatails Data (Null). *** ");

                _context.EmpMst.Add(salaryDetails);
                var Results = await _context.SaveChangesAsync();
                Employee_sel_det += "* --- 01 = Salary Details Added Successfully ----* ";


                if (Results != 0)
                {
                   var existingEmployee =  await _context.PersonalMst.FindAsync(salaryDetails.EMP_NO); 
                    if (existingEmployee != null)
                    {
                        existingEmployee.Salary_Det = "Y";
                        _context.PersonalMst.Update(existingEmployee);
                        await _context.SaveChangesAsync();
                        Employee_sel_det += "* --- 02 = Emp Y And Salary Details Added Successfully ----* ";
                        
                    }
                    else
                    {
                        Employee_sel_det += "*** Employee Y det not updated employee Not Found. *** ";
                    }
                }
                return Ok(new { message = "* Salary Details Added Successfully!", EmpNo = salaryDetails.EMP_NO, DetailsApi = Employee_sel_det });
            }
            catch (Exception ex)
            {
                var Check = "";
                if (ex.HResult == -2146233088)
                {
                    Check = " '"+ salaryDetails.EMP_NO + "' is already exist in database.";
                }
                return Ok(new { Errormessage = "*** On Salary Details Saving ***  " + ex.Message, DetailsApi = Employee_sel_det, MSG = Check });
            }
        }

        [HttpPost("GetUpdate-SalaryWagesDetails")]
        public async Task<IActionResult> GetUpdateSalaryWagesDetails(string EMP_NO)
        {

            try
            {
                var dataTable = _context.EmpMst.AsNoTracking().FirstOrDefault(x => x.EMP_NO.Equals(EMP_NO));

                if (dataTable == null)
                    return BadRequest("Invalid SalaryWages data Not Found.");
                //var jsonResult = _masterMethod.ConvertDataTableToJson(dataTable);
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "SalaryWages update data retrieved successfully",
                    Data = dataTable
                });

            }
            catch (Exception ex)
            {
                return Ok(new { Errormessage = "*** Error On SalaryWages update Data fetch ***" + ex.Message });
            }
        }
    }
}
