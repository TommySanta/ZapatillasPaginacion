using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZapatillasPaginacion.Models
{
    [Table ("IMAGENESZAPASPRACTICA")]
    public class ImagenesZapasPractica
    {
        [Key]
        [Column("IDIMAGEN")]
        public int IdImagen { get; set; }
        [Column("IDPRODUCTO")]
        public int IdProducto { get; set; }
        [Column("IMAGEN")]
        public string Imagen { get; set; }

        [ForeignKey(nameof(IdProducto))]
        public Zapatilla Zapatilla { get; set; }

    }
}
