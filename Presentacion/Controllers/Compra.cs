using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Presentacion.Controllers
{
    public class Compra : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CompraAlimento()
        {


            return View();
        }

        public IActionResult TusCompras() 
        {

            var usuarioDeserializado = JsonConvert.DeserializeObject<BL.Usuario>(HttpContext.Session.GetString("Usuario"));

            Dictionary<string, object> diccionario = BL.Compra.GetComprasById(usuarioDeserializado.IdUsuario);
            bool respuesta = (bool)diccionario["Respuesta"];
            BL.Compra compra = (BL.Compra)diccionario["Compra"];
            var cantidadProductos = compra.Compras.Count();

            if(respuesta)
            {
                ViewBag.CantidadCompras = cantidadProductos;
                return View(compra);
            }



            return View();
        
        }
    }
}
