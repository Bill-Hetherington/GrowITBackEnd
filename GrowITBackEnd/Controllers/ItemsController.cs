using ApiTemplate.Models.AuthModels;
using ApiTemplate.Models.AuthModels.JWTAuthentication.NET6._0.Auth;
using GrowITBackEnd.Data;
using GrowITBackEnd.Models.DataModels;
using GrowITBackEnd.Models.RequestsModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
        public Item GetSelectedItem(int id)
        {
            var selectedItem=_context.Items.Where(i=>i.ItemID==id).FirstOrDefault();
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
        public async Task<Item> CreateItem(Item item)
        {
            var newItem = new Item
            {                
                Item_Name=item.Item_Name,
                Price=item.Price,
                Description=item.Description,
                Quantity_on_Hand=item.Quantity_on_Hand,
                Category=item.Category,
                imageURL=item.imageURL,
                hotDeal=item.hotDeal,
            };
            _context.Items.Add(newItem);
  
            await _context.SaveChangesAsync();

            return newItem;
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

            return Ok(new Response { Status = "Success", Message = "Item Updated Succesfully" });
        }

        //Triggered: Admin deletes item
        [HttpPost]
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
        public async Task<IActionResult> SaveFile([FromForm] FileModel file)
        {
            try
            {
                var httpRequest = Request.Form;

                var physicalPath = _env.ContentRootPath + "Images\\" + file.FileName;
                Item item = new Item { Category = file.Category, Description = file.Description, hotDeal = file.hotDeal, imageURL = file.imageURL, ItemID = file.ItemID, Item_Name = file.Item_Name, Price = Convert.ToDecimal(file.Price), Quantity_on_Hand = file.Quantity_on_Hand };
                item.imageURL = String.Format("https://localhost:5000/Images/{0}", file.FileName);
                item.hotDeal = null;
                _context.Entry(item).State = EntityState.Modified;
                using (var strean = new FileStream(physicalPath, FileMode.Create))
                {
                    file.FormFile.CopyTo(strean);
                }
                await _context.SaveChangesAsync();
                return Ok(new Response { Status = "Success", Message = "Item Image Succesfully Updated" });
            }
            catch
            {
                return NotFound(new Response { Status = "Success", Message = "Unable to update item image" });
            }
        }
    }
}
