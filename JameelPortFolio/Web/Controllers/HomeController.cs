using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Owner> _owner;
        private readonly IUnitOfWork<PortFolioItem> _portfolio;

        public HomeController(IUnitOfWork<Owner> owner, IUnitOfWork<PortFolioItem> portfolio)
        {
            _owner = owner;
            _portfolio = portfolio;
        }
        public IActionResult Index()
        {
            var homeViewModel = new HomeVM()
            {
                Owner = _owner.Entity.GetAll().First(),
                PortFolioItems = _portfolio.Entity.GetAll().ToList()
            };
            return View(homeViewModel);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}