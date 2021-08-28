using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SalesSystem.Areas.Users.Models;

namespace SalesSystem.Areas.Users.Pages.Account
{
    public class RegistrarModel : PageModel
    {
        public void OnGet()
        {

        }

        //Creamos una propiedad de la clase input model porque vamos a utilizar esta propiedad para poder tener acceso a los elementos de la clase InputModel y tendremos acceso desde la interfaz usuario osea desde la sig. vista(Registrar.cshtml) tendremos acceso a esa propiedad InputModel 
        [BindProperty]
        public InputModel Input { get; set; }

        //Para tener acceso a los elementos que definimos del formulario(nombre, apellido, Nid, numero telefonico...) tenemos que poner el nombre del modelo en donde definimos los elementos
        public class InputModel : InputModelRegister    //Esta clase (InputModel) está heredando de InputModelRegister, esto significa que con el objeto(Input) tendremos acceso a los elemetos de la clase InputModelRegister.cs
        {
            // Propiedad de la interfaz que nos va a ayudar a gestionar toda la información de los archivos que nosotros cargemos utilizando el siguiente formulario y el siguiente input.
            public IFormFile AvatarImage { get; set; }

            //Creamos la propiead que vamos a ocupar en Registrar.cshtml
            public string ErrorMessage { get; set; }
        }
    }
}
