using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Alimento
    {
        public int IdAlimento { get; set; }

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        public string? Imagen { get; set; }

        public decimal? Precio { get; set; }

        public List<object> Alimentos { get; set; }


        public static Dictionary<string, object> GetAll()
        {
            Alimento alimento = new Alimento();
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Respuesta", false }, { "Mensaje", "" }, {"Alimento", alimento  } };

            try
            {
                using (DL.MaquinaAlimentosContext context = new DL.MaquinaAlimentosContext())
                {
                    var query = (from Alimentos in context.Alimentos
                                 select new
                                 {
                                    IdAlimento = Alimentos.IdAlimento,
                                    Nombre = Alimentos.Nombre,
                                    Descripcion = Alimentos.Descripcion,
                                    Imagen = Alimentos.Imagen,
                                    Precio = Alimentos.Precio

                                 }).ToList();

                    if (query != null)
                    {

                        alimento.Alimentos = new List<object>();
                        foreach (var item in query)
                        {
                            Alimento alimentoObj = new Alimento();
                            alimentoObj.IdAlimento = item.IdAlimento;
                            alimentoObj.Nombre = item.Nombre;
                            alimentoObj.Descripcion = item.Descripcion;
                            alimentoObj.Imagen = item.Imagen;
                            alimentoObj.Precio = item.Precio;
                            alimento.Alimentos.Add(alimentoObj);
                        }
                        
                            diccionario["Respuesta"] = true;
                            diccionario["Mensaje"] = "Se cargaron los alimentos";

                        diccionario["Alimento"] = alimento;
                        
                    }

                    else
                    {
                        diccionario["Mensaje"] = "No se cargaron los alimentos";
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Mensaje"] = "No se cargaron los alimentos" + ex;
            }

            return diccionario;
        }


        public static Dictionary<string, object> GetById(int idAlimento)
        {
            Alimento alimento = new Alimento();
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Respuesta", false }, { "Mensaje", "" }, { "Alimento", alimento } };

            try
            {
                using (DL.MaquinaAlimentosContext context = new DL.MaquinaAlimentosContext())
                {
                    var query = (from Alimentos in context.Alimentos
                                 where Alimentos.IdAlimento == idAlimento
                                 select new
                                 {
                                     IdAlimento = Alimentos.IdAlimento,
                                     Nombre = Alimentos.Nombre,
                                     Descripcion = Alimentos.Descripcion,
                                     Imagen = Alimentos.Imagen,
                                     Precio = Alimentos.Precio

                                 }).SingleOrDefault();

                    if (query != null)
                    {

                      
                       
                            
                            alimento.IdAlimento = query.IdAlimento;
                            alimento.Nombre = query.Nombre;
                            alimento.Descripcion = query.Descripcion;
                            alimento.Imagen = query.Imagen;
                            alimento.Precio = query.Precio;
                            
                        

                        diccionario["Respuesta"] = true;
                        diccionario["Mensaje"] = "Se cargaron los alimentos";

                        diccionario["Alimento"] = alimento;

                    }

                    else
                    {
                        diccionario["Mensaje"] = "No se cargaron los alimentos";
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Mensaje"] = "No se cargaron los alimentos" + ex;
            }

            return diccionario;
        }


        public static Dictionary<int, int> CalcularBilletesYMonedas(decimal cantidad)
        {


            Dictionary<int, int> resultado = new Dictionary<int, int>();

            Dictionary<int, int> denominacionesBilletes;
            Dictionary<int, int> denominacionesMonedas;

            denominacionesBilletes = new Dictionary<int, int>
            {

                { 100, 0 },
                { 50, 0 },
               
            };

            denominacionesMonedas = new Dictionary<int, int>
            {
                { 10, 0 },
              
            };



            int cantidadCentavos = (int)cantidad;


            foreach (var denominacion in denominacionesBilletes.Keys)
            {
                int cantidadBilletes = cantidadCentavos / denominacion;
                if (cantidadBilletes > 0 && denominacionesBilletes.ContainsKey(denominacion))
                {
                    resultado[denominacion] = cantidadBilletes;
                    cantidadCentavos -= cantidadBilletes * denominacion;
                }
            }
            foreach (var denominacion in denominacionesMonedas.Keys)
            {
                int cantidadMonedas = cantidadCentavos / denominacion;
                if (cantidadMonedas > 0 && denominacionesMonedas.ContainsKey(denominacion))
                {
                    resultado[denominacion] = cantidadMonedas;
                    cantidadCentavos -= cantidadMonedas * denominacion;
                }
            }

            return resultado;
        }



    }
}
