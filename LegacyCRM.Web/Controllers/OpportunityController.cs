using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using LegacyCRM.Core;
using LegacyCRM.Core.Models;

namespace LegacyCRM.Web.Controllers
{
    [Authorize]
    public class OpportunityController : Controller
    {
        private readonly CrmDbContext _context = new CrmDbContext();

        public ActionResult Index()
        {
            var opportunities = _context.Opportunities.Include(o => o.Customer).Include(o => o.AssignedUser).OrderBy(o => o.ExpectedCloseDate).ToList();
            ViewBag.StageSummary = opportunities.GroupBy(o => o.Stage).ToDictionary(g => g.Key, g => g.Count());
            return View(opportunities);
        }

        public ActionResult Create()
        {
            PopulateSelections(null, null, "Prospecting");
            return View(new Opportunity { ExpectedCloseDate = DateTime.UtcNow.AddDays(30), Stage = "Prospecting", Probability = 10 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Opportunity model)
        {
            if (!ModelState.IsValid)
            {
                PopulateSelections(model.CustomerId, model.AssignedToUserId, model.Stage);
                return View(model);
            }

            model.CreatedAt = DateTime.UtcNow;
            model.ModifiedAt = DateTime.UtcNow;
            _context.Opportunities.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var opportunity = _context.Opportunities.Find(id);
            if (opportunity == null)
            {
                return HttpNotFound();
            }

            PopulateSelections(opportunity.CustomerId, opportunity.AssignedToUserId, opportunity.Stage);
            return View(opportunity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Opportunity model)
        {
            var opportunity = _context.Opportunities.Find(id);
            if (opportunity == null)
            {
                return HttpNotFound();
            }

            if (!ModelState.IsValid)
            {
                PopulateSelections(model.CustomerId, model.AssignedToUserId, model.Stage);
                return View(model);
            }

            opportunity.CustomerId = model.CustomerId;
            opportunity.Title = model.Title;
            opportunity.Description = model.Description;
            opportunity.Value = model.Value;
            opportunity.Stage = model.Stage;
            opportunity.ExpectedCloseDate = model.ExpectedCloseDate;
            opportunity.AssignedToUserId = model.AssignedToUserId;
            opportunity.Probability = model.Probability;
            opportunity.ModifiedAt = DateTime.UtcNow;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var opportunity = _context.Opportunities.Find(id);
            if (opportunity == null)
            {
                return HttpNotFound();
            }

            _context.Opportunities.Remove(opportunity);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            base.Dispose(disposing);
        }

        private void PopulateSelections(int? customerId, int? assignedToUserId, string stage)
        {
            ViewBag.CustomerId = new SelectList(_context.Customers.OrderBy(c => c.CompanyName).ToList(), "Id", "CompanyName", customerId);
            ViewBag.AssignedToUserId = new SelectList(_context.Users.Where(u => u.IsActive).OrderBy(u => u.FullName).ToList(), "Id", "FullName", assignedToUserId);
            ViewBag.Stage = new SelectList(new List<string> { "Prospecting", "Qualification", "Proposal", "Negotiation", "ClosedWon", "ClosedLost" }, stage);
        }
    }
}
