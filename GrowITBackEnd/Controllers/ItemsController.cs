using ApiTemplate.Models.AuthModels;
using ApiTemplate.Models.AuthModels.JWTAuthentication.NET6._0.Auth;
using GrowITBackEnd.Data;
using GrowITBackEnd.Models.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrowITBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ItemsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        //Triggered: Admin 
        [Authorize(Roles =UserRoles.User)]
        [HttpGet]
        [Route("GetAllItems")]
        public async Task<List<Item>> GetAllItems()
        {
            return _context.Items.ToListAsync().Result;
        }

        //Triggered: User clicks category name on the select category combo box
        //Get items for that category
        [HttpGet]
        [Route("GetCategoryItems")]
        public async Task<List<Item>> GetCategoryItems(string category)
        {
            return _context.Items.Where(i => i.Category == category).ToListAsync().Result;
        }

        //Gives type Error
        //Triggered: user clicks on specific item from category page/home page
        //get item for that selected item
        [HttpGet]
        [Route("GetSelectedItem")]
        public Item GetSelectedItem(Item item)
        {
            var selectedItem=_context.Items.Where(i=>i.ItemID==item.ItemID).FirstOrDefault();
            if (selectedItem == null)
            {
               NotFound(new Response { Status = "Failed", Message = "Could not find item" });
                return null;
            }
            return selectedItem;
        }

        //Triggered: Admin creates item
        [HttpPost]
        [Route("CreateItem")]
        public async Task<IActionResult> CreateItem(Item item)
        {
            var newItem = new Item
            {                
                Item_Name=item.Item_Name,
                Price=item.Price,
                Description=item.Description,
                Quantity_on_Hand=item.Quantity_on_Hand,
                Category=item.Category
            };
            _context.Items.Add(newItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound(new Response { Status = "Failed", Message = "Item Creation failed" });
            }
            return Ok(new Response { Status = "Success", Message = "Item Creation Successful" });
        }

        //Triggered by admin changing details of item
        //Item id must not be editable
        [HttpPut]
        [Route("UpdateItem")]
        public async Task<IActionResult> UpdateItem(int id,Item item)
        {
            if (id != item.ItemID)
            {
                return BadRequest(); 
            }
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound(new Response { Status = "Failed", Message = "Item not Found" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //Triggered: Admin deletes item
        [HttpDelete]
        [Route("DeleteItem")]
        public async Task<IActionResult> DeleteItem(Item item)
        {
            _context.Items.Remove(item);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound(new Response { Status = "Failed", Message = "Item Deletion failed" });
            }

            return Ok(new Response { Status = "Success", Message = "Item Deleted Succesfully" });
        }

        private bool ItemExists(int id)
        {
            return (_context.Items?.Any(e => e.ItemID == id)).GetValueOrDefault();
        }

        //Api Endpoing for saving images to backend
        [Route("SaveImage")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Images/" + filename;

                using (var strean = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(strean);
                }
                return new JsonResult(filename);

            }
            catch (Exception)
            {
                return new JsonResult("default.png");
            }
        }
    }
}
