using System.Linq;
using System.Resources;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using LegacyCRM.Core;
using LegacyCRM.Core.Resources;
using LegacyCRM.Web.Models.ViewModels;

namespace LegacyCRM.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly CrmDbContext _context = new CrmDbContext();

        [OutputCache(Duration = 300, VaryByParam = "none")]
        public ActionResult Index()
        {
            var objectContext = ((IObjectContextAdapter)_context).ObjectContext;
            objectContext.CommandTimeout = 60;

            var resourceManager = new ResourceManager("LegacyCRM.Core.Resources.Strings", typeof(Strings).Assembly);
            var model = new DashboardViewModel
            {
                WelcomeMessage = resourceManager.GetString("WelcomeMessage") ?? "Welcome to LegacyCRM",
                TotalCustomers = _context.Customers.Count(),
                OpenOpportunities = _context.Opportunities.Count(o => o.Stage != "ClosedWon" && o.Stage != "ClosedLost"),
                OpenTickets = _context.Tickets.Count(t => t.Status != "Closed"),
                RecentCustomers = _context.Customers.OrderByDescending(c => c.CreatedAt).Take(10).ToList(),
                RecentTickets = _context.Tickets.Include(t => t.Customer).OrderByDescending(t => t.CreatedAt).Take(5).ToList(),
                PipelineSummary = _context.Opportunities.GroupBy(o => o.Stage).ToDictionary(g => g.Key, g => g.Sum(x => x.Value))
            };

            ViewBag.CommandTimeout = objectContext.CommandTimeout;
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
