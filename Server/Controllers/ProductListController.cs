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
            // Copilot 제안: 서버 부하 감소를 위해 응답 캐싱 헤더 추가 (30초)
            Response.Headers.Append("Cache-Control", "public, max-age=30");
            return Ok(products);
        }
    }
}