using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Moneda
    {
        public int IdMoneda { get; set; }

        public int? Cantidad { get; set; }

        public int? Denominacion { get; set; }

        public int? Denominacion100 { get; set; }
        public int? Denominacion50 { get; set; }
        public int? Denominacion10 { get; set; }

        public int? TotalDinero { get; set; }

        public List<object> Monedas { get; set; }

        //public static Dictionary<string, object> GetAll()
        //{
        //    Moneda moneda = new Moneda();
        //    Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Respuesta", false }, { "Mensaje", "" }, { "Moneda", moneda } };

        //    try
        //    {
        //        using (DL.MaquinaAlimentosContext context = new DL.MaquinaAlimentosContext())
        //        {
        //            var query = (from Monedas in context.Moneda
        //                         select new
        //                         {
        //                             IdMoneda = Monedas.IdMoneda,
        //                             Cantidad = Monedas.Cantidad,
                                     

        //                         }).ToList();

        //            if (query != null)
        //            {

        //                moneda.Monedas = new List<object>();
        //                foreach (var item in query)
        //                {
        //                    Moneda monedaObj = new Moneda();
        //                    monedaObj.IdMoneda = item.IdMoneda;
        //                    monedaObj.Cantidad = item.Cantidad;
                           
        //                    moneda.Monedas.Add(monedaObj);
        //                }

        //                diccionario["Respuesta"] = true;
        //                diccionario["Mensaje"] = "Se cargaron los alimentos";

        //                diccionario["Moneda"] = moneda;

        //            }

        //            else
        //            {
        //                diccionario["Mensaje"] = "No se cargaron los alimentos";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        diccionario["Mensaje"] = "No se cargaron los alimentos" + ex;
        //    }

        //    return diccionario;
        //}
    }
}
