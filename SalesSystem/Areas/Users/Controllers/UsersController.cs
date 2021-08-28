using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesSystem.Areas.Users.Controllers
{
    //Especificamos al controlador que se encuentra en la área Users, cada vez que llamamos al área User y el nombre del controlador se va a invocar al controlador Users
    [Area("Users")]
    public class UsersController : Controller
    {
        public IActionResult Users()
        {
            return View();
        }
    }
}
