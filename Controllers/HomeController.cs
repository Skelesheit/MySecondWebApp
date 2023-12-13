using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEB.Models;

namespace WEB.Controllers
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
            User user = new User()
            {
                Name = "null"
            };
            if (Request.Cookies.TryGetValue("UserId", out string userId))
            {
                int id = int.Parse(userId);
                using (var context = new ApplicationDbContext())
                {
                    user = context.Users.FirstOrDefault(user => user.Id == id);
                }
            }

            Dictionary<string, List<AppS>> Dict = new();
            Dict["Free"] = new List<AppS>();
            Dict["Music"] = new List<AppS>();
            Dict["Creation"] = new List<AppS>();
            Dict["SocialNetwork"] = new List<AppS>();
            List<AppS> apps = new List<AppS>();
            using (var context = new ApplicationDbContext())
            {
                apps = context.Apps.ToList();
            }
            foreach (var app in apps)
            {
                Dict[app.Tag].Add(app);
                if (app.Price == 0)
                {
                    Dict["Free"].Add(app);
                }
            }

            return View(model: (user, Dict));
        }

        public IActionResult ShowApps() // представление приложений
        {
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
        [HttpGet("/Home/Apps/{request?}")]
        public IActionResult Apps(string request)
        {
            bool result = int.TryParse(request, out int appId); // если ввёден определённый id

            if (result)
            {
                return ShowAppId(appId);
            }
            else
            {
                List<AppS> apps = new List<AppS>();
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    apps = context.Apps.Where(x => x.Tag == request).ToList();
                }
                return View(apps);
            }

        }

        public IActionResult ShowAppId(int id)
        {
            AppS ChooseApp = new AppS();
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                ChooseApp = context.Apps.FirstOrDefault(x => x.Id == id);
            }
            if (Request.Cookies.TryGetValue("UserId", out string userId))
            {
                int idUser = int.Parse(userId);
                User user;
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    user = context.Users.FirstOrDefault(x => x.Id == idUser);
                }
                return View("ShowAppId", (ChooseApp, user));
            }
            return RedirectToAction("ValidationView", "Account");
            
        }

        [HttpGet("/Home/Apps/Install/{id?}")]
        public IActionResult InstallApp(int id)
        {
            if (Request.Cookies.TryGetValue("UserId", out string userId))
            {
                int idUser = int.Parse(userId);
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    User user = context.Users.FirstOrDefault(x => x.Id == idUser);
                    if (user != null)
                    {
                        List<string> installApps = user.IstallApps.Split(',').ToList();
                        if (!installApps.Contains(id.ToString()))
                        {
                            installApps.Add(id.ToString());
                            user.IstallApps = String.Join(',', installApps);

                            context.Users.Update(user);
                            context.SaveChanges();
                        }
                    }

                    return RedirectToAction("Apps", "Home");
                }
            }

            return RedirectToAction("ValidationView", "Account");
        }

        [HttpGet("/Home/Apps/Delete/{id?}")]
        public IActionResult DeleteApp(int id)
        {
            if (Request.Cookies.TryGetValue("UserId", out string userId))
            {
                int idUser;
                if (int.TryParse(userId, out idUser))
                {
                    using (ApplicationDbContext context = new ApplicationDbContext())
                    {
                        User user = context.Users.FirstOrDefault(x => x.Id == idUser);
                        if (user != null)
                        {
                            List<string> installApps = user.IstallApps.Split(',').ToList();
                            installApps.Remove(id.ToString());
                            user.IstallApps = String.Join(',', installApps);

                            context.Users.Update(user);
                            context.SaveChanges();

                            return RedirectToAction("ShowAppId", "Home", new { id = id });
                        }
                    }
                }
            }

            return RedirectToAction("PersonalAccount", "Account");
        }

    }
}