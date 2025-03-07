using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ZapatillasPaginacion.Data;
using ZapatillasPaginacion.Models;

#region
/*
 CREATE PROCEDURE GetZapatillaById
    @IdProducto INT
AS
BEGIN
    SELECT IDPRODUCTO, NOMBRE, DESCRIPCION, PRECIO
    FROM ZAPASPRACTICA
    WHERE IDPRODUCTO = @IdProducto
END

EXEC GetZapatillaById 1;


alter PROCEDURE GetImagenesByZapatilla
    @IdProducto INT,
    @PageNumber INT,
    @PageSize INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IDIMAGEN, IDPRODUCTO, IMAGEN
    FROM IMAGENESZAPASPRACTICA
    WHERE IDPRODUCTO = @IdProducto
    ORDER BY IDIMAGEN
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END
 */
#endregion
namespace ZapatillasPaginacion.Repositories
{
    public class RepositoryZapatillas
    {
        public ZapatillasContext context;
        public RepositoryZapatillas(ZapatillasContext context)
        {
            this.context = context;
        }
        public async Task<List<Zapatilla>> GetZapatillasAsync()
        {
            return await this.context.Zapatillas
                .Include(z => z.Imagenes)
                .ToListAsync();
        }
        public async Task<Zapatilla> GetZapatillaByIdAsync(int idProducto)
        {
            string sql = "GetZapatillaById @IdProducto";
            SqlParameter pamProducto =
                new SqlParameter("@IdProducto", idProducto);
            var consulta =
                this.context.Zapatillas.FromSqlRaw(sql, pamProducto).AsEnumerable(); ;
            return consulta.FirstOrDefault();
        }

        public async Task<List<ImagenesZapasPractica>> GetImagenesByZapatillaAsync(int idProducto, int pageNumber, int pageSize)
        {
            string sql = "EXEC GetImagenesByZapatilla @IdProducto, @PageNumber, @PageSize";

            SqlParameter pamIdProducto = new SqlParameter("@IdProducto", idProducto);
            SqlParameter pamPageNumber = new SqlParameter("@PageNumber", pageNumber);
            SqlParameter pamPageSize = new SqlParameter("@PageSize", pageSize);

            var consulta = this.context.ImagenesZapas
                .FromSqlRaw(sql, pamIdProducto, pamPageNumber, pamPageSize)
                .AsEnumerable();

            return consulta.ToList();
        }

    }
}
