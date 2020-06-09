using System.Threading.Tasks;
using InsideAirbnbApp.Repositories;
using InsideAirbnbApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsideAirbnbApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRepository<NeighbourhoodsViewModel> _repo;

        public AdminController(IRepository<NeighbourhoodsViewModel> repo)
        {
            _repo = repo;
        }
        [Authorize (Policy = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _repo.All().ToListAsync());
        }
    }
}
