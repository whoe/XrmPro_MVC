using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XrmPro_MVC.Services;
using XrmPro_MVC.Models;
using Microsoft.Extensions.DependencyInjection;
namespace XrmPro_MVC.Controllers
{
    public class StudentServiceController : Controller
    {

        private IRepositoryService repository;

        public StudentServiceController(IRepositoryService repository)
        {
            this.repository = repository;
        }

        [Route("StudentService")]
        public IActionResult Index()
        {
            return View(this.repository.LoadAll());
        }

        public IActionResult Create(StudentModel model, [FromServices] IRepositoryService repos)
        {
            if (ModelState.IsValid)
            {
                if (!repos.Save(model))
                    ViewData.Add("result", "That identifier already exists");
                else
                    ViewData.Add("result", "Success");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var repos = HttpContext
                .RequestServices
                .GetService<IRepositoryService>();

            var model = repos.Load(id);

            if (model == null)
                return Redirect("/StudentService");

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete()
        {
            return NotFound();
        }
        
        public IActionResult Details(int id)
        {
            var instance = ActivatorUtilities.CreateInstance<StudentModelContainer>(HttpContext.RequestServices, id);
            return View(instance.GetModel());
        }

        public IActionResult Edit(int id)
        {
            var modelFromController = repository.Load(id);
            return View(modelFromController);
        }

    }

    public class StudentModelContainer
    {
        private IRepositoryService repository;
        private int id;

        public StudentModelContainer(int id, IRepositoryService repository)
        {
            this.id = id;
            this.repository = repository;
        }

        public StudentModel GetModel()
        {
            return this.repository.Load(this.id);
        }
    }

}