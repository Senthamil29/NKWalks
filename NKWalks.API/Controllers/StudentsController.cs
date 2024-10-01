using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NKWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] student = new string[] { "Pooja", "Yakindra", "Munib", "Jyothi" };

            return Ok(student);
        }
    }
}
