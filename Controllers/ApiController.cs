using Microsoft.AspNetCore.Mvc;
using MSIT158Site.Models;
using Newtonsoft.Json;

namespace MSIT158Site.Controllers
{
    public class ApiController : Controller
    {
     private readonly MyDBContext _context;
        
        public ApiController(MyDBContext context)// 建構函數，通過依賴注入注入資料庫上下文
        {
            _context = context;
        }



        public IActionResult Index()
        {
            System.Threading.Thread.Sleep(5000);
            return Content("<h2>世界, 您好!!</h2>","text/html", System.Text.Encoding.UTF8);
        }

        public IActionResult Cities()
        {
            var cities = _context.Addresses.Select(a => new { a.City, a.SiteId, a.Road }).ToList().Distinct();
            return Json(cities);// 以 JSON 格式返回城市清單
        }
        public IActionResult Avatar(int id = 1)
        {
            // 從資料庫中查找對應 id 的會員資料
            Member? member = _context.Members.Find(id);
            // 如果找到會員資料
            if (member != null)
            {
                // 取得會員的圖片資料
                byte[] img = member.FileData;
                // 如果圖片資料不為空，返回圖片
                if (img != null)
                {
                    return File(img, "image/jpeg");
                }
            }
            // 如果沒有找到會員資料或圖片資料，返回 404 Not Found
            return NotFound();
        }

        public IActionResult Register(int id,string name,int age = 20)
        {
            if  (string.IsNullOrEmpty(name))
            {
                name = "guest";
            }
            return Content($"{id} - {name} 你好!你 {age} 歲了", "text/html", System.Text.Encoding.UTF8);
        }

    }
}
