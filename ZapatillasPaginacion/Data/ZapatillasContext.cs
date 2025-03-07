using Microsoft.EntityFrameworkCore;
using ZapatillasPaginacion.Models;

namespace ZapatillasPaginacion.Data
{
    public class ZapatillasContext : DbContext
    {
        public ZapatillasContext(DbContextOptions<ZapatillasContext> options) : base(options) { }
        public DbSet<Zapatilla> Zapatillas { get; set; }
        public DbSet<ImagenesZapasPractica> ImagenesZapas { get; set; }

    }
}