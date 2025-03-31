namespace WebAPITickets.Models
{
    public class Tiquetes
    {
        public int ti_identificador { get; set; }
        public string ti_asunto { get; set; }
        public string ti_categoria { get; set; }
        public int ti_us_id_asigna { get; set; }
        public string ti_urgencia { get; set; }
        public string ti_importancia { get; set; }
        public string ti_estado { get; set; }
        public string? ti_solucion { get; set; }
        public DateTime ti_fecha_adicion { get; set; }
        public string ti_adicionado_por { get; set; }
        public DateTime? ti_fecha_modificacion { get; set; }
        public string? ti_modificado_por { get; set; }

    }
}
