using BL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Presentacion.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string? email , string? contraseña)
        {
            if(email == null && contraseña == null)
            {
                return View();
            }
            else
            {
                Dictionary<string, object> resultado = BL.Usuario.Login(email, contraseña);
                bool respuesta = (bool)resultado["Respuesta"];
                if (respuesta)
                {
                    Dictionary<string, object> resultadoId = BL.Usuario.GetByEmail(email);
                    bool respuestaId = (bool)resultadoId["Respuesta"];
                    BL.Usuario usuario = new BL.Usuario();
                    if (respuestaId)
                    {
                        usuario = (BL.Usuario)resultadoId["Usuario"];
                    }

                    HttpContext.Session.SetString("Usuario", Newtonsoft.Json.JsonConvert.SerializeObject(usuario));
                    TempData["usuario"] = JsonConvert.SerializeObject(usuario);
                   
                    return RedirectToAction("GetAll", "Alimento");
                }
            }
           


            return View();
        }
    }
}
