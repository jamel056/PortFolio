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
        private readonly IUnitOfWork<Address> _address;

        public HomeController(IUnitOfWork<Owner> owner, IUnitOfWork<PortFolioItem> portfolio
            , IUnitOfWork<Address> address)
        {
            _owner = owner;
            _portfolio = portfolio;
            _address = address;
        }
        public IActionResult Index()
        {
            var homeViewModel = new HomeVM()
            {
                Owner = _owner.Entity.GetAll().First(),
                PortFolioItems = _portfolio.Entity.GetAll().ToList(),
                Address = _address.Entity.GetAll().First()
            };
            return View(homeViewModel);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}