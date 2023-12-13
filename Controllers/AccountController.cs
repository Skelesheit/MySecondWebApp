using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEB.Models;
namespace WEB
{
    public class AccountController : Controller
    {
        
        [HttpGet]
        public IActionResult RegisterView()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterView(RegistryModel model)
        {
            if (model == null) return View();
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = PasswordHashing.HashPassword(model.Password),
                    IstallApps = ""
                };
                User userExist;
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    userExist = context.Users.FirstOrDefault(x => x.Name == user.Name); // проверяем есть ли такой же user  в бд
                    if (userExist == null)
                    {
                        context.Users.Add(user);
                        context.SaveChanges();
                    }
                    user = context.Users.FirstOrDefault(x => x.Name == user.Name); // узнаём id нового пользователя
                }
                Response.Cookies.Append("UserId", user.Id.ToString());
                return RedirectToAction("PersonalAccount", "Account");
            }
            return View();
        }

        [HttpGet]
        public IActionResult ValidationView()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ValidationView(ValidationModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Email = model.Email,
                    Password = PasswordHashing.HashPassword(model.Password)
                };
                User userCheck;
                
                List<AppS> apps = new List<AppS>();
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    userCheck = context.Users.FirstOrDefault(x => x.Email == user.Email & x.Password == user.Password);
                    
                }
                if (userCheck != null)
                {
                    Response.Cookies.Append("UserId", userCheck.Id.ToString());
                    return RedirectToAction("PersonalAccount", "Account");
                }
            }

            return View();
        }

        public IActionResult PersonalAccount()
        {
            if (Request.Cookies.TryGetValue("UserId", out string userId))
            {
                User user;
                int id = int.Parse(userId);
                List<AppS> apps = new List<AppS>();
                using (var context = new ApplicationDbContext())
                {
                    user = context.Users.FirstOrDefault(user => user.Id == id);
                    List<int> IdApp;
                    if(user.IstallApps != String.Empty)
                    {
                        IdApp = user.IstallApps.Split(',').Select(x => int.Parse(x)).ToList();
                        foreach (int appId in IdApp)
                        {
                            var appS = context.Apps.FirstOrDefault(x => x.Id == appId);
                           
                            apps.Add(appS);
                        }
                    }
                    
                }
                return View(model: (user, apps));
            }

            return RedirectToAction("ValidationView", "Account");

        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
