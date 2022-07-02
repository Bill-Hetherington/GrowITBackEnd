using ApiTemplate.Models.AuthModels.JWTAuthentication.NET6._0.Auth;
using GrowITBackEnd.Data;
using GrowITBackEnd.Models.DataModels;
using GrowITBackEnd.Models.RequestsModels;
using GrowITBackEnd.Models.ReturnModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeoplAPV2.Models.AuthModels;

namespace GrowITBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportTicketsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SupportTicketsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        //create a support ticket
        //Triggered: user clicks support then complete button on the request support page
        [HttpPost]
        [Route("CreateSupportTicket")]
        public async Task<IActionResult> CreateSupportTicket(CreateSupportRequest supportRequest)
        {
            var user = await _userManager.FindByNameAsync(supportRequest.Username);
            if (user == null)
            {
               return NotFound(new Response { Status = "Failed", Message = "Cannot find user" });
            }
            //create new support ticket then add to context
            Support_Tickets supportTicket = new Support_Tickets
            {
                Date_Generated=DateTime.Now,
                Description=supportRequest.Description,
                UserId=user.Id,
                ApplicationUser = user
            };
            _context.Support_tickets.Add(supportTicket);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound(new Response { Status = "Failed", Message = "Support Ticket Creation failed" });
            }

            return Ok(new Response { Status = "Success", Message = "Support Ticket created Succesfully" });
        }

        [HttpGet]
        [Route("GetSupportTicket")]
        public async Task<List<GetSupportTicketReturn>> GetSupportTicketAsync()
        {
            //Initialize list to return
            List<GetSupportTicketReturn> supportReturns=new List<GetSupportTicketReturn>();

            //get reference list of support tickets
            var allSupportTickets=_context.Support_tickets.ToListAsync().Result;
            foreach(Support_Tickets ticket in allSupportTickets)
            {
                var user = await _userManager.FindByIdAsync(ticket.UserId);
                GetSupportTicketReturn supportReturn= new GetSupportTicketReturn
                {
                    SuppID=ticket.SuppID,
                    Username=user.UserName,
                    Date_Generated = ticket.Date_Generated,
                    Description=ticket.Description,
                };
                supportReturns.Add(supportReturn);
            }
            return supportReturns;
        }

        //close support ticket
        //Triggered by admin clicking close on the specific ticket
        [HttpDelete]
        [Route("DeleteSupportTicket")]
        public async Task<IActionResult> DeleteSupportTicket(int SuppID)
        {
            //get reference to exact support ticket
            var ticket = await _context.Support_tickets.FindAsync(SuppID);
            //remove this support ticket
            _context.Support_tickets.Remove(ticket);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound(new Response { Status = "Failed", Message = "Support Ticket Deletion failed" });
            }

            return Ok(new Response { Status = "Success", Message = "Support Ticket Deleted Succesfully" });
        }
    }
}
