using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;

namespace src.Controllers
{
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder
        //        .Entity<Rider>()
        //        .Property(e => e.Mount)
        //        .HasConversion(
        //            v => v.ToString(),
        //            v => (EquineBeast)Enum.Parse(typeof(EquineBeast), v));
        //}

        public IActionResult Index(Guid org)
        {
            if (org == Guid.Empty)
            {
                return NotFound();
            }
            Organization organization = _context.Organization.Where(x => x.organizationId.Equals(org)).FirstOrDefault();
            ViewData["org"] = org;
            return View(organization);
        }

        public IActionResult Customer(Guid cust)
        {
            if (cust == Guid.Empty)
            {
                return NotFound();
            }
            Customer customer = _context.Customer.Where(x => x.customerId.Equals(cust)).FirstOrDefault();
            ViewData["cust"] = cust;
            return View(customer);
        }

        public IActionResult AddEdit(Guid org, Guid id)
        {
            if (id == Guid.Empty)
            {
                var username = HttpContext.Session.GetString("username");
                ViewBag.user = HttpContext.Session.GetString("username");

                Ticket ticket = new Ticket();
                ticket.organizationId = org;

                IList<Product> products = _context.Product.Where(x => x.organizationId.Equals(org)).ToList();
                ViewBag.productId = new SelectList(products, "productId", "productName");

                IList<SupportAgent> agents = _context.SupportAgent.Where(x => x.organizationId.Equals(org)).ToList();
                ViewBag.supportAgentId = new SelectList(agents, "supportAgentName", "supportAgentName");

                IList<SupportEngineer> engineers = _context.SupportEngineer.Where(x => x.organizationId.Equals(org)).ToList();
                ViewBag.supportEngineerId = new SelectList(engineers, "supportEngineerId", "supportEngineerName");

                IList<Contact> contacts = _context.Contact
                    .Where(x => x.customer.organizationId.Equals(org)).ToList();
                ViewBag.contactId = new SelectList(contacts, "contactId", "contactName");

                return View(ticket);
            }
            else
            {
                var username = HttpContext.Session.GetString("username");
                ViewBag.user = HttpContext.Session.GetString("username");
                Ticket ticket = _context.Ticket.Where(x => x.ticketId.Equals(id)).FirstOrDefault();
                ViewBag.Text = ticket.description;
                ViewBag.paymentReference = ticket.paymentReference;
                ViewBag.sessionID = ticket.sessionID;
                ViewBag.accountNumber = ticket.accountNumber;
                ViewBag.date = ticket.date;
                ViewBag.amount = ticket.amount;
                ViewBag.billerRef = ticket.billerRef;
                ViewBag.meterNumber = ticket.meterNumber;
                ViewBag.email = ticket.email;
                //IList<Product> products = _context.Product.Where(x => x.organizationId.Equals(ticket.organizationId)).ToList();
                //ViewBag.productId = new SelectList(products, "productId", "productName", ticket.productId);

                IList<SupportAgent> agents = _context.SupportAgent.Where(x => x.organizationId.Equals(ticket.organizationId)).ToList();
                ViewBag.supportAgentId = new SelectList(agents, "supportAgentName", "supportAgentName", ticket.supportAgentId);

                IList<TicketTypes> TicketTypes = _context.TicketTypes.ToList();
                ViewBag.TicketTypes = new SelectList(TicketTypes, "ticketType", "ticketType", TicketTypes);

                IList<TicketChannel> TicketChannel = _context.TicketChannel.ToList();
                ViewBag.TicketChannel = new SelectList(TicketChannel, "ticketChannel", "ticketChannel", TicketChannel);

                IList<TicketPriority> TicketPriority = _context.TicketPriority.ToList();
                ViewBag.TicketPriority = new SelectList(TicketPriority, "ticketPriority", "ticketPriority", TicketPriority);

                IList<TicketStatus> TicketStatus = _context.TicketStatus.ToList();
                ViewBag.TicketStatus = new SelectList(TicketStatus, "ticketChannel", "ticketChannel", TicketStatus);

                //IList<SupportEngineer> engineers = _context.SupportEngineer.Where(x => x.organizationId.Equals(ticket.organizationId)).ToList();
                //ViewBag.supportEngineerId = new SelectList(engineers, "supportEngineerId", "supportEngineerName", ticket.supportAgentId);

                //IList<Contact> contacts = _context.Contact
                //    .Where(x => x.customer.organizationId.Equals(ticket.organizationId)).ToList();
                //ViewBag.contactId = new SelectList(contacts, "contactId", "contactName", ticket.contactId);

                return View(ticket);
            }

        }

        public IActionResult AddEditCustomerTicket(Guid cust, Guid id)
        {
            if (id == Guid.Empty)
            {
                Customer customer = _context.Customer.Where(x => x.customerId.Equals(cust)).FirstOrDefault();
                var applicationUserId = _userManager.GetUserId(User);
                Contact contact = _context.Contact.Where(x => x.applicationUserId.Equals(applicationUserId)).FirstOrDefault();
                Ticket ticket = new Ticket();
                //ticket.customerId = cust;
                ticket.organizationId = customer.organizationId;
                //ticket.contactId = contact.contactId;

                IList<Product> products = _context.Product.Where(x => x.organizationId.Equals(customer.organizationId)).ToList();
                ViewBag.productId = new SelectList(products, "productId", "productName");

                return View(ticket);
            }
            else
            {
                Ticket ticket = _context.Ticket.Where(x => x.ticketId.Equals(id)).FirstOrDefault();

                //IList<Product> products = _context.Product.Where(x => x.organizationId.Equals(ticket.organizationId)).ToList();
                //ViewBag.productId = new SelectList(products, "productId", "productName", ticket.productId);

                return View(ticket);
            }

        }

        public IActionResult SubmitAddEdit()
        {
            Ticket ticket = new Ticket();
            return View(ticket);
        }
    }
}