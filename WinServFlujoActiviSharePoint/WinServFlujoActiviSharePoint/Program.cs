using Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;
using Microsoft.SharePoint.Client;
using SP = Microsoft.SharePoint.Client;
using System.Net;

namespace WinServFlujoActiviSharePoint
{
    class Program
    {
        static void Main(string[] args)
        {
            FlujoActividadesSharePoint();
        }
        private static void FlujoActividadesSharePoint()
        {
            ArchivoLog.EscribirLog(null, "Se inicia el proceso");

            ParametroDAT parametroDAT = new ParametroDAT();
            DataTable dtParametro = new DataTable();
            List<Parametro> ListParametro = new List<Parametro>();
            Parametro parametro = new Parametro();

            //CARGAR INFORMACION BD
            DataTable dtActividades = new DataTable();
            BDDAT Datos = new BDDAT();

            string SiteUrl = string.Empty;
            string UsuarioSharePoint = string.Empty;
            string PasswordSharePoint = string.Empty;
            string ListaFlujoCronograma = string.Empty;
            string ClaveCampania = string.Empty;
            string NombreCampania = string.Empty;
            string MailResponsable = string.Empty;
            string MailResponsable2 = string.Empty;
            string FechaInicio = string.Empty;
            string FechaFin = string.Empty;
            string IDTarea = string.Empty;
            string TxtTarea = string.Empty;
            string TipoFlujo = string.Empty;
            string IdDependiente = string.Empty;
            string IdCampania = string.Empty;

            try
            {
                dtParametro = parametroDAT.GetParametro(0, null);

                ListParametro = dtParametro.AsEnumerable()
                                .Select(n => new Parametro
                                {
                                    Id = n.Field<int?>("Id").GetValueOrDefault(),
                                    Nombre = n.Field<string>("Nombre"),
                                    Valor = n.Field<string>("Valor")
                                }).ToList();

                ArchivoLog.EscribirLog(null, "Se obtienen variables de la base de datos");

                //SiteURL
                parametro = ListParametro.Where(n => n.Nombre.ToUpper() ==
                   ConfigurationManager.AppSettings["SiteURL"].ToString().ToUpper()).FirstOrDefault();
                if (parametro != null)
                {
                    SiteUrl = parametro.Valor;
                }

                //UsuarioSharePoint
                parametro = ListParametro.Where(n => n.Nombre.ToUpper() ==
                    ConfigurationManager.AppSettings["UsuarioSharePoint"].ToString().ToUpper()).FirstOrDefault();
                if (parametro != null)
                {
                    UsuarioSharePoint = parametro.Valor;
                }

                //PasswordSharePoint
                parametro = ListParametro.Where(n => n.Nombre.ToUpper() ==
                    ConfigurationManager.AppSettings["PasswordSharePoint"].ToString().ToUpper()).FirstOrDefault();
                if (parametro != null)
                {
                    PasswordSharePoint = parametro.Valor;
                }

                //IdCampania
                parametro = ListParametro.Where(n => n.Nombre.ToUpper() ==
                   ConfigurationManager.AppSettings["IdCampania"].ToString().ToUpper()).FirstOrDefault();
                if (parametro != null)
                {
                    IdCampania = parametro.Valor;
                }

                //ClaveCampania
                parametro = ListParametro.Where(n => n.Nombre.ToUpper() ==
                    ConfigurationManager.AppSettings["ClaveCampania"].ToString().ToUpper()).FirstOrDefault();
                if (parametro != null)
                {
                    ClaveCampania = parametro.Valor;
                }

                //NombreCampania
                parametro = ListParametro.Where(n => n.Nombre.ToUpper() ==
                    ConfigurationManager.AppSettings["NombreCampania"].ToString().ToUpper()).FirstOrDefault();
                if (parametro != null)
                {
                    NombreCampania = parametro.Valor;
                }

                //ListaFlujoCronograma
                parametro = ListParametro.Where(n => n.Nombre.ToUpper() ==
                    ConfigurationManager.AppSettings["ListaFlujoCronograma"].ToString().ToUpper()).FirstOrDefault();
                if (parametro != null)
                {
                    ListaFlujoCronograma = parametro.Valor;
                }

                //MailResponsable
                parametro = ListParametro.Where(n => n.Nombre.ToUpper() ==
                    ConfigurationManager.AppSettings["MailResponsable"].ToString().ToUpper()).FirstOrDefault();
                if (parametro != null)
                {
                    MailResponsable = parametro.Valor;
                }

                //MailResponsable2
                parametro = ListParametro.Where(n => n.Nombre.ToUpper() ==
                    ConfigurationManager.AppSettings["MailResponsable2"].ToString().ToUpper()).FirstOrDefault();
                if (parametro != null)
                {
                    MailResponsable2 = parametro.Valor;
                }

                //FechaInicio
                parametro = ListParametro.Where(n => n.Nombre.ToUpper() ==
                    ConfigurationManager.AppSettings["FechaInicio"].ToString().ToUpper()).FirstOrDefault();
                if (parametro != null)
                {
                    FechaInicio = parametro.Valor;
                }

                //FechaFin
                parametro = ListParametro.Where(n => n.Nombre.ToUpper() ==
                    ConfigurationManager.AppSettings["FechaFin"].ToString().ToUpper()).FirstOrDefault();
                if (parametro != null)
                {
                    FechaFin = parametro.Valor;
                }

                //IDTarea
                parametro = ListParametro.Where(n => n.Nombre.ToUpper() ==
                    ConfigurationManager.AppSettings["IDTarea"].ToString().ToUpper()).FirstOrDefault();
                if (parametro != null)
                {
                    IDTarea = parametro.Valor;
                }

                //TxtTarea
                parametro = ListParametro.Where(n => n.Nombre.ToUpper() ==
                    ConfigurationManager.AppSettings["TxtTarea"].ToString().ToUpper()).FirstOrDefault();
                if (parametro != null)
                {
                    TxtTarea = parametro.Valor;
                }

                //TipoFlujo
                parametro = ListParametro.Where(n => n.Nombre.ToUpper() ==
                    ConfigurationManager.AppSettings["TipoFlujo"].ToString().ToUpper()).FirstOrDefault();
                if (parametro != null)
                {
                    TipoFlujo = parametro.Valor;
                }

                //IdDependiente
                parametro = ListParametro.Where(n => n.Nombre.ToUpper() ==
                    ConfigurationManager.AppSettings["IdDependiente"].ToString().ToUpper()).FirstOrDefault();
                if (parametro != null)
                {
                    IdDependiente = parametro.Valor;
                }

                ArchivoLog.EscribirLog(null, "Se cargan variables del archivo config");

                //ACTIVIDADES TOTALES DE CRONOGRAMA
                dtActividades = Datos.GetFlujoActividad();

                List<FlujoActividad> ListFlujoActividad = new List<FlujoActividad>();

                ListFlujoActividad = dtActividades.AsEnumerable()
                                        .Select(n => new FlujoActividad
                                        {
                                            IdCampania = n.Field<int?>("Id_Campaña").GetValueOrDefault(),
                                            ClaveCampania = n.Field<string>("Camp_Number"),
                                            NombreCampania = n.Field<string>("Nombre_Camp"),
                                            MailResponsable = n.Field<string>("Correo"),
                                            MailResponsable2 = n.Field<string>("Correo_2"),
                                            IDTarea = n.Field<int?>("Id_Tarea").GetValueOrDefault(),
                                            TxtTarea = n.Field<string>("Actividad"),
                                            FechaInicio = n.Field<DateTime?>("FechaInicio").GetValueOrDefault(),
                                            FechaFin = n.Field<DateTime?>("FechaFin").GetValueOrDefault(),
                                            TipoFlujo = n.Field<int?>("IdTipoFlujo").GetValueOrDefault(),
                                            IdDependiente = n.Field<string>("Predecesor")
                                        }).ToList();

                ArchivoLog.EscribirLog(null, "Se obtuvo informacion de actividades");

                if (ListFlujoActividad.Count > 0)
                {
                    //CARGAR LISTA DE SHAREPOINT
                    ClientContext clientContext = new ClientContext(SiteUrl);

                    clientContext.Credentials = new NetworkCredential(UsuarioSharePoint, PasswordSharePoint);

                    ArchivoLog.EscribirLog(null, "Se cargo Correctamente el sitio Share Point");

                    SP.Web myWeb = clientContext.Web;

                    List myListFlujoCronograma = myWeb.Lists.GetByTitle(ListaFlujoCronograma);

                    ArchivoLog.EscribirLog(null, "Se cargo Correctamente la Lista");

                    foreach (FlujoActividad actividad in ListFlujoActividad)
                    {
                        try
                        {
                            ListItem item = myListFlujoCronograma.AddItem(new ListItemCreationInformation());

                            item[ClaveCampania] = actividad.ClaveCampania;
                            item[NombreCampania] = actividad.NombreCampania;
                            item[MailResponsable] = actividad.MailResponsable;
                            item[MailResponsable2] = actividad.MailResponsable2;
                            item[FechaInicio] = actividad.FechaInicio;
                            item[FechaFin] = actividad.FechaFin;
                            item[IDTarea] = actividad.IDTarea;
                            item[TxtTarea] = actividad.TxtTarea;
                            item[TipoFlujo] = actividad.TipoFlujo;
                            item[IdDependiente] = actividad.IdDependiente;
                            item[IdCampania] = actividad.IdCampania;

                            item.Update();

                            Datos.UpdateFlujoActividad(actividad.IdCampania, actividad.IDTarea);
                        }
                        catch (Exception ex)
                        {
                            ArchivoLog.EscribirLog(null, "ERROR: No se agrego el registro de la actividad, Source: " + ex.Source +
                                                        ", Message: " + ex.Message);
                        }
                    }

                    clientContext.ExecuteQuery();

                    ArchivoLog.EscribirLog(null, "Se actualiza la informacion en la lista");
                }
            }
            catch(Exception ex)
            {
                ArchivoLog.EscribirLog(null, "ERROR: Source: " + ex.Source + ", Message: " + ex.Message);
            }
        }
    }
}
