using Microsoft.AspNetCore.Mvc;
using PlayForDays.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayForDays.Controllers
{
    public class BrowseController : Controller
    {
        //DB Context connection object
        private readonly ApplicationDbContext _context;

        public BrowseController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
