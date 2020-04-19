using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Web.ViewModels
{
    public class PortfolioVM : EntityBase
    {
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile File { get; set; }
    }
}
