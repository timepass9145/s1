using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIBSAPI.Data;
using SIBSAPI.Master;
using SIBSAPI.Models;
using SIBSAPI.Services;

namespace SIBSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationIncomeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly MasterMethod _masterMethod;
        public ApplicationIncomeController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _masterMethod = new MasterMethod(context, configuration);
        }


        [HttpPost("add-Application-income")]
        public async Task<IActionResult> AddApplication([FromBody] ApplicationIncome ApplicationIncome)
        {

            try
            {
                if (ApplicationIncome == null)
                    return BadRequest("Invalid Application data.");


                string query = "select Profcen_Cd from Applicable_Incomes where Profcen_Cd = '" + ApplicationIncome.Profcen_Cd + "' ";
                var dataTable = await _masterMethod.GetLoginTableData(query);

                if (dataTable.Rows.Count > 0)
                {
                    _context.ApplicationIncome.Update(ApplicationIncome);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Application Updated successfully!", Application_profecd = ApplicationIncome.Profcen_Cd });
                }
                else
                {
                    _context.ApplicationIncome.Add(ApplicationIncome);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Application added successfully!", Application_profecd = ApplicationIncome.Profcen_Cd });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Errormessage = "*** On Application Saving ***" + ex.Message });
            }
        }


        [HttpDelete("Delete-Application-income")]
        public async Task<IActionResult> deleteApplication([FromBody] ApplicationIncome ApplicationIncome)
        {
            try
            {
                string query = "select Profcen_Cd from Applicable_Incomes where Profcen_Cd = '" + ApplicationIncome.Profcen_Cd + "' ";
                var dataTable = await _masterMethod.GetLoginTableData(query);

                if (dataTable.Rows.Count > 0)
                {
                    _context.ApplicationIncome.Remove(ApplicationIncome);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Application Deleted successfully!", Application_profecd = ApplicationIncome.Profcen_Cd });
                }
                else
                {
                    return Ok(new { Errormessage = "*** Application Delete Not Found ***" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Errormessage = "*** On Application Saving ***" + ex.Message });
            }
        }






    }
}
