using GymManagementBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GymManagementPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnalyticService _analyticsService;

        // Actions

        public HomeController(IAnalyticService analyticsService)
        {
            _analyticsService = analyticsService;
        }
        public ActionResult Index()
        {
            var Data = _analyticsService.GetAnalyticsData();
            return View(Data);
        }

    }
}