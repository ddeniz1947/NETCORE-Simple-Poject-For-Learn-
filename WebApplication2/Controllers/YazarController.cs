using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    public class YazarController : Controller
    {
        private UserManager<ApplicationUser> _usermanager;
        private ApplicationDbContext _ctx;

        public YazarController(ApplicationDbContext _ctx, UserManager<ApplicationUser> _usermanager)
        {
            this._usermanager = _usermanager;
            this._ctx = _ctx;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        [Authorize]

        public JsonResult Yaziekle(string konu, string yazi, string resim)
        {
            try
            {
                ApplicationUser user = _usermanager.FindByNameAsync(HttpContext.User.Identity.Name).Result;
                var yeni_nesne = new Blog();
                yeni_nesne.Konu = konu;
                yeni_nesne.Yazi = yazi;
                yeni_nesne.Resim = resim;
                yeni_nesne.Yazar = user;
                _ctx.Blog.Add(yeni_nesne);
                _ctx.SaveChanges();
                return Json("false");
            }
            catch
            {
                return Json("false");

            }

        }
        [Authorize]
        public IActionResult Sil()
        {
            var yazilar = _ctx.Blog.Select(s => new BlogViewModel() { Yazar = s.Yazar, Konu = s.Konu, Resim = s.Resim, Id = s.Id }).ToList();
            ViewBag.yazilar = yazilar;
            return View();
        }
        [Authorize]
        [HttpGet]
        public JsonResult Yazisil(int id)
        {
            try
            {
                var silinecek_yazi = _ctx.Blog.Where(a => a.Id == id).FirstOrDefault();
                _ctx.Blog.Remove(silinecek_yazi);
                _ctx.SaveChanges();
                return Json("silindi");
            }
            catch
            {
                return Json("olmadi");
            }

        }
    }
}
