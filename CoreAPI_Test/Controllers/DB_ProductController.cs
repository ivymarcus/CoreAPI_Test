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


        [HttpPost("add")]
        public async Task<IActionResult> PostProduct(DB_ProductModel model)
        {
            int result = await _dbResp.InsertProduct(model);
            if (result > 0)
            {
                return CreatedAtAction(nameof(GetProductBtId), new { id = model.ProductID }, model);
            }
            return BadRequest();
        }

        // PUT: api/[controller]/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, DB_ProductModel model)
        {
            if (id != model.ProductID)
            {
                return BadRequest();
            }
            int reslt = await _dbResp.UpdateProduct(model);
            if (reslt > 0)
            {
                // 通常用於 PUT、PATCH 或 DELETE 操作，表示請求成功 但無須回傳內容給用戶端
                // 不會包含任何主體
                return NoContent();
            }
            return NotFound();
        }

        // Delete: api/[controller]/5
        //[HttpDelete]                    // https://localhost:7074/api/DB_Product?id=80  (會有 ?後 帶參數)
        [HttpDelete("D/{id}")]            // https://localhost:7074/api/DB_Product/D/90   (直接 帶 id)
        public async Task<IActionResult> DeleteProduct(int id)
        {
            int result = await _dbResp.DeleteProduct(id);
            if (result > 0)
            {
                return NoContent();
            }
            return NotFound();
        }


    }
}
