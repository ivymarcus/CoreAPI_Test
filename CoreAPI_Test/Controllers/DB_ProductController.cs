using CoreAPI_Test.Models;
using CoreAPI_Test.Repostiroy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreAPI_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DB_ProductController : ControllerBase
    {
        private readonly DB_ProductRepository _dbResp;
        public DB_ProductController(IConfiguration config)
        {
            var conn = config.GetConnectionString("TestDB");
            _dbResp = new DB_ProductRepository(conn);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DB_ProductModel>>> GetAllProduct()
        {
            IEnumerable<DB_ProductModel> model = await _dbResp.GetAllProduct();
            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DB_ProductModel>> GetProductBtId(int id)
        {
            var model = await _dbResp.GetProductById(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }
    }
}
