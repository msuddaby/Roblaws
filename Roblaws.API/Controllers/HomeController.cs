using Microsoft.AspNetCore.Mvc;

namespace JWTAuthTemplate.Controllers;

public class HomeController: Controller {
    public IActionResult Index() {
        return View();
    }
}