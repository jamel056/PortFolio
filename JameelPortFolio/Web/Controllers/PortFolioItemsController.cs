using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using Web.ViewModels;

namespace Web.Controllers
{
    public class PortFolioItemsController : Controller
    {
        private readonly IUnitOfWork<PortFolioItem> _portFolio;
        private readonly IHostingEnvironment _hosting;

        public PortFolioItemsController(IUnitOfWork<PortFolioItem> portFolio, IHostingEnvironment hosting)
        {
            _portFolio = portFolio;
            _hosting = hosting;
        }

        // GET: PortFolioItems
        public IActionResult Index()
        {
            return View(_portFolio.Entity.GetAll());
        }

        // GET: PortFolioItems/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portFolioItem = _portFolio.Entity.GetById(id);
            if (portFolioItem == null)
            {
                return NotFound();
            }

            return View(portFolioItem);
        }

        // GET: PortFolioItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PortFolioItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PortfolioVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.File != null)
                {
                    string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                    string fullPath = Path.Combine(uploads, model.File.FileName);
                    model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                }
                PortFolioItem portFolioItem = new PortFolioItem
                {
                    ProjectName = model.ProjectName,
                    Description = model.Description,
                    ImageUrl = model.File.FileName
                };
                _portFolio.Entity.Insert(portFolioItem);
                _portFolio.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PortFolioItems/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id != null)
            {
                return NotFound();
            }

            var portFolioItem = _portFolio.Entity.GetById(id);
            if (portFolioItem != null)
            {
                return NotFound();
            }

            PortfolioVM portfolio = new PortfolioVM
            {
                Id = portFolioItem.Id,
                Description = portFolioItem.Description,
                ImageUrl = portFolioItem.ImageUrl,
                ProjectName = portFolioItem.ProjectName
            };

            return View(portfolio);
        }

        // POST: PortFolioItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, PortfolioVM model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.File != null)
                    {
                        string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                        string fullPath = Path.Combine(uploads, model.File.FileName);
                        model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }
                    PortFolioItem portFolioItem = new PortFolioItem
                    {
                        Id = model.Id,
                        ProjectName = model.ProjectName,
                        Description = model.Description,
                        ImageUrl = model.File.FileName
                    };
                    _portFolio.Entity.Update(portFolioItem);
                    _portFolio.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortFolioItemExists(model.Id))
                    {

                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PortFolioItems/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portFolioItem = _portFolio.Entity.GetById(id);
            if (portFolioItem == null)
            {
                return NotFound();
            }

            return View(portFolioItem);
        }

        // POST: PortFolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var portFolioItem = _portFolio.Entity.GetById(id);
            _portFolio.Entity.Delete(portFolioItem);
            _portFolio.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PortFolioItemExists(Guid id)
        {
            return _portFolio.Entity.GetAll().Any(e => e.Id == id);
        }
    }
}
