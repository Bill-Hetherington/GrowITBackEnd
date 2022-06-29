using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GrowITBackEnd.Data;
using GrowITBackEnd.Models.DataModels;
using GrowITBackEnd.Models.RequestsModels;
using PeoplAPV2.Models.AuthModels;
using ApiTemplate.Models.AuthModels.JWTAuthentication.NET6._0.Auth;
using Microsoft.AspNetCore.Identity;

namespace GrowITBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //this endpoint triggers after clicking checkout on maintain cart page        
        [HttpPost]
        [Route("createOrder")]
        public async Task<IActionResult> createOrder(createOrderRequest orderRequest)
        {
            //get user from username
            var user = await _userManager.FindByNameAsync(orderRequest.Username);

            //create new order
            Orders order = new Orders()
            {
                UserId=user.Id,
                Order_Total=orderRequest.Order_Total,
                Date_Started = DateTime.Now,
            };
            //add to context and save changes
            _context.Orders.Add(order);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                NotFound(new Response { Status = "Failed", Message = "Order Creation failed" });
            }
            return Ok(new Response { Status = "Success", Message = "Order created successfully!" });
        }

        //creating the order items from the cart
        //could alternatively add item list in orderRequest model and iterate through to add order items
        [HttpPost]
        [Route("createOrderItem")]
        public async Task<IActionResult> createOrderItem(createOrderItemRequest orderItemRequest)
        {
            return Ok(new Response { Status = "Success", Message = "orderItem created successfully!" });
        }       

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrders(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdersExists(int id)
        {
            return (_context.Orders?.Any(e => e.OrdersID == id)).GetValueOrDefault();
        }
    }
}
