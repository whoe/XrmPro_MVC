using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using XrmPro_MVC.Context;
using XrmPro_MVC.Models;

namespace XrmPro_MVC.Controllers
{
    public class StudentEFController : Controller
    {
        private readonly StudentEFContext _context;

        public StudentEFController(StudentEFContext context)
        {
            _context = context;
        }

        // GET: StudentEFModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudentEFModel.ToListAsync());
        }

        // GET: StudentEFModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentEFModel = await _context.StudentEFModel
                .SingleOrDefaultAsync(m => m.Id == id);
            if (studentEFModel == null)
            {
                return NotFound();
            }

            return View(studentEFModel);
        }

        // GET: StudentEFModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentEFModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Git")] StudentEFModel studentEFModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentEFModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentEFModel);
        }

        // GET: StudentEFModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentEFModel = await _context.StudentEFModel.SingleOrDefaultAsync(m => m.Id == id);
            if (studentEFModel == null)
            {
                return NotFound();
            }
            return View(studentEFModel);
        }

        // POST: StudentEFModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Git")] StudentEFModel studentEFModel)
        {
            if (id != studentEFModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentEFModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentEFModelExists(studentEFModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(studentEFModel);
        }

        // GET: StudentEFModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentEFModel = await _context.StudentEFModel
                .SingleOrDefaultAsync(m => m.Id == id);
            if (studentEFModel == null)
            {
                return NotFound();
            }

            return View(studentEFModel);
        }

        // POST: StudentEFModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentEFModel = await _context.StudentEFModel.SingleOrDefaultAsync(m => m.Id == id);
            _context.StudentEFModel.Remove(studentEFModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentEFModelExists(int id)
        {
            return _context.StudentEFModel.Any(e => e.Id == id);
        }
    }
}
