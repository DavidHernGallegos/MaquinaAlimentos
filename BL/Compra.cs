using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

        public int? Cambio { get; set; }

        public int? DineroIngresado { get; set; }

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

        public static Dictionary<string, object> GetComprasById(int idUsuario, int? idAlimento)
        {
            BL.Compra compra = new BL.Compra();

            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Compra", compra }, { "Respuesta", false }, { "Mensaje", "" } };

            try
            {
                using (DL.MaquinaAlimentosContext context = new DL.MaquinaAlimentosContext())
                {
                    var query = (from Compra in context.Compras
                                 join alimento in context.Alimentos on Compra.IdAlimento equals alimento.IdAlimento
                                 join moneda in context.Moneda on Compra.IdMoneda equals moneda.IdMoneda
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
                                     IdMoneda = moneda.IdMoneda,
                                     Denominacion100 = moneda.Denominacion100,
                                     Denominacion50 = moneda.Denominacion50,
                                     Denominacion10 = moneda.Denominacion10
                                     

                                 }).ToList();

                    if (idAlimento != null)
                    {
                        query = query.Where(s => s.IdAlimento.Equals(idAlimento)).ToList();
                    }


                    if (query != null)
                    {
                        compra.Compras = new List<object>();
                       

                        foreach(var item in query)
                        {
                            var Contador100 = 0;
                            var Contador50 = 0;
                            var Contador10 = 0;
                            var DineroIngresado = 0;
                            var Cambio = 0;

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
                            compraObj.Moneda = new Moneda();
                            compraObj.Moneda.IdMoneda = item.IdMoneda;
                            compraObj.Moneda.Denominacion100 = item.Denominacion100;
                            compraObj.Moneda.Denominacion50 = item.Denominacion50;
                            compraObj.Moneda.Denominacion10 = item.Denominacion10;
                            if(compraObj.Moneda.Denominacion100 > 0)
                            {
                                Contador100 = compraObj.Moneda.Denominacion100.Value * 100;
                                DineroIngresado = Contador100;
                            }
                            if (compraObj.Moneda.Denominacion50 > 0)
                            {
                                Contador50 = compraObj.Moneda.Denominacion50.Value * 50;
                                DineroIngresado = DineroIngresado + Contador50;
                            }
                            if (compraObj.Moneda.Denominacion10 > 0)
                            {
                                Contador10 = compraObj.Moneda.Denominacion10.Value * 10;
                                DineroIngresado = DineroIngresado + Contador10;
                            }

                            compraObj.DineroIngresado = DineroIngresado;

                            if(DineroIngresado > compraObj.Total)
                            {
                                Cambio = (int)(DineroIngresado- compraObj.Total);
                                compraObj.Cambio = Cambio;
                            }

                            compra.Compras.Add(compraObj);  

                            
                        }
                      

                        diccionario["Compra"] = compra;
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



        public static Dictionary<string, object> GetComprasByIdCompra(int IdCompra)
        {
            BL.Compra compra = new BL.Compra();

            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Compra", compra }, { "Respuesta", false }, { "Mensaje", "" } };

            try
            {
                using (DL.MaquinaAlimentosContext context = new DL.MaquinaAlimentosContext())
                {
                    var query = (from Compra in context.Compras
                                 join alimento in context.Alimentos on Compra.IdAlimento equals alimento.IdAlimento
                                 join moneda in context.Moneda on Compra.IdMoneda equals moneda.IdMoneda
                                 where Compra.IdCompra == IdCompra
                                 select new
                                 {
                                     IdCompra = Compra.IdCompra,
                                     IdUsuario = Compra.IdUsuario,
                                     Total = Compra.Total,
                                     IdAlimento = alimento.IdAlimento,
                                     Nombre = alimento.Nombre,
                                     Imagen = alimento.Imagen,
                                     Descripcion = alimento.Descripcion,
                                     IdMoneda = moneda.IdMoneda,
                                     Denominacion100 = moneda.Denominacion100,
                                     Denominacion50 = moneda.Denominacion50,
                                     Denominacion10 = moneda.Denominacion10


                                 }).SingleOrDefault();

                   

                    if (query != null)
                    {
                        


                       
                            var Contador100 = 0;
                            var Contador50 = 0;
                            var Contador10 = 0;
                            var DineroIngresado = 0;
                            var Cambio = 0;

                          
                            compra.IdCompra = query.IdCompra;
                            compra.Usuario = new Usuario();
                            compra.Usuario.IdUsuario = query.IdUsuario.Value;
                            compra.Total = query.Total.Value;
                            compra.Alimento = new Alimento();
                            compra.Alimento.IdAlimento = query.IdAlimento;
                            compra.Alimento.Nombre = query.Nombre;
                            compra.Alimento.Imagen = query.Imagen;
                            compra.Alimento.Descripcion = query.Descripcion;
                            compra.Moneda = new Moneda();
                            compra.Moneda.IdMoneda = query.IdMoneda;
                            compra.Moneda.Denominacion100 = query.Denominacion100;
                            compra.Moneda.Denominacion50 = query.Denominacion50;
                            compra.Moneda.Denominacion10 = query.Denominacion10;
                            if (compra.Moneda.Denominacion100 > 0)
                            {
                                Contador100 = compra.Moneda.Denominacion100.Value * 100;
                                DineroIngresado = Contador100;
                            }
                            if (compra.Moneda.Denominacion50 > 0)
                            {
                                Contador50 = compra.Moneda.Denominacion50.Value * 50;
                                DineroIngresado = DineroIngresado + Contador50;
                            }
                            if (compra.Moneda.Denominacion10 > 0)
                            {
                                Contador10 = compra.Moneda.Denominacion10.Value * 10;
                                DineroIngresado = DineroIngresado + Contador10;
                            }

                            compra.DineroIngresado = DineroIngresado;

                            if (DineroIngresado > compra.Total)
                            {
                                Cambio = (int)(DineroIngresado - compra.Total);
                                compra.Cambio = Cambio;
                            }

                          


                    }


                        diccionario["Compra"] = compra;
                        diccionario["Respuesta"] = true;
                        diccionario["Mensaje"] = "Se han recuperado los datos";

                   

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
