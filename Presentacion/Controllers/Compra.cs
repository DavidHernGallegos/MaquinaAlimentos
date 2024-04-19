using BL;
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

        public IActionResult TusCompras(int? IdAlimento) 
        {

            var usuarioDeserializado = JsonConvert.DeserializeObject<BL.Usuario>(HttpContext.Session.GetString("Usuario"));

            Dictionary<string, object> diccionario = BL.Compra.GetComprasById(usuarioDeserializado.IdUsuario, IdAlimento);
            bool respuesta = (bool)diccionario["Respuesta"];
            BL.Compra compra = (BL.Compra)diccionario["Compra"];
            var cantidadProductos = compra.Compras.Count();

            if(respuesta)
            {
                Dictionary<string, object> diccionarioAlimentos = BL.Alimento.GetAll();
                BL.Alimento alimento = (BL.Alimento)diccionarioAlimentos["Alimento"];
                ViewBag.IdAlimento = alimento.IdAlimento;
                ViewBag.ListaAlimento = alimento.Alimentos;
                //var a =  alimento.Alimentos.Select(a => a.)).ToList();
                ViewBag.CantidadCompras = cantidadProductos;
                return View(compra);
            }


         
            return View();
        
        }



        public IActionResult ResumenCompra(int? idCompra)
        {

            var usuarioDeserializado = JsonConvert.DeserializeObject<BL.Usuario>(HttpContext.Session.GetString("Usuario"));

            Dictionary<string, object> diccionario = BL.Compra.GetComprasByIdCompra(idCompra.Value);
            bool respuesta = (bool)diccionario["Respuesta"];
            BL.Compra compra = (BL.Compra)diccionario["Compra"];
            var cadenaCambio = "";
            if (compra.Cambio == null)
            {
                ViewBag.Cambio = cadenaCambio;
            }
            else
            {
                Dictionary<int, int> diccionarioCambio = BL.Alimento.CalcularBilletesYMonedas(compra.Cambio.Value);
                
                    foreach (var item in diccionarioCambio)
                    {
                        BL.Moneda monedaObj = new BL.Moneda();

                        if (item.Key >= 50)
                        {

                            monedaObj.Denominacion = item.Key;
                            monedaObj.Cantidad = item.Value;




                            cadenaCambio = cadenaCambio + $",Billete {item.Key} = {item.Value} ";
                        }
                        else
                        {
                            monedaObj.Denominacion = item.Key;
                            monedaObj.Cantidad = item.Value;




                            cadenaCambio = cadenaCambio + $",Moneda {item.Key} = {item.Value}";
                        }

                    }

                }

                if (respuesta)
                {
                //Dictionary<string, object> diccionarioAlimentos = BL.Alimento.GetAll();
                //BL.Alimento alimento = (BL.Alimento)diccionarioAlimentos["Alimento"];
                //ViewBag.IdAlimento = alimento.IdAlimento;
                //ViewBag.ListaAlimento = alimento.Alimentos;
                //var a =  alimento.Alimentos.Select(a => a.)).ToList();
                ViewBag.Cambio = cadenaCambio;
                return View(compra);
                }
                else
                {
                    return View(); 
                }



            
        }

        public IActionResult ResumenCompras()
        {
            var usuarioDeserializado = JsonConvert.DeserializeObject<BL.Usuario>(HttpContext.Session.GetString("Usuario"));

            Dictionary<string, object> diccionario = BL.Compra.GetComprasById(usuarioDeserializado.IdUsuario,1);
            bool respuesta = (bool)diccionario["Respuesta"];
            BL.Compra compra = (BL.Compra)diccionario["Compra"];
            
            List<object> listaCompra1 = compra.Compras;
            List<object> listaCompra2 = compra.Compras;

            var count = 0;
            foreach(BL.Compra objCompra in listaCompra1)
            {
                foreach(BL.Compra objCompra2 in listaCompra2)
                {
                    if(objCompra2.Alimento.IdAlimento == 1)
                    {
                        count++;
                    }
                    else
                    {

                    }
                }
            }



            if (respuesta)
            {
              
                return View(compra);
            }



            return View();

        }
    }
}
