using ApiTemplate.Models.AuthModels.JWTAuthentication.NET6._0.Auth;
using GrowITBackEnd.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeoplAPV2.Models.AuthModels;

namespace GrowITBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //clicking profile icon brings up two boxes one for details and one for past orders
        //Triggered: User clicks view personal details in the profile page
        [HttpGet]
        [Route("GetUserDetails")]
        public async Task<ApplicationUser> GetUserDetails(string username)
        {
            return   await _userManager.FindByNameAsync(username);
        }

        //Triggered: user clicks save changes on profile edit page
        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateItem(string id, ApplicationUser user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound(new Response { Status = "Failed", Message = "User not Found" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        private bool ItemExists(string id)
        {
            return (_context.Users?.Any(u => u.Id == id)).GetValueOrDefault();
        }
    }
}
