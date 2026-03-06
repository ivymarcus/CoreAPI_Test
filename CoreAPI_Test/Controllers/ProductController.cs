using CoreAPI_Test.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreAPI_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        List<ProductModel> productModels = new List<ProductModel>()
        {
            new ProductModel {ID = 1, ProductName = "蘋果", Category = "水果", Price = 10 },
            new ProductModel {ID = 2, ProductName = "香蕉", Category = "水果", Price = 20},
            new ProductModel {ID = 3, ProductName = "電腦", Category = "電器", Price = 50},
            new ProductModel {ID = 4, ProductName = "電視", Category = "電器", Price = 100},
            new ProductModel {ID = 5, ProductName = "滑鼠", Category = "其他", Price = 90}
        };

        [HttpGet(Name = "GetlProducts")]
        public IEnumerable<ProductModel> GetAllProduct()
        {
            return productModels;
        }

        [HttpGet("{id:int}", Name = "GetProductById")]
        public ActionResult<ProductModel> GetProductById(int id) 
        {
            var product = productModels.Where(x => x.ID == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            return product;

        }

        [HttpPost]
        public ActionResult<ApiResponseModel> CreateProduct([FromBody]ProductModel newProduct)
        {
            if (newProduct == null)
            {
                return BadRequest("產品資料不能為空");
            }
            int newId = productModels.Count()+1;
            newProduct.ID = newId;
            productModels.Add(newProduct);

            ApiResponseModel apiResponseModel = new ApiResponseModel() { Status = 1, Message = "建立成功" };
            return apiResponseModel;

            /*
            ProductModel? tempProduct = productModels.Where(x => x.ProductName == newProduct.ProductName).FirstOrDefault();
            if (tempProduct == null)
            {
                return BadRequest("查無此商品");
            }

            int index = productModels.FindIndex(x => x.ProductName == newProduct.ProductName);
            if(index != -1)
            {
                productModels[index].Category = newProduct.Category;
                productModels[index].Price = newProduct.Price;
            }

            return productModels[index];
            */
        }

    }
}
