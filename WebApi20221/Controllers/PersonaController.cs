using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApi20221.Models;

namespace WebApi20221.Controllers
{
    [RoutePrefix("api/v1/persona")]
    public class PersonaController : ApiController
    {
        #region BuscarTodos
        [HttpPost,Route("BuscarTodos")]
        public HttpResponseMessage ListarPersona()
        {
            object datos = null;
            HttpStatusCode respuesta = new HttpStatusCode();
            List<Persona> listado = new List<Persona>();

            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from personasdb";

                    connection.Open();

                    SqlDataReader resultado = cmd.ExecuteReader();

                    while (resultado.Read())
                    {
                        Persona persona = new Persona();

                        persona.Id = Convert.ToInt32(resultado[0]);
                        persona.Nombres = resultado[1].ToString();
                        persona.Apellidos = resultado[2].ToString();

                        listado.Add(persona);
                    }

                    if (listado.Any())
                    {
                        respuesta = HttpStatusCode.OK;
                        datos = new { Vacio = false, Mensaje = listado };
                    }
                    else 
                    {
                        respuesta = HttpStatusCode.OK;
                        datos = new { Vacio = true, Mensaje = "No hay datos en la DB" };
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = ex.Message;

                datos = new { Vacio = true, Mensaje = msg };
            }

            return Request.CreateResponse(respuesta, datos, "application/json");
        }
        #endregion

        #region BuscarUno
        [HttpPost, Route("BuscarUno")]
        public HttpResponseMessage BuscarUno([FromBody] string personaparam)
        {
            object datos = null;
            HttpStatusCode respuesta = new HttpStatusCode();
            Persona persona = null;

            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from personasdb where id = @id";

                    cmd.Parameters.AddWithValue("@id", personaparam);

                    connection.Open();

                    SqlDataReader resultado = cmd.ExecuteReader();

                    if (resultado.Read())
                    {
                        persona = new Persona();

                        persona.Id = Convert.ToInt32(resultado[0]);
                        persona.Nombres = resultado[1].ToString();
                        persona.Apellidos = resultado[2].ToString();
                    }

                    if (persona != null)
                    {
                        respuesta = HttpStatusCode.OK;
                        datos = new { Vacio = false, Mensaje = persona };
                    }
                    else
                    {
                        respuesta = HttpStatusCode.OK;
                        datos = new { Vacio = true, Mensaje = persona };
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

            return Request.CreateResponse(respuesta, datos, "application/json");
        }
        #endregion

        #region Insertar
        [HttpPost, Route("Inserta")]
        public HttpResponseMessage Insertar([FromBody] Persona personaparam)
        {
            object datos = null;
            HttpStatusCode respuesta = new HttpStatusCode();
            Persona persona = new Persona();

            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into personasdb (nombres, apellidos) values (@nombres, @apellidos)";

                    cmd.Parameters.AddWithValue("@nombres", personaparam.Nombres);
                    cmd.Parameters.AddWithValue("@apellidos", personaparam.Apellidos);

                    connection.Open();

                    int resultado = cmd.ExecuteNonQuery();

                   

                    if (resultado > 0)
                    {
                        respuesta = HttpStatusCode.OK;
                        datos = new { Vacio = false, Mensaje = resultado };
                    }
                    else
                    {
                        respuesta = HttpStatusCode.OK;
                        datos = new { Vacio = true, Mensaje = resultado };
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

            return Request.CreateResponse(respuesta, datos, "application/json");
        }
        #endregion

        #region Actualizar
        [HttpPost, Route("Actualizar")]
        public HttpResponseMessage Actualizar([FromBody] Persona personaparam)
        {
            object datos = null;
            HttpStatusCode respuesta = new HttpStatusCode();
            Persona persona = new Persona();

            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update personasdb set nombres = @nombres, apellidos = @apellidos where id = @id";

                    cmd.Parameters.AddWithValue("@nombres", personaparam.Nombres);
                    cmd.Parameters.AddWithValue("@apellidos", personaparam.Apellidos);
                    cmd.Parameters.AddWithValue("@id", personaparam.Id);

                    connection.Open();

                    int resultado = cmd.ExecuteNonQuery();



                    if (resultado > 0)
                    {
                        respuesta = HttpStatusCode.OK;
                        datos = new { Vacio = false, Mensaje = resultado };
                    }
                    else
                    {
                        respuesta = HttpStatusCode.OK;
                        datos = new { Vacio = true, Mensaje = resultado };
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

            return Request.CreateResponse(respuesta, datos, "application/json");
        }
        #endregion

        #region Actualizar
        [HttpPost, Route("Eliminar")]
        public HttpResponseMessage Eliminar([FromBody] string personaparam)
        {
            object datos = null;
            HttpStatusCode respuesta = new HttpStatusCode();
            Persona persona = new Persona();

            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "delete from personasdb where id = @id";

                
                    cmd.Parameters.AddWithValue("@id", personaparam);

                    connection.Open();

                    int resultado = cmd.ExecuteNonQuery();



                    if (resultado > 0)
                    {
                        respuesta = HttpStatusCode.OK;
                        datos = new { Vacio = false, Mensaje = resultado };
                    }
                    else
                    {
                        respuesta = HttpStatusCode.OK;
                        datos = new { Vacio = true, Mensaje = resultado };
                    }
                }

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

            return Request.CreateResponse(respuesta, datos, "application/json");
        }
        #endregion
    }
}