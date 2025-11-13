namespace DESCARGAR_CATALOGO.Models
{
    public class ProductoDto
    {
        public string ItemCode { get; set; } = "";
        public string ItemName { get; set; } = "";
        public string Grupo { get; set; } = "";
        public decimal PrecioLista1 { get; set; }
        public decimal PrecioCliente { get; set; }
        public decimal Caja { get; set; }
        public decimal M { get; set; }
        public string UFamilia { get; set; } = "";
        public string USubFamilia { get; set; } = "";
        public string Marca { get; set; } = "";
        public decimal Stock { get; set; }
    }
}
