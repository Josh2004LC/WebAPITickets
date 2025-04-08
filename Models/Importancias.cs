using System;

namespace WebAPITickets.Models
{
    public class Importancias
    {
        public int im_identificador { get; set; }
        public string im_descripcion { get; set; }
        public DateTime im_fecha_adicion { get; set; }
        public string im_adicionado_por { get; set; }
        public DateTime? im_fecha_modificacion { get; set; }
        public string? im_modificado_por { get; set; }
    }
}
