using System;

namespace WebAPITickets.Models
{
    public class Categorias
    {
        public int ca_identificador { get; set; }
        public string ca_descripcion { get; set; }
        public DateTime ca_fecha_adicion { get; set; }
        public string ca_adicionado_por { get; set; }
        public DateTime? ca_fecha_modificacion { get; set; }
        public string? ca_modificado_por { get; set; }
    }
}
