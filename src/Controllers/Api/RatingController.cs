using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using src.Data;
using src.Models;


namespace src.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Rating")]
    [Authorize]
    public class RatingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IConfiguration _configuration;
        private string connectionString;

        public RatingController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            connectionString = configuration.GetSection("ConnectionStrings").GetValue<string>("DefaultConnection");

        }


        // GET: api/Ticket
        [HttpGet("{ticketNo}")]
        public IActionResult GetTicket([FromRoute] Guid organizationId)
        {
            //_context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            //var query = _context.Ticket.Where(x => x.organizationId.Equals(organizationId)).ToList();
            //    var sql = ((Entity.Core.Objects.ObjectQuery)query).ToTraceString();

            string oID = organizationId.ToString();
            //string oIDNew = oID.Replace('{', "'")
            var rr = Json(new { data = _context.Ticket.Where(x => x.organizationId.Equals(organizationId)).ToList() });
            return rr;
        }


        // GET: api/Ticket/Customer
        [HttpGet("Customer/{customerId}")]
        public IActionResult GetTicketCustomer([FromRoute] Guid customerId)
        {
            return Json(new { data = _context.Ticket.Where(x => x.customerId.Equals(customerId)).ToList() });
        }

        //POST: api/Rating
        [HttpPost]
        public async Task<IActionResult> PostRating([FromBody] string Rating)
        {
            var table = "";
            var values = Rating.Substring(Rating.IndexOf('?'));
            string[] arr = values.Split('?');
            string first = arr[1].ToString();
            string ticketType = arr[2].ToString();
            string rating = arr[3].ToString();
            long  ticketNo = Convert.ToInt64(first);

            if (ticketType == "0")
            {
                table = "Ticket";
            }
            else
            {
                table = "EmailTicket";
            }

            int resp = 0;
            var query = "update " + table + " set rating = '" + rating + "' where Ticketno = " + ticketNo;
            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.CommandType = CommandType.Text;

                            conn.Open();
                            resp = cmd.ExecuteNonQuery();
                            conn.Close();

                        }
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, message = ex.Message });
                    }

                }

                return Json(new { success = true, message = "Thank you. We have received your feedback!" });

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