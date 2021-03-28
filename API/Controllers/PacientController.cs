using API.Data;

namespace API.Controllers
{
    public class PacientController : BaseApiController
    {
        private readonly DataContext _context;
        public PacientController(DataContext context)
        {
            _context = context;
        }

        
    }
}