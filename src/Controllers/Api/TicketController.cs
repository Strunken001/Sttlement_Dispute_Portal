using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;
using src.Services;

namespace src.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Ticket")]
    [Authorize]
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public TicketController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }


        // GET: api/Ticket
        [HttpGet("{organizationId}")]
        public IActionResult GetTicket([FromRoute] Guid organizationId)
        {
            //_context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            //var query = _context.Ticket.Where(x => x.organizationId.Equals(organizationId)).ToList();
            //    var sql = ((Entity.Core.Objects.ObjectQuery)query).ToTraceString();

            string oID = organizationId.ToString();
            //string oIDNew = oID.Replace('{', "'")
            var mn = _context.Ticket.Where(x => x.organizationId.Equals(organizationId)).ToList();
            var rr = Json(new { data = _context.Ticket.Where(x => x.organizationId.Equals(organizationId)).ToList() });
            return rr;
        }


        // GET: api/Ticket/Customer
        [HttpGet("Customer/{customerId}")]
        public IActionResult GetTicketCustomer([FromRoute] Guid customerId)
        {
            return Json(new { data = _context.Ticket.Where(x => x.customerId.Equals(customerId)).ToList() });
        }

        //POST: api/Ticket
        [HttpPost]
        public async Task<IActionResult> PostTicket([FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (ticket.ticketId == Guid.Empty)
                {

                    //Contact contact = _context.Contact.Where(x => x.contactId.Equals(ticket.contactId)).FirstOrDefault();

                    //int value = GetValueFromDb();
                    //var enumDisplayStatus = (EnumDisplayStatus)value;
                    //string stringValue = enumDisplayStatus.ToString();

                    ticket.ticketId = Guid.NewGuid();
                    //ticket.customerId = contact.customerId;
                    var username = HttpContext.Session.GetString("username");
                    ViewBag.user = HttpContext.Session.GetString("username");
                    ticket.CreateBy = username;
                    _context.Ticket.Add(ticket);

                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Add new data success." });
                }
                else
                {
                    Guid ID = ticket.ticketId;
                    var username = HttpContext.Session.GetString("username");
                    ViewBag.user = HttpContext.Session.GetString("username");
                    int type = 0;
                    string ratingLink = "http://localhost:59420/rating?20?0" + type;
                    var obj = _context.Ticket.SingleOrDefault(item => item.ticketId == ID);
                    if (obj != null)
                    {
                        obj.comments = ticket.comments;
                        obj.ticketStatus = ticket.ticketStatus;
                        obj.assignedTo = ticket.assignedTo;
                        obj.CreateBy = username;
                        if (ticket.ticketStatus.ToString() == "Closed")
                        {
                            await _emailSender.SendEmailAsync(obj.email, "Ticket Closed",
                                                    $"Dear Customer, Your ticket with number: '{obj.ticketNo}' and type '{type}' has been closed.Kindly rate your experience using this link: <a href='{HtmlEncoder.Default.Encode(ratingLink)}'>link</a>");
                        }
                        _context.SaveChanges();
                    }

                    return Json(new { success = true, message = "Edit data success." });
                }
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }


        }

        // POST: api/Ticket/Customer
        [HttpPost("Customer")]
        public async Task<IActionResult> PostTicketCustomer([FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (ticket.ticketId == Guid.Empty)
                {
                    ticket.ticketId = Guid.NewGuid();
                    _context.Ticket.Add(ticket);

                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Add new data success." });
                }
                else
                {
                    _context.Update(ticket);

                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Edit data success." });
                }
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }


        }

        // DELETE: api/Ticket/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var ticket = await _context.Ticket.SingleOrDefaultAsync(m => m.ticketId == id);
                if (ticket == null)
                {
                    return NotFound();
                }

                _context.Ticket.Remove(ticket);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Delete success." });
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }


        }

        // DELETE: api/Ticket/Customer/5
        [HttpDelete("Customer/{id}")]
        public async Task<IActionResult> DeleteTicketCustomer([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var ticket = await _context.Ticket.SingleOrDefaultAsync(m => m.ticketId == id);
                if (ticket == null)
                {
                    return NotFound();
                }

                _context.Ticket.Remove(ticket);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Delete success." });
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }


        }

        private bool TicketExists(Guid id)
        {
            return _context.Ticket.Any(e => e.ticketId == id);
        }
    }
}