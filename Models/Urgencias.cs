using System;

namespace WebAPITickets.Models
{
    public class Urgencias
    {
        public int ur_identificador { get; set; }
        public string ur_descripcion { get; set; }
        public DateTime ur_fecha_adicion { get; set; }
        public string ur_adicionado_por { get; set; }
        public DateTime? ur_fecha_modificacion { get; set; }
        public string? ur_modificado_por { get; set; }
    }
}
