﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjAJAXDemo.Models;
using prjAJAXDemo.ViewModels;

namespace prjAJAXDemo.Controllers
{
    public class apiController : Controller
    {
        private readonly DemoContext _context; //指向資料庫的物件
        private readonly IWebHostEnvironment _host;//用於獲取網站主機環境的訊息，構件文件路徑

        public apiController(DemoContext context, IWebHostEnvironment host)
        { 
            _context = context;
            _host = host;

        }
        public IActionResult Index()
        {
            System.Threading.Thread.Sleep(5000);
            return Content("Hello AJAX"); 
        }

        //public IActionResult GetDemo(string name, int age)
        public IActionResult GetDemo(UserVM user)
        {
            if (string.IsNullOrEmpty(user.name))
            {
                user.name = "Guest";
            }
            return Content($"這是一個GET的測試{user.name}，你是{user.age}歲");
        }

        public IActionResult Register(Members member, IFormFile file)//IFormFile物件代表被上傳的檔案
        {

            //return Content($"{_host.ContentRootPath}");  //C:\shared\Ajax\MSIT150Site\
            //   return Content($"{_host.WebRootPath}");  //C:\shared\Ajax\MSIT150Site\wwwroot
            string filePath = Path.Combine(_host.WebRootPath, "uploads", file.FileName); //C:\shared\Ajax\MSIT150Site\wwwroot\uploads\walk.gif

            //上傳檔案到uploads資料夾中
            using (var fileStream = new FileStream(filePath, FileMode.Create))//FileStream用來寫入新建立的檔案
            {
                file.CopyTo(fileStream);
            }
            //將圖轉成二進位
            byte[]? imageByte = null;
            using (var memoryStream = new MemoryStream())//MemoryStream 物件，這是一個在記憶體中創建的資料流，用於暫存檔案的數據
            {
                file.CopyTo(memoryStream);
                imageByte = memoryStream.ToArray();
            }
            member.FileName = file.FileName;
            member.FileData = imageByte;
            _context.Members.Add(member);
            _context.SaveChanges();
            return Content($"上傳檔案到{filePath}新增成功!!");
            //  return Content($"{file.FileName} - {file.Length} - {file.ContentType}");

        }

        public IActionResult GetImageByte(int id = 1)
        {
            Members? member = _context.Members.Find(id);
            byte[]? img = member.FileData;
            return File(img, "image/jpeg");

        }


        public IActionResult CheckAccount(string name)
        {
            Members m = _context.Members.FirstOrDefault(item => item.Name == name);
            bool exists = m != null;
            return Json(new { exists });
        
        }


        //=========================
        public IActionResult Cities()
        {
            
            var cities = _context.Address.Select(c => c.City).Distinct();
            return Json(cities);
        }





        public IActionResult Districts(string city)
        {
            var districts = _context.Address.Where(a => a.City == city).Select(c => c.SiteId).Distinct();
            return Json(districts);
        }


        public IActionResult Roads(string siteId)
        {
            var roads = _context.Address.Where(a => a.SiteId == siteId).Select(c => c.Road).Distinct();
            return Json(roads);
        }


    }
}
