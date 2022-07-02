using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using ApiTemplate.Models.AuthModels.JWTAuthentication.NET6._0.Auth;
using GrowITBackEnd.Data;
using GrowITBackEnd.Models.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeoplAPV2.Models.AuthModels;
using GrowITBackEnd.Models.RequestsModels;
using GrowITBackEnd.Models.ReturnModels;


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

        //Triggered: clicking checkout on maintain cart page
        //all this info is grabbed from the cookie for cart
        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> CreateOrder(createOrderRequest orderRequest)
        {
            //get user from username
            var user = await _userManager.FindByNameAsync(orderRequest.Username);
            if(user == null)
            {
                NotFound();
            }

            //create new order
            Orders order = new Orders()
            {
                UserId=user.Id,
                Order_Total=orderRequest.Order_Total,
                Date_Started = DateTime.Now,
                Date_Completed = null,
            };
            //add to context and save changes
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            //create the corresponding order_items            
            foreach (var item in orderRequest.Items)
            {
                Order_Items order_Items = new Order_Items()
                {
                    OrdersID = order.OrdersID,
                    ItemID = item.ItemID,
                    Quantity = item.Quantity
                };
                //check if quanitity demanded exceeds quantity on hand
                var itemInStock=_context.Items.Where(now=>now.ItemID==item.ItemID).FirstOrDefault();
                if (item.Quantity > itemInStock.Quantity_on_Hand)
                {
                    //remove order_item for order
                    foreach(var oItem in _context.Order_Items)
                    {
                        if(oItem.OrdersID == order.OrdersID)_context.Order_Items.Remove(oItem);
                    }
                    _context.Orders.Remove(order);
                    return NotFound(new Response { Status = "Failed", Message = "Order item exceeds quantity on hand!" });
                }
                _context.Order_Items.Add(order_Items);
            }          

            try{
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                NotFound(new Response { Status = "Failed", Message = "Order Creation failed" });
            }
            return Ok(new Response { Status = "Success", Message = "Order created successfully!" });
        }                       

        //Triggered: admin clicks view Orders
        [HttpGet]
        [Route("GetAllOrders")]
        public List<OrdersReturn> GetAllOrders()
        {                       
            var allOrders=_context.Orders.ToListAsync().Result;
            //Declaring list for our request
            List<OrdersReturn> ordersReturns = new List<OrdersReturn>();
            //temp list for order-Items            
            foreach(var order in allOrders)
            {
                //Mapping of this temp
                OrdersReturn tempOrdersReturn = new OrdersReturn{
                    OrdersID = order.OrdersID,
                    UserId = order.UserId,
                    Order_Total = order.Order_Total,
                    Date_Started = order.Date_Started,
                    Date_Completed = order.Date_Completed,                    
                };
                var itemsInOrder = _context.Order_Items.Where(o => o.OrdersID == order.OrdersID).ToListAsync().Result;
                foreach(var item in itemsInOrder)
                {
                    //get item info
                    var tempItem=_context.Items.FindAsync(item.ItemID).Result;
                    ItemsInOrder tempItemInOrder = new ItemsInOrder
                    {
                        ItemID = tempItem.ItemID,
                        Item_Name=tempItem.Item_Name,
                        Price = tempItem.Price,
                        Quantity=item.Quantity
                    };
                    tempOrdersReturn.itemsInOrder.Add(tempItemInOrder);
                }
                ordersReturns.Add(tempOrdersReturn);
            }
            return ordersReturns;
        }

        //Triggered: clicking Complete button on a order which is outstanding
        //item quantity_onHand is updated and date completed is updated
        [HttpPost]
        [Route("CompleteOrder")]
        public async Task<IActionResult> CompleteOrder(int orderID)
        {
            if (!_context.Orders.Where(order => order.OrdersID == orderID).Any())
            {
                return NotFound(new Response { Status = "Failed", Message = "no such order in DB matches this orderID" });
            }
            //make list of order items with input ordersID
            List<Order_Items> order_Items = new List<Order_Items>();
            foreach (Order_Items oItem in _context.Order_Items)
            {
                if (oItem.OrdersID == orderID) { order_Items.Add(oItem); }                
            }
            //reduce the quantity on hand for each item in order
            foreach(Order_Items oItem in order_Items)
            {
                var item=_context.Items.Where(item1=>item1.ItemID==oItem.ItemID).FirstOrDefault();
                if (item != null)
                {
                    //extra precaution to protect stock going negative
                    if (item.Quantity_on_Hand >= oItem.Quantity)
                    {
                        _context.Items.Where(item1 => item1.ItemID == oItem.ItemID).FirstOrDefault().Quantity_on_Hand -= oItem.Quantity;
                    }                   
                }
            }
            //do the date completed for order
            _context.Orders.Where(order => order.OrdersID == orderID).FirstOrDefault().Date_Completed = DateTime.Now;
            //save chanegs
            await _context.SaveChangesAsync();
            return Ok(new Response { Status = "Success", Message = "Order completed successfully!" });
        }        
        private bool OrdersExists(int id)
        {
            return (_context.Orders?.Any(e => e.OrdersID == id)).GetValueOrDefault();
        }
    }
}
