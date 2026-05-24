using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using LegacyCRM.Core;
using LegacyCRM.Core.Models;
using LegacyCRM.Web.Models.ViewModels;
using LegacyCRM.Web.Services;

namespace LegacyCRM.Web.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private readonly CrmDbContext _context = new CrmDbContext();
        private readonly EmailNotificationService _emailNotificationService = new EmailNotificationService();

        public ActionResult Index(string status = null)
        {
            var tickets = _context.Tickets.Include(t => t.Customer).Include(t => t.AssignedUser).OrderByDescending(t => t.CreatedAt).ToList();
            if (!string.IsNullOrWhiteSpace(status))
            {
                tickets = tickets.Where(t => t.Status == status).ToList();
            }

            ViewBag.Status = new SelectList(GetStatusOptions(), status);
            return View(tickets);
        }

        public ActionResult Create()
        {
            PopulateSelections(null, null, "Open", "Medium");
            return View(new TicketViewModel { Status = "Open", Priority = "Medium" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TicketViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PopulateSelections(model.CustomerId, model.AssignedToUserId, model.Status, model.Priority);
                return View(model);
            }

            var ticket = new Ticket
            {
                CustomerId = model.CustomerId,
                Title = model.Title,
                Description = model.Description,
                Priority = model.Priority,
                Status = model.Status,
                AssignedToUserId = model.AssignedToUserId,
                CreatedAt = DateTime.UtcNow,
                ResolutionNotes = model.ResolutionNotes
            };

            _context.Tickets.Add(ticket);
            _context.SaveChanges();

            var customer = _context.Customers.Find(ticket.CustomerId);
            if (customer != null)
            {
                _emailNotificationService.SendTicketCreatedNotification(ticket, customer);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var ticket = _context.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }

            var model = new TicketViewModel
            {
                Id = ticket.Id,
                CustomerId = ticket.CustomerId,
                Title = ticket.Title,
                Description = ticket.Description,
                Priority = ticket.Priority,
                Status = ticket.Status,
                AssignedToUserId = ticket.AssignedToUserId,
                ResolutionNotes = ticket.ResolutionNotes
            };

            PopulateSelections(model.CustomerId, model.AssignedToUserId, model.Status, model.Priority);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TicketViewModel model)
        {
            var ticket = _context.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }

            if (!ModelState.IsValid)
            {
                PopulateSelections(model.CustomerId, model.AssignedToUserId, model.Status, model.Priority);
                return View(model);
            }

            ticket.CustomerId = model.CustomerId;
            ticket.Title = model.Title;
            ticket.Description = model.Description;
            ticket.Priority = model.Priority;
            ticket.Status = model.Status;
            ticket.AssignedToUserId = model.AssignedToUserId;
            ticket.ResolutionNotes = model.ResolutionNotes;
            ticket.ResolvedAt = model.Status == "Resolved" || model.Status == "Closed" ? (DateTime?)DateTime.UtcNow : null;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var ticket = _context.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }

            _context.Tickets.Remove(ticket);
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

        private void PopulateSelections(int? customerId, int? assignedToUserId, string status, string priority)
        {
            ViewBag.CustomerId = new SelectList(_context.Customers.OrderBy(c => c.CompanyName).ToList(), "Id", "CompanyName", customerId);
            ViewBag.AssignedToUserId = new SelectList(_context.Users.Where(u => u.IsActive).OrderBy(u => u.FullName).ToList(), "Id", "FullName", assignedToUserId);
            ViewBag.StatusOptions = new SelectList(GetStatusOptions(), status);
            ViewBag.PriorityOptions = new SelectList(new List<string> { "Low", "Medium", "High", "Critical" }, priority);
        }

        private static IList<string> GetStatusOptions()
        {
            return new List<string> { "Open", "InProgress", "Resolved", "Closed" };
        }
    }
}
