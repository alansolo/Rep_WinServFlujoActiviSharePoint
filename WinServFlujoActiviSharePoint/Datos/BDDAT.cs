using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class BDDAT:BD
    {
        public DataTable GetFlujoActividad()
        {
            const string spName = "MostrarCronogramaSharePoint";
            //const string spName = "MostrarCronogramaSharePointAll";
            List<SqlParameter> ListParametros = new List<SqlParameter>();

            try
            {
                return base.ExecuteDataTable(spName, ListParametros);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public int UpdateFlujoActividad(int IdCampania, int IdTarea)
        {
            int resultado = 0;
            const string spName = "UpdateCronogramaSharePoint";
            List<SqlParameter> ListParametros = new List<SqlParameter>();
            SqlParameter Parametro;


            try
            {
                Parametro = new SqlParameter();
                Parametro.ParameterName = "@IDCampania";
                Parametro.Value = IdCampania;
                ListParametros.Add(Parametro);

                Parametro = new SqlParameter();
                Parametro.ParameterName = "@IDTarea";
                Parametro.Value = IdTarea;
                ListParametros.Add(Parametro);

                return base.ExecuteNonQuery(spName, ListParametros);
            }
            catch(Exception ex)
            {

            }

            return resultado;
        }
        public DataTable GetActividadNoProgramar(int IdAlcance, int IdTarea)
        {
            const string spName = "MostrarCronogramaNoProgramar";
            List<SqlParameter> ListParametros = new List<SqlParameter>();
            SqlParameter Parametro = new SqlParameter();

            try
            {
                Parametro = new SqlParameter();
                Parametro.ParameterName = "@IdAlcance";
                if(IdAlcance > 0)
                {
                    Parametro.Value = IdAlcance;
                }
                ListParametros.Add(Parametro);

                Parametro = new SqlParameter();
                Parametro.ParameterName = "@IdTarea";
                if(IdTarea > 0)
                {
                    Parametro.Value = IdTarea;
                }
                ListParametros.Add(Parametro);

                return base.ExecuteDataTable(spName, ListParametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
