using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using LegacyCRM.Core;
using LegacyCRM.Core.Models;

namespace LegacyCRM.Web.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly CrmDbContext _context = new CrmDbContext();

        public ActionResult Index()
        {
            var contacts = _context.Contacts.Include(c => c.Customer).OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ToList();
            return View(contacts);
        }

        public ActionResult Create()
        {
            PopulateCustomers(null);
            return View(new Contact { CreatedAt = DateTime.UtcNow });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contact model)
        {
            if (!ModelState.IsValid)
            {
                PopulateCustomers(model.CustomerId);
                return View(model);
            }

            model.CreatedAt = DateTime.UtcNow;
            _context.Contacts.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var contact = _context.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            PopulateCustomers(contact.CustomerId);
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Contact model)
        {
            var contact = _context.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            if (!ModelState.IsValid)
            {
                PopulateCustomers(model.CustomerId);
                return View(model);
            }

            contact.CustomerId = model.CustomerId;
            contact.FirstName = model.FirstName;
            contact.LastName = model.LastName;
            contact.Email = model.Email;
            contact.Phone = model.Phone;
            contact.Title = model.Title;
            contact.IsPrimary = model.IsPrimary;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var contact = _context.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            _context.Contacts.Remove(contact);
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

        private void PopulateCustomers(int? selectedCustomerId)
        {
            ViewBag.CustomerId = new SelectList(_context.Customers.OrderBy(c => c.CompanyName).ToList(), "Id", "CompanyName", selectedCustomerId);
        }
    }
}
