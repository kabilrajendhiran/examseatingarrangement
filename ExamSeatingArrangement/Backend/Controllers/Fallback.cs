using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace ExamSeatingArrangement2020.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Fallback : ControllerBase
    {
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
        }
    }
}