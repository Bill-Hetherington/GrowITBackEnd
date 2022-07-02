using GrowITBackEnd.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GrowITBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ItemsController(ApplicationDbContext context)
        {
            _context = context;            
        }
    }
}
