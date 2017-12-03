using Microsoft.AspNetCore.Mvc;
using XrmPro_MVC.Models;
using System;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XrmPro_MVC.Controllers
{
    public class StudentController : Controller
    {
        [HttpPost]
        public IActionResult Create([FromBody] StudentModel model)
        {
            if (ModelState.IsValid && model.Create())
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
            if (ModelState.IsValid && model.Update())
                return Ok();
            else
                return BadRequest();
        }
        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = new StudentModel().Read(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit([FromForm] StudentModel model)
        {
            if (ModelState.IsValid)
            {
                model.Update();
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (StudentModel.Delete(id))
                return Ok();
            else
                return NotFound();

        }

        [Route("")]
        [Route("Student")]
        [Route("Student/All")]
        [HttpGet]
        public IActionResult ViewAll()
        {
           return View("All", StudentModel.ReadAll());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add([FromForm] StudentModel model)
        {
            if (!ModelState.IsValid || !model.Create())
            {
                return View(model);
            }

            return RedirectToAction("ViewAll");
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            StudentModel.Delete(id);
            return RedirectToAction("ViewAll");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var model = new StudentModel();
            model.Read(id);
            return View(model);
        }
    }
}
