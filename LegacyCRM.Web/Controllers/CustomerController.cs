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
    public class CustomerController : Controller
    {
        private readonly CrmDbContext _context = new CrmDbContext();
        private readonly ExportService _exportService = new ExportService();

        public ActionResult Index(string search = null, string status = null, int page = 1)
        {
            const int pageSize = 10;
            IEnumerable<Customer> customers;

            if (!string.IsNullOrWhiteSpace(search))
            {
                string safeSearch = search.Replace("'", "''");
                customers = _context.Database.SqlQuery<Customer>("SELECT * FROM Customers WHERE Name LIKE '%" + safeSearch + "%' OR CompanyName LIKE '%" + safeSearch + "%'").ToList();
            }
            else
            {
                customers = _context.Customers.Include(c => c.AssignedUser).ToList();
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                customers = customers.Where(c => string.Equals(c.Status, status, StringComparison.OrdinalIgnoreCase));
            }

            var results = customers.OrderBy(c => c.Name).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            ViewBag.Search = search;
            ViewBag.CurrentStatus = status;
            ViewBag.Page = page;
            ViewBag.StatusFilter = new SelectList(GetStatusOptions(), status);
            return View(results);
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers
                .Include(c => c.AssignedUser)
                .Include(c => c.Contacts)
                .Include(c => c.Opportunities)
                .Include(c => c.Tickets)
                .FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            ViewBag.DocumentFolder = _exportService.GetDocumentFolder(id);
            return View(customer);
        }

        public ActionResult Create()
        {
            PopulateSelections(null, "Lead");
            return View(new CustomerViewModel { Status = "Lead" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PopulateSelections(model.AssignedToUserId, model.Status);
                return View(model);
            }

            var customer = new Customer
            {
                Name = model.Name,
                CompanyName = model.CompanyName,
                Email = model.Email,
                Phone = model.Phone,
                Address = model.Address,
                City = model.City,
                Country = model.Country,
                Industry = model.Industry,
                Status = model.Status,
                AssignedToUserId = model.AssignedToUserId,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
                Notes = model.Notes
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("Details", new { id = customer.Id });
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            var model = new CustomerViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                CompanyName = customer.CompanyName,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
                City = customer.City,
                Country = customer.Country,
                Industry = customer.Industry,
                Status = customer.Status,
                AssignedToUserId = customer.AssignedToUserId,
                Notes = customer.Notes
            };

            PopulateSelections(model.AssignedToUserId, model.Status);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CustomerViewModel model)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            if (!ModelState.IsValid)
            {
                PopulateSelections(model.AssignedToUserId, model.Status);
                return View(model);
            }

            customer.Name = model.Name;
            customer.CompanyName = model.CompanyName;
            customer.Email = model.Email;
            customer.Phone = model.Phone;
            customer.Address = model.Address;
            customer.City = model.City;
            customer.Country = model.Country;
            customer.Industry = model.Industry;
            customer.Status = model.Status;
            customer.AssignedToUserId = model.AssignedToUserId;
            customer.Notes = model.Notes;
            customer.ModifiedAt = DateTime.UtcNow;
            _context.SaveChanges();

            return RedirectToAction("Details", new { id = customer.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public ActionResult RecentActivities(int customerId)
        {
            var activities = _context.Activities
                .Include(a => a.CreatedByUser)
                .Where(a => a.CustomerId == customerId)
                .OrderByDescending(a => a.ActivityDate)
                .Take(10)
                .ToList();

            return PartialView("_RecentActivities", activities);
        }

        public FileResult Export(string status = null)
        {
            var customers = _context.Customers.AsQueryable();
            if (!string.IsNullOrWhiteSpace(status))
            {
                customers = customers.Where(c => c.Status == status);
            }

            byte[] csv = _exportService.ExportCustomers(customers.OrderBy(c => c.Name).ToList());
            return File(csv, "text/csv", "customers.csv");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            base.Dispose(disposing);
        }

        private void PopulateSelections(int? assignedToUserId, string status)
        {
            ViewBag.AssignedToUserId = new SelectList(_context.Users.Where(u => u.IsActive).OrderBy(u => u.FullName).ToList(), "Id", "FullName", assignedToUserId);
            ViewBag.StatusOptions = new SelectList(GetStatusOptions(), status);
        }

        private static IList<string> GetStatusOptions()
        {
            return new List<string> { "Lead", "Prospect", "Active", "Inactive" };
        }
    }
}
