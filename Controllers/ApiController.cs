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

        //檢查帳號是否存在
        public IActionResult CheckAccount(string name)
        {
            var member = _context.Members.Any(m => m.Name == name);

            return Content(member.ToString(), "text/plain", System.Text.Encoding.UTF8);
        }

        public IActionResult Index()
        {
            System.Threading.Thread.Sleep(5000);
            return Content("<h2>世界, 您好!!</h2>","text/html", System.Text.Encoding.UTF8);
        }

        //public IActionResult Cities()
        //{
        //    var cities = _context.Addresses.Select(a => new { a.City, a.SiteId, a.Road }).ToList().Distinct();
        //    return Json(cities);// 以 JSON 格式返回城市清單
        //}

        //讀出不會重複的城市名
        public IActionResult Cities()
        {
            var cities = _context.Addresses.Select(a => a.City).Distinct();
            return Json(cities);
        }

        //根據城市名讀出不會重複的鄉鎮區
        public IActionResult Districts(string city)
        {
            var districts = _context.Addresses.Where(a => a.City == city).Select(a => a.SiteId).Distinct();
            return Json(districts);
        }
        //根據鄉鎮區讀出路名
        public IActionResult Roads(string districts)
        {
            var roads = _context.Addresses.Where(a => a.SiteId == districts).Select(a => a.Road);
            return Json(roads);
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
