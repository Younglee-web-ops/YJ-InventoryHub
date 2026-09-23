using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/data")]
    public class DataController : ControllerBase
    {
        private static List<InventoryItem> items = new List<InventoryItem>
        {
            new InventoryItem { Id = 1, Name = "노트북", Quantity = 10, Price = 1200000 },
            new InventoryItem { Id = 2, Name = "마우스", Quantity = 50, Price = 25000 }
        };

        private static List<User> users = new List<User>
        {
            new User { Id = 1, Name = "홍길동", Email = "hong@example.com" }
        };

        // ---------------- Inventory ----------------

        [HttpGet("inventory")]
        public ActionResult<List<InventoryItem>> GetAllInventory()
        {
            try
            {
                Response.Headers.Append("Cache-Control", "public, max-age=30");
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"서버 오류: {ex.Message}");
            }
        }

        [HttpGet("inventory/{id}")]
        public ActionResult<InventoryItem> GetInventoryById(int id)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item == null) return NotFound($"ID {id}에 해당하는 항목을 찾을 수 없습니다.");
            return Ok(item);
        }

        [HttpPost("inventory")]
        public ActionResult<InventoryItem> CreateInventory(InventoryItem newItem)
        {
            if (string.IsNullOrWhiteSpace(newItem.Name) || newItem.Quantity < 0 || newItem.Price < 0)
            {
                return BadRequest("이름, 수량, 가격을 올바르게 입력해주세요.");
            }
            newItem.Id = items.Count > 0 ? items.Max(i => i.Id) + 1 : 1;
            items.Add(newItem);
            return CreatedAtAction(nameof(GetInventoryById), new { id = newItem.Id }, newItem);
        }

        [HttpPut("inventory/{id}")]
        public IActionResult UpdateInventory(int id, InventoryItem updatedItem)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item == null) return NotFound();
            item.Name = updatedItem.Name;
            item.Quantity = updatedItem.Quantity;
            item.Price = updatedItem.Price;
            return NoContent();
        }

        [HttpDelete("inventory/{id}")]
        public IActionResult DeleteInventory(int id)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item == null) return NotFound();
            items.Remove(item);
            return NoContent();
        }

        // ---------------- Users ----------------

        [HttpGet("users")]
        public ActionResult<List<User>> GetAllUsers()
        {
            try
            {
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"서버 오류: {ex.Message}");
            }
        }

        [HttpGet("users/{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            try
            {
                var user = users.FirstOrDefault(u => u.Id == id);
                if (user == null) return NotFound($"ID {id}에 해당하는 사용자를 찾을 수 없습니다.");
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"서버 오류: {ex.Message}");
            }
        }

        [HttpPost("users")]
        public ActionResult<User> CreateUser(User newUser)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(newUser.Name))
                {
                    return BadRequest("이름은 필수입니다.");
                }
                if (string.IsNullOrWhiteSpace(newUser.Email) || !Regex.IsMatch(newUser.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    return BadRequest("유효한 이메일 주소를 입력해주세요.");
                }
                newUser.Id = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1;
                users.Add(newUser);
                return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"서버 오류: {ex.Message}");
            }
        }

        [HttpPut("users/{id}")]
        public IActionResult UpdateUser(int id, User updatedUser)
        {
            try
            {
                var user = users.FirstOrDefault(u => u.Id == id);
                if (user == null) return NotFound($"ID {id}에 해당하는 사용자를 찾을 수 없습니다.");
                if (string.IsNullOrWhiteSpace(updatedUser.Name))
                {
                    return BadRequest("이름은 필수입니다.");
                }
                if (string.IsNullOrWhiteSpace(updatedUser.Email) || !Regex.IsMatch(updatedUser.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    return BadRequest("유효한 이메일 주소를 입력해주세요.");
                }
                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"서버 오류: {ex.Message}");
            }
        }

        [HttpDelete("users/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var user = users.FirstOrDefault(u => u.Id == id);
                if (user == null) return NotFound($"ID {id}에 해당하는 사용자를 찾을 수 없습니다.");
                users.Remove(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"서버 오류: {ex.Message}");
            }
        }
    }
}