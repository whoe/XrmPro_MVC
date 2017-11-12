using Microsoft.AspNetCore.Mvc;
using XrmPro_MVC.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XrmPro_MVC.Controllers
{
    public class StudentController : Controller
    {
        // GET: /<controller>/
        [HttpPost]
        public IActionResult Create([FromBody] StudentModel model)
        {
            if (model.Create())
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet]
        public IActionResult Read(int id)
        {
            var model = new StudentModel();
            var student = model.Read(id);
            if (student == null)
                return NotFound();
            return Json(student);
        }

        [HttpPost]
        public IActionResult Update([FromBody] StudentModel model)
        {
            if (model.Update())
                return Ok();
            else
                return BadRequest();
        }

        
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = new StudentModel();
            if (model.Delete(id))
                return Ok();
            else
                return NotFound();

        }
    }
}
