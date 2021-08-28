using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SalesSystem.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SalesSystem.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //Creamos este método(Crear roles), va a ejecutar una tarea y va a recibir un paràmetro de la siguiente interface
        private async Task CreateRoleAsync(IServiceProvider serviceProvider)
        {
            //Obtener un servicio: en donde Creamos el siguiente objeto roleManager, donde creamos el objeto "roleManager", le asignamos el paràmetro de la siguiente interface(serviceProvider) para poder llamar al siguiente mètodo(GetRequiredService) y poder obtener un servicio y obtendremos un servicio de la clase "IdentityRole" utilizando la clase RoleManager, clase que no permitirà administrar todos los roles que nosotros registremos en nuestro sistema
            //De esta manera vamos a obtener un servicio utilizando el mètodo "GetRequiredService", vamos a obtener el servicio utilizando <RoleManager> que va a gestionar toda la informaciòn respecto a los roles que nosotros registremos, y tenemos la clase <IdentityRole>, si nosotros seleccionamos esta clase y hacemos clic derecho(ir a su definición) podemos observar que esta clase contiene una sobrecarga de mètodo constructores que no recibe ningún parámetro, pero si tiene un método contructor que recibe un paràmetro donde nosotros vamos a pasarle el nombre de un role, está heredando de la clase IdentityRole genérica
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //Creamos un arreglo de tipo strig llamado "rolesName", inicializandolos con los siguientes datos, que serà la colecciòn de roles que vamos a registrar en el sistema, solo manejaremos 2 roles, administrador y usuario
            String[] roleName = { "Admin", "User" };
            //Ahora vamos a crear un foreach para poder recorrer eta colecciòn de roles
            foreach(var item in roleName)
                //Colocamos el objeto "rolesName", que es nuetsro arreglo, la que contiene la colección de roles, vamos a obtener cada elemento utilizando el objeto(item)
            {
                //Creamos una variable u objeto(roleExist) de tipo bool porque este método nos va a retornar un valor "verdadero o falso",
                //Se ha colocado la palabra "await" para poder indicarle al método crear roles que tiene que esperar al método "await" que realice la tarea porque este método va a ejecutar una tarea que será verificar si el role que nosotros estamos obteniendo de la siguiente colección de datos utilizando el "foreach" ya está registrado en la tabla roles
                //Una vez que finalice la tarea este método, el método CreateRoleAsync podrá continuar ejecutando su procedimiento
                //Se ha colocado la palabra await, luego el objeto "roleManager" que contiene el servicio, podemos llamar al siguiente método "RoleExistsAsync" para poder verificar los roles que le vamos a proporcionar  utilizando el objeto"item", se va a verificar el role que está en el objeto "item"(administrador o user), va a verificar si ese role ya está registrado en la tabla roles, si está registrado entonces este método nos va a devolver un valor verdadero  se va a almacenar en el objeto "roleExist", pero si el rol que  le estamos proporcionando no está registrado en la tabla role, el método "roleExistsAsync" va a devolver un valor falso
                var roleExist = await roleManager.RoleExistsAsync(item);
                //Ahora creamos una condiciòn donde vamos a evaluar la siguiente variable "roleExist" y vamos a utilizar el operador de negación para poder negar el resultado que contenga la variable "roleExist", si esta variable contiene el valor de verdadero, prácticamente es como si fuera un valor falso, pero si contiene el valor falso es como si fuera un valor vardadero y es la opción que nosotros necesitamos, si este método"RoleExistsAsync" nos devuele un valor falso es porque el rol no está registrado
                if(!roleExist)
                    {
                    //Colocamos la palabra "await" para poder indicarle al método que se sincronice con el método "CreateAsyncs" y que tiene que esperarlo hasta que ejecute su tarea, donde colocamos el objeto "roleManager" para poder llamar al método crear role(CreateAsync), este método recibe como parámettro un objeto de la clase "IdentityRole" y le vamos a pasar la instancia de la clase "IdentityRole" y a esta clase vamos a pasarle como paràmetro el rol que contenga el objeto"item".
                    //La clase IdentityRole contiene 2 métodos contructores, un método constructor que no recibe ningún parámetro  y un método constructor que si recibe un parámetro que es el nombre del rol,utilizamos este método contructor, le pasamos el nombre del rol para poder crear ese rol en la siguiente tabla
                    await roleManager.CreateAsync(new IdentityRole(item));
                }


            }
        }
    }
}
