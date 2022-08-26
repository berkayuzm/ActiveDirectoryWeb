using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using WebApplicationAD.Models;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using DirectoryEntry = System.DirectoryServices.DirectoryEntry;
using System.Reflection.Emit;

namespace WebApplicationAD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Index(string kullaniciadi, string password)
        {
            ViewBag.Name = string.Format("Name: {0} {1}", kullaniciadi, password);
            Console.WriteLine(ViewBag.Name);
            //1. login 
            //DirectoryEntry myDomain=ADConnection.login1(kullaniciadi, password);
            //bool loginBasarili = false;
            //try {
            //    if (myDomain.Properties.Count > 0)
            //    {
            //        loginBasarili = true;
            //        Console.WriteLine(myDomain.Properties.ToString());
            //    }
            //    if (loginBasarili)
            //    {
            //        ViewBag.kullaniciadi = kullaniciadi;
            //       return RedirectToAction("success");
            //    }
            //}
            //catch {

            //   return RedirectToAction("unsuccess");
            //}

            
          DomainBilgileri db =ADConnection.login2(kullaniciadi, password);
            Console.WriteLine("Oturum açma durum==>"+db.AdiSoyadi);
            if (db.isAccessSuccess)
            {
               
                return RedirectToAction("success",new RouteValueDictionary(new {Controller="home",Action="success",obj=db.AdiSoyadi,mail=db.Email,phoneNumber=db.PhoneNumber,description=db.Description}));
            }
            else
            {
                return RedirectToAction("unsuccess");
            }
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Success(string obj,string mail,string phoneNumber,string description)
        {
            ViewBag.Adi = obj;
            ViewBag.mail = mail;
            ViewBag.phoneNumber = phoneNumber;
            ViewBag.description = description;
            Console.WriteLine("fjasldkjfş==>"+mail);
            return View();
        }
        public IActionResult Unsuccess()
        {
            return View();
        }
    }
}