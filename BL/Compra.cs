using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Compra
    {
        public int IdCompra { get; set; }

        public Usuario Usuario { get; set; }

        public Alimento Alimento { get; set; }

        public decimal? Total { get; set; }

        public Moneda Moneda { get; set; }

        public List<object> Compras { get; set; }


        public static Dictionary<string, object> RealizarCompra(int IdUsuario, int idAlimento, int Total, int denominacion10, int denominacion50, int denominacion100 )
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Respuesta", false }, { "Mensaje", "" } };

            try
            {
                using (DL.MaquinaAlimentosContext context = new DL.MaquinaAlimentosContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"RealizarCompra {IdUsuario}, {idAlimento},{Total},{denominacion10},{denominacion50},{denominacion100}");

                    if (query >0)
                    {
                        
                            diccionario["Respuesta"] = true;
                       
                        
                            diccionario["Mensaje"] = "Se ha registrado la compra";
                        
                    }

                    else
                    {
                        diccionario["Mensaje"] = "No se ha registrado la compra";
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Mensaje"] = "No se ha registrado la compra" + ex;
            }

            return diccionario;
        }

        public static Dictionary<string, object> GetComprasById(int idUsuario)
        {
            BL.Compra compra = new BL.Compra();

            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Compra", compra }, { "Respuesta", false }, { "Mensaje", "" } };

            try
            {
                using (DL.MaquinaAlimentosContext context = new DL.MaquinaAlimentosContext())
                {
                    var query = (from Compra in context.Compras
                                 join alimento in context.Alimentos on Compra.IdAlimento equals alimento.IdAlimento
                                 where Compra.IdUsuario == idUsuario
                                 select new
                                 {
                                     IdCompra = Compra.IdCompra,
                                     IdUsuario = Compra.IdUsuario,
                                     Total = Compra.Total,
                                     IdAlimento = alimento.IdAlimento,
                                     Nombre = alimento.Nombre,  
                                     Imagen = alimento.Imagen,
                                     Descripcion = alimento.Descripcion,
                                     

                                 }).ToList();

                    if (query != null)
                    {
                        compra.Compras = new List<object>();
                        foreach(var item in query)
                        {
                            BL.Compra compraObj = new BL.Compra();
                            compraObj.IdCompra = item.IdCompra;
                            compraObj.Usuario = new Usuario();
                            compraObj.Usuario.IdUsuario = item.IdUsuario.Value;
                            compraObj.Total = item.Total.Value;
                            compraObj.Alimento = new Alimento();
                            compraObj.Alimento.IdAlimento = item.IdAlimento;
                            compraObj.Alimento.Nombre = item.Nombre;
                            compraObj.Alimento.Imagen = item.Imagen;
                            compraObj.Alimento.Descripcion = item.Descripcion;  
                            compra.Compras.Add(compraObj);  
                        }
                      

                        diccionario["Usuario"] = compra;
                        diccionario["Respuesta"] = true;
                        diccionario["Mensaje"] = "Se han recuperado los datos";

                    }

                    else
                    {
                        diccionario["Mensaje"] = "Usuario incorrecto";
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Mensaje"] = "Usuario incorrecto" + ex;
            }

            return diccionario;
        }






    }
}
