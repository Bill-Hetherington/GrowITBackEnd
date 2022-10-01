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
    public class WishListController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public WishListController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //return wish list contents as items
        //user clicks wishlist icon to trigger this
        [HttpPost]
        [Route("getWishListContent")]
        public async Task<ActionResult<ICollection<Item>>> getWishListContent(getWishlistContentRequest wishlistRequest)
        {
            //get wishlist of user requesting
            var user = await _userManager.FindByNameAsync(wishlistRequest.Username);
            var wishlist = _context.Wishlists.Where(wishlist1 => wishlist1.UserId == user.Id).FirstOrDefault();
            if (wishlist == null)
            {
                return NotFound();
            }

            //create list of wishlist items with wishlist id
            var list= _context.Wishlist_Items.Where(wishlistItem => wishlistItem.WishID == wishlist.WishID).ToListAsync().Result;
            if (list == null)
            {
                return NotFound();
            }
            //map list to itemList of db
            List<Item> itemList = new List<Item>();
            for (int i=0; i<list.Count; i++)
            {
                Item item=(Item)_context.Items.Where(item => item.ItemID == list[i].ItemID).FirstOrDefault();
                itemList.Add(item);
            }
            return itemList;
        }

        [HttpPost]
        [Route("getWishList")]
        public async Task<ActionResult<int>> getWishList(getWishlistContentRequest wishlistRequest)
        {
            //get wishlist of user requesting
            var user = await _userManager.FindByNameAsync(wishlistRequest.Username);
            var wishlist = _context.Wishlists.Where(wishlist1 => wishlist1.UserId == user.Id).FirstOrDefault();
            return wishlist.WishID;
        }

        //create a wishlist item then add it to the wishlist of the user
        //add item to wishlist
        //triggered from user clicking add to wishlist on item page
        [HttpPost]
        [Route("createWishListItem")]
        public async Task<IActionResult> createWishlistItem(createWishListItemRequest itemRequest)
        {            
            var user = await _userManager.FindByNameAsync(itemRequest.Username);
            //Reference wishlist with userID corresponding to requests userID
            var wishlist =(Wishlist) _context.Wishlists.Where(wishlist1 => wishlist1.UserId == user.Id).FirstOrDefault();           
            if (wishlist == null)
            {
                return NotFound();                
            }

            //create a new wishlist item
            //add that WishList_item to the context
            Wishlist_Items wishlist_Items = new Wishlist_Items()
            {
                ItemID = itemRequest.ItemID,
                WishID = wishlist.WishID
            };
            //this ensures duplicates are not entered into wishlist
            if (_context.Wishlist_Items.Where(existingItem => existingItem.ItemID == wishlist_Items.ItemID&&
            existingItem.WishID==wishlist_Items.WishID).Any())
            {
                return Ok(new Response { Status = "Fail", Message = "Wishlist already contains this item" });
            }
            _context.Wishlist_Items.Add(wishlist_Items);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                NotFound(new Response { Status = "Failed", Message = "WishList item Creation failed" });
            }
            return Ok(new Response { Status = "Success", Message = "Wishlist Item created successfully!" });
        }

        [HttpDelete]
        [Route("deleteWishListItem")]
        public async Task<IActionResult> deleteWishlistItem(createWishListItemRequest itemRequest)
        {
            //get user and check if they exist
            var user = await _userManager.FindByNameAsync(itemRequest.Username);
            if (user == null)
            {
                NotFound(new Response { Status = "Failed", Message = "user not found" });
            }
            //get wishlist
            var wishlist = _context.Wishlists.Where(wishlist1 => wishlist1.UserId == user.Id).FirstOrDefault();
            //remove wishlist item with item id and wish id            
            var wishlistItem=_context.Wishlist_Items.Where(wishItem=>wishItem.ItemID==itemRequest.ItemID&&
            wishItem.WishID==wishlist.WishID).FirstOrDefault();
            if (wishlistItem == null)
            {
                NotFound();
            }
            _context.Wishlist_Items.Remove(wishlistItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                NotFound(new Response { Status = "Failed", Message = "WishList item deletion failed" });
            }
            return Ok(new Response { Status = "Success", Message = "Wishlist Item deletion successfull!" });
        }
    }
}
