using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SalesSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Se està colocando el objeto "services" de la interface "services" para poder crear nuestro servicio, se llama al mètodo
            //AddDbContext que es un mètodo genèrico y se le pasa la clase "ApplicationDbContext", clase que se va a utilizar para poder vincular
            //nuestra base de datos que tenemos en nuestro servidor con la clase "ApplicationDbContext", esto significa que utilizando esta clase nosotros
            //tendremos acceso a esa base de datos y a todas las tablase que contenga la base de datos, luego a este mètodo le creamos un parámetro para poder crear opciones 
            //y poder crear una opción y luego on el paràmetro "options" vamos a llamar al mètodo"UseSqlServer" para poder crear la conexión al servidor SQL Server
            //, por este motivo este mètodo contiene el siguiente nombre y este mètodo como paràmetro recibe la cadena de conexiòn
            //y para poder obtener la cadena de conexión se ha colocado la siguiente propiedad(configuration) para poder llamar al siguiente mètodo(GetConnectionString)
            //que vamos a utlizar para poder obtener la cadena de conexiòn del siguiente elemento ("DefaultConnection") que tenemos en el archivo "appsettinggs.json"
            //De esta forma estamos obteniendo la cadena de conexión para poder conectarnos al servidor de SQL Server
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
           //---- services.AddDatabaseDeveloperPageExceptionFilter();
            //Aquí se está creando un servicio utilizando el método AddDefaultIdentity que recibe como clase la clase "IdentityUser", con este servicio es como nosotros estamos gestionando toda la informaciòn respecto a los usuarios que nosotros registremos en nuestro sistema,
            //pero necesitamos otro servicio para gestionar toda la informaciòn de los roles que  nosotros registremos en nuestro sistema, roles que nosotros le podremos proporcionar a todos los usuarios que estemos registrando
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            //Colocamos el objeto servicies de la interface servicies que se està utilizando para poder crear a colecciòn de servicio, con ese objeto llamamos al método "AddIdentity", mètodo genérico
            //Ahora a este método tenemos que pasarle dos clases, la primera clase IdentityUser(para adminsitrar los usuarios), IdentityRole(servicio para admisnistrar los roles), de esta forma ya hemos creado 2 servicios
           services.AddIdentity<IdentityUser, IdentityRole>()
          //----- services.AddDefaultIdentity<IdentityUser>(options => options.SignIn. RequireConfirmedAccount = true)
               .AddEntityFrameworkStores<ApplicationDbContext>(); //Para poder indicarle al framework quién va a gestionar toda esa información y nuestra base de datos se`rá la siguiete clase que hace referencia a la BD que nosotros tenemos en el Servidor SQL Server

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //Ocupamos el metodo MapAreaControllerRoute para poder crear nuetsra ruta o la tuberia que van a utilizar los controladores de nuestra area
                endpoints.MapAreaControllerRoute("Users","Users", "{controller=Users}/{action=Users}/{id?}"); //El primer parametro es el nombre que va a contener esta ruta(User), el segundo es el nombre de la area(User), el tercer parametro es el patron, por lo tanto lo copiamos(Vamos a ejecutar un controlador llamado "Users" que contiene el mismo nombre que la area y ejecutamos un metodo de accion con el mismo nombre)
                endpoints.MapRazorPages();
            });
        }
    }
}
