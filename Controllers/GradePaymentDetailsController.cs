using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIBSAPI.Data;
using SIBSAPI.Master;
using SIBSAPI.Models;

namespace SIBSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradePaymentDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly MasterMethod _masterMethod;


        public GradePaymentDetailsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _masterMethod = new MasterMethod(context, configuration);

        }


        [HttpGet("Fetch-Grade-PaymentDetails")]
        public async Task<IActionResult> FetchGradePaymentDetails()
        {
            try
            {
                string query = @"SELECT G.grade 
                                    FROM GRADE_MST G
                                    LEFT JOIN PAY_MASTER P ON G.grade = P.grade
                                    WHERE P.grade IS NULL;
                                    ";
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


        [HttpPost("add-PaymentDetails-GradeWise")]
        public async Task<IActionResult> AddPaymentDetailsGradeWise([FromBody] PAY_MASTER PAYMASTER)
        {

            try
            {
                if (PAYMASTER == null)
                    return BadRequest("Invalid PaymentDetails data.");


                string query = "select Grade from PAY_MASTER where Grade = '" + PAYMASTER.grade + "' ";
                var dataTable = await _masterMethod.GetLoginTableData(query);

                if (dataTable.Rows.Count > 0)
                {
                    _context.PAY_MASTER.Update(PAYMASTER);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "PaymentDetails Updated successfully!", PaymentDetails = PAYMASTER.grade });
                }
                else
                {
                    _context.PAY_MASTER.Add(PAYMASTER);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "PaymentDetails added successfully!", PaymentDetails = PAYMASTER.grade });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Errormessage = "*** Error On PaymentDetails Saving ***" + ex.Message });
            }
        }



        [HttpDelete("Delete-PaymentDetails-GradeWise")]
        public async Task<IActionResult> deletePaymentDetails([FromBody] PAY_MASTER PAY_MASTER)
        {
                    var dataTable = _context.PAY_MASTER.AsNoTracking().FirstOrDefault(x=>x.grade.Equals(PAY_MASTER.grade));
            try
            {
                //string query = "select grade from PAY_MASTER where grade = '" + PAY_MASTER.grade + "' ";
                //var dataTable = await _masterMethod.GetLoginTableData(query);

                if (dataTable != null)
                {
                    _context.PAY_MASTER.Remove(PAY_MASTER);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "PAY_MASTER Deleted successfully!", PAY_MASTER = PAY_MASTER.grade });
                }
                else
                {
                    return Ok(new { Errormessage = "*** PAY_MASTER Delete Not Found ***" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Errormessage = "*** Error On PAY_MASTER Saving ***" + ex.Message });
            }
        }


        [HttpPost("GetUpdate-PaymentDetails-GradeWise")]
        public async Task<IActionResult> GetUpdatePaymentDetailsGradeWise(string grade)
        {

            try
            {
                var dataTable = _context.PAY_MASTER.AsNoTracking().FirstOrDefault(x => x.grade.Equals(grade));

                if (dataTable == null)
                    return BadRequest("Invalid PaymentDetails data Not Found.");
                             //var jsonResult = _masterMethod.ConvertDataTableToJson(dataTable);
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "PaymentDetails update data retrieved successfully",
                    Data = dataTable
                });

            }
            catch (Exception ex)
            {
                return Ok(new { Errormessage = "*** Error On PaymentDetails update Data fetch ***" + ex.Message });
            }
        }







    }
}
