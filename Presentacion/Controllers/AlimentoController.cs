using BL;
using DL;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Presentacion.Controllers
{
    public class AlimentoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll(int? valor)
        {

           
            if (valor == null)
            {
                ViewBag.Respuesta = false;
            }
            else
            {
                if (TempData["alert"].Equals(true))
                {
                    ViewBag.Respuesta = true;
                }
                

            }

            Dictionary<string, object> resultado = BL.Alimento.GetAll();
            bool respuesta = (bool)resultado["Respuesta"];

            if (respuesta)
            {
                BL.Alimento alimento = (BL.Alimento)resultado["Alimento"];
              

                return View(alimento);
            }
            else
            {
                return PartialView("Modal");
            }
            
        }


        public IActionResult PagarAlimento(int idAlimento)
        {
            Dictionary<string, object> resultado = BL.Alimento.GetById(idAlimento);

            bool respuesta = (bool)resultado["Respuesta"];

            if (respuesta)
            {
                BL.Alimento alimento = (BL.Alimento)resultado["Alimento"];

                return View(alimento);
            }
            else
            {
                return PartialView("Modal");
            }

          
        }

        [HttpPost]
        public IActionResult ProcesarPago(int precio, int Insertadas, int? cantidad10, int? cantidad50, int? cantidad100, int IdAlimento)
        {

           BL.Moneda moneda = new BL.Moneda();
           var usuarioDeserializado = JsonConvert.DeserializeObject<BL.Usuario>(TempData["usuario"].ToString());

            if(cantidad10 == null)
            {
                cantidad10 = 0;
            }
            if(cantidad50 == null)
            {
                cantidad50 = 0;
            }
            if (cantidad100 == null)
            {
                cantidad100 = 0;
            }

            Dictionary<string, object> diccionarioCompra = BL.Compra.RealizarCompra(usuarioDeserializado.IdUsuario, IdAlimento, precio, cantidad10.Value, cantidad50.Value, cantidad100.Value);
            bool respuesta = (bool)diccionarioCompra["Respuesta"];
            string mensaje = (string)diccionarioCompra["Mensaje"];
            if (respuesta)
            {
                if (precio < Insertadas)
                {
                    int cambio = Insertadas - precio;
                    Dictionary<int, int> diccionarioCambio = BL.Alimento.CalcularBilletesYMonedas(cambio);
                    var cadenaCambio = "";

                    moneda.Monedas = new List<object>();
                    foreach (var item in diccionarioCambio)
                    {
                        BL.Moneda monedaObj = new BL.Moneda();

                        if (item.Key >= 50)
                        {

                            monedaObj.Denominacion = item.Key;
                            monedaObj.Cantidad = item.Value;

                            moneda.Monedas.Add(monedaObj);


                            cadenaCambio = cadenaCambio + $",Billete {item.Key} = {item.Value} ";
                        }
                        else
                        {
                            monedaObj.Denominacion = item.Key;
                            monedaObj.Cantidad = item.Value;

                            moneda.Monedas.Add(monedaObj);


                            cadenaCambio = cadenaCambio + $",Moneda {item.Key} = {item.Value}";
                        }
                    }


                    ViewBag.Mensaje = $"Tu cambio es de: {cambio} {cadenaCambio}";
                    //return PartialView("Modal");

                    TempData["moneda"] = JsonConvert.SerializeObject(moneda.Monedas);

                    ViewBag.ShowAlert = false;

                    return RedirectToAction("DevolverCambio");
                }
                else if (precio > Insertadas)
                {

                    int faltante = precio - Insertadas;
                    ViewBag.Mensaje = $"Un falta cubir el precio total {faltante}";
                    return PartialView("Modal");
                }
                else
                {
                    TempData["alert"] = true;
                    return RedirectToAction("GetAll", new {valor = 1});
                }
            }
            else
            {
                ViewBag.Mensaje = mensaje;
                return PartialView("Modal");
            }


           
          
        }

        public IActionResult DevolverCambio()
        {
            Moneda moneda = new Moneda();
            int suma = 0;
            var monedaDeserializada = JsonConvert.DeserializeObject<List<object>>(TempData["moneda"].ToString());

            moneda.Monedas = new List<object>();

            foreach(var item in monedaDeserializada)
            {
                Moneda monedaDes  = Newtonsoft.Json.JsonConvert.DeserializeObject<Moneda>(item.ToString());
                moneda.Monedas.Add(monedaDes);

                suma = suma + (monedaDes.Denominacion.Value*monedaDes.Cantidad.Value);
            }

            ViewBag.Mensaje= $"Tu cambio es de: {suma}";


            return View(moneda);
        }
    }
}
