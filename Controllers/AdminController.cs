using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEB.Models;
namespace WEB
{
    public class AdminController : Controller
    {
        public IActionResult EnterAdmin() // страница на внесение приложений
        {
            return View();
        }

        [HttpPost]
        public IActionResult EnterAdmin(CreateAppViewModel modelView) // вносит приложения
        {
            if (ModelState.IsValid)
            {
                AppS appS = new AppS()
                {
                    Name = modelView.Name,
                    Company = modelView.Company,
                    Tag = modelView.Tag,
                    Mark = modelView.Mark,
                    Price = modelView.Price,
                    IconSource = modelView.IconSource,
                    ImageSource = modelView.ImageSource,
                    DescriptionSource = modelView.DescriptionSource
                };
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    context.Apps.Add(appS);
                    context.SaveChanges();
                }
                // здесь бд добавляет приложение
            }
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
