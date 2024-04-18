using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {

        public int IdUsuario { get; set; }

        public string? Nombre { get; set; }

        public string? ApellidoPaterno { get; set; }

        public string? ApellidoMaterno { get; set; }

        public string? CorreoElectronico { get; set; }

        public string? Contraseña { get; set; }


        public static Dictionary<string,object> Login(string email, string contraseña)
        {
            Dictionary<string,object> diccionario = new Dictionary<string, object> { {"Respuesta", false },{"Mensaje", "" } };

            try
            {
                using(DL.MaquinaAlimentosContext context = new DL.MaquinaAlimentosContext())
                {
                    var query = (from Usuarios in context.Usuarios
                                        where Usuarios.CorreoElectronico == email
                                        select new
                                        {
                                            CorreoElectronico = Usuarios.CorreoElectronico,
                                            Contraseña = Usuarios.Contraseña,
                                        

                                        }).SingleOrDefault();

                    if(query != null)
                    {
                        if(query.Contraseña == contraseña)
                        {
                            diccionario["Respuesta"] = true;
                            diccionario["Mensaje"] = "Inicio de session exitoso";
                        }
                        else
                        {
                            diccionario["Mensaje"] = "Contraseña incorrecta";
                        }
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

        public static Dictionary<string, object> GetById(int idUsuario)
        {
            BL.Usuario usuario = new BL.Usuario();

            Dictionary<string, object> diccionario = new Dictionary<string, object> { {"Usuario", usuario },{ "Respuesta", false }, { "Mensaje", "" } };

            try
            {
                using (DL.MaquinaAlimentosContext context = new DL.MaquinaAlimentosContext())
                {
                    var query = (from Usuarios in context.Usuarios
                                 select new
                                 {
                                     IdUsuario = Usuarios.IdUsuario,
                                     Nombre = Usuarios.Nombre,
                                     CorreoElectronico = Usuarios.CorreoElectronico,
                                     Contraseña = Usuarios.Contraseña

                                 }).SingleOrDefault();

                    if (query != null)
                    {
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.Nombre;
                        usuario.CorreoElectronico = query.CorreoElectronico;
                        usuario.Contraseña = query.Contraseña;

                        diccionario["Usuario"] = usuario;
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



        public static Dictionary<string, object> GetByEmail(string email)
        {
            BL.Usuario usuario = new BL.Usuario();

            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Usuario", usuario }, { "Respuesta", false }, { "Mensaje", "" } };

            try
            {
                using (DL.MaquinaAlimentosContext context = new DL.MaquinaAlimentosContext())
                {
                    var query = (from Usuarios in context.Usuarios
                                 where Usuarios.CorreoElectronico == email
                                 select new
                                 {
                                     IdUsuario = Usuarios.IdUsuario,
                                     Nombre = Usuarios.Nombre,
                                     CorreoElectronico = Usuarios.CorreoElectronico,
                                     Contraseña = Usuarios.Contraseña

                                 }).SingleOrDefault();

                    if (query != null)
                    {
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.Nombre;
                        usuario.CorreoElectronico = query.CorreoElectronico;
                        usuario.Contraseña = query.Contraseña;

                        diccionario["Usuario"] = usuario;
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
