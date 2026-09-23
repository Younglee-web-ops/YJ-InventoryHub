using Microsoft.AspNetCore.Mvc;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/productlist")]
    public class ProductListController : ControllerBase
    {
        private static List<Product> products = new List<Product>
        {
            new Product
            {
                Id = 1, Name = "노트북", Price = 1200000, Stock = 10,
                Category = new Category { Id = 101, Name = "전자제품" }
            },
            new Product
            {
                Id = 2, Name = "마우스", Price = 25000, Stock = 50,
                Category = new Category { Id = 102, Name = "액세서리" }
            }
        };

        [HttpGet]
        public ActionResult<List<Product>> GetAll()
        {
            return Ok(products);
        }
    }
}