using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;
using System.Text.Json;
using src.Services;
//using System.Web.Script.Serialization;

namespace src.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/EmailTicket")]
    [Authorize]
    public class EmailTicketController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public EmailTicketController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }


        // GET: api/EmailTicket
        [HttpGet]
        public IActionResult GetTicket()
        {
            //_context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            //var query = _context.Ticket.Where(x => x.organizationId.Equals(organizationId)).ToList();
            //    var sql = ((Entity.Core.Objects.ObjectQuery)query).ToTraceString();

            //string oID = organizationId.ToString();
            //string oIDNew = oID.Replace('{', "'")
            //return Json(new { data = _context.EmailTicket.Where(x => x.TicketNo.Equals(1)).ToList() });
            //var mn = _context.EmailTicket.ToList();

            //var json = JsonSerializer.Serialize(mn);
            //var tt = Json(new { data = mn });

            var mn = _context.EmailTicket.Where(x => x.TicketNo.Equals(10)).ToList();
            var rr = Json(new { data = _context.EmailTicket.Where(x => !x.TicketNo.Equals(0)).ToList() });
            return rr;


        }

        // GET: api/Ticket/Customer
        [HttpGet("Customer/{customerId}")]
        public IActionResult GetTicketCustomer([FromRoute] Guid customerId)
        {
            return Json(new { data = _context.Ticket.Where(x => x.customerId.Equals(customerId)).ToList() });
        }


        // POST: api/EmailTicket
        [HttpPost]
        public async Task<IActionResult> PostEmailTicket([FromBody] EmailTicket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //_context.Ticket.Add(ticket);
                string source = ticket.EmailFrom;
                string result = source.Split(new string[] { "<" }, 3, StringSplitOptions.None)[1];
                ticket.EmailFrom = result.Replace(">", "");
                await _emailSender.SendEmailAsync(ticket.EmailFrom, "RE: " + ticket.EmailSubject, ticket.ReplyCustomer + "   " + "\r\n" + "Previous Message:" + "\r\n" + ticket.EmailContent);

                var obj = _context.EmailTicket.SingleOrDefault(item => item.TicketNo == ticket.TicketNo);
                if (obj != null)
                {
                    obj.ReplyStatus = 1;
                    obj.RepliedBy = HttpContext.Session.GetString("username");

                }
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Email Sent to customer Successfuly." });

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