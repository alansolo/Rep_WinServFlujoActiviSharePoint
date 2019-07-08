using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinServFlujoActiviSharePoint
{
    public class FlujoActividad
    {
        public int IdCampania { get; set; }
        public string ClaveCampania { get; set; }
        public string NombreCampania { get; set; }
        public string MailResponsable { get; set; }
        public string MailResponsable2 { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int IDTarea { get; set; }
        public string TxtTarea { get; set; }
        public int TipoFlujo { get; set; }
        public string IdDependiente { get; set; }
    }
}
