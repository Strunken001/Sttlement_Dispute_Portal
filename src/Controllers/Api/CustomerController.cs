using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;
using src.Services;

namespace src.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Customer")]
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public CustomerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Customer
        [HttpGet("{organizationId}")]
        public IActionResult GetCustomer([FromRoute]Guid organizationId)
        {
            return Json(new { data = _context.Customer.Where(x => x.organizationId.Equals(organizationId)).ToList() });
        }



        // POST: api/Customer
        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                if (customer.customerId == Guid.Empty)
                {
                    customer.customerId = Guid.NewGuid();
                    _context.Customer.Add(customer);

                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Add new data success." });
                }
                else
                {
                    _context.Update(customer);

                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Edit data success." });
                }
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> PostCustomer2([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (customer.customerId == Guid.Empty)
            {
                try
                {
                    var user = new ApplicationUser { UserName = customer.email, Email = customer.email, FullName = customer.customerName };

                    user.IsSupportAgent = true;
                    var randomPassword = new Random().Next(0, 999999);
                    var result = await _userManager.CreateAsync(user, randomPassword.ToString());
                    if (result.Succeeded)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);

                        await _emailSender.SendEmailAsync(customer.email, "Confirm your email and Registration",
                        $"Your email has been registered. With username:'{customer.email}'  and temporary  password:'{randomPassword.ToString()}' .Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>link</a>");

                        //customer..applicationUser = user;
                        Organization org = _context.Organization.Where(x => x.organizationId.Equals(customer.organizationId)).FirstOrDefault();
                        customer.organization = org;

                        //customer.supportAgentId = Guid.NewGuid();
                        _context.Customer.Add(customer);

                        await _context.SaveChangesAsync();

                        return Json(new { success = true, message = "Add new data success." });
                    }
                    else
                    {
                        return Json(new { success = false, message = "UserManager CreateAsync Fail." });
                    }

                }
                catch (Exception ex)
                {

                    return Json(new { success = false, message = ex.Message });
                }




            }
            else
            {
                try
                {
                    _context.Update(customer);

                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Edit data success." });
                }
                catch (Exception ex)
                {

                    return Json(new { success = false, message = ex.Message });
                }

            }
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var customer = await _context.Customer.SingleOrDefaultAsync(m => m.customerId == id);
                if (customer == null)
                {
                    return NotFound();
                }

                _context.Customer.Remove(customer);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Delete success." });
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }

           
        }

        private bool CustomerExists(Guid id)
        {
            return _context.Customer.Any(e => e.customerId == id);
        }
    }
}