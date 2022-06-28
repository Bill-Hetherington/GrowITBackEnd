using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GrowITBackEnd.Data;
using GrowITBackEnd.Models;
using PeoplAPV2.Models.AuthModels;
using Microsoft.AspNetCore.Identity;
using ApiTemplate.Models.AuthModels.JWTAuthentication.NET6._0.Auth;
using GrowITBackEnd.Models.RequestsModels;

namespace GrowITBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintaincartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MaintaincartController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Post: api/MaintainCart
        //return list of a single Cart        
        //Front end will display cart items from the returned instance of Cart
        [Route("GetCartOfUser")]
        [HttpPost]
        public async Task<ActionResult<Cart>> GetCartOfUser(String username)
        {
            var user = await _userManager.FindByNameAsync(username);
           //var cart = _context.Carts.Where(cart => cart.UserId== user.Id).ToListAsync();
            var cart=await _context.Carts.FindAsync(user.Id);

            if (_context.Carts == null)
          {
                return NotFound();
          }
            
            return cart;
        }

        //add item to Cart
        //Done from clicking add to cart on product or category page
        [Route("AddCartItem")]
        [HttpPost]
        public async Task<ActionResult> AddCartItem(AddCartItemRequest itemRequest)
        {
            var user = await _userManager.FindByNameAsync(itemRequest.Username);

            //anonymous user
            if (user == null)
            {
                return NotFound();
            }
            //cart Id of curUser
            var carts = _context.Carts.Where(cart => cart.UserId == user.Id).ToListAsync().Result;
            var cart = carts[0];

            //add cart items to context
            Cart_Items cart_Item = new Cart_Items
            {
                ItemID = itemRequest.ItemID,
                CartID = cart.CartID,
                Quantity = 1
            };
            _context.Cart_Items.Add(cart_Item);
            await _context.SaveChangesAsync();
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });

        }
        // GET: api/MaintainCart/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCarts(int id)
        {
          if (_context.Carts == null)
          {
              return NotFound();
          }
            var cart = await _context.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        // PUT: api/MaintainCart/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(int id, Cart cart)
        {
            if (id != cart.CartID)
            {
                return BadRequest();
            }

            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MaintainCart
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(Cart cart)
        {
          if (_context.Carts == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Carts'  is null.");
          }
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCart", new { id = cart.CartID }, cart);
        }

        // DELETE: api/MaintainCart/5
        //Delete the cart Item when user clicks remove in Cart view page
        [Route("DeleteCartItem")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCartItem(Cart_Items cartItem)
        {
            if(_context.Cart_Items.Where(item=>item==cartItem) == null)
            {
                return NoContent();
            }
            _context.Cart_Items.Remove(cartItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartExists(int id)
        {
            return (_context.Carts?.Any(e => e.CartID == id)).GetValueOrDefault();
        }
    }
}
