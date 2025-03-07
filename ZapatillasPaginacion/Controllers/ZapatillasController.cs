using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using ZapatillasPaginacion.Models;
using ZapatillasPaginacion.Repositories;

namespace ZapatillasPaginacion.Controllers
{
    public class ZapatillasController : Controller
    {
        public RepositoryZapatillas repo;
        public ZapatillasController(RepositoryZapatillas repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Zapatillas()
        {
            List<Zapatilla> zapatillas =
                await this.repo.GetZapatillasAsync();

            return View(zapatillas);
        }

        public async Task<IActionResult> Details(int id)
        {
            var zapatilla = await this.repo.GetZapatillaByIdAsync(id);
            if (zapatilla == null)
            {
                return NotFound();
            }

            var imagenes = await this.repo.GetImagenesByZapatillaAsync(id, 1, int.MaxValue);
            zapatilla.Imagenes = imagenes;
            ViewData["TotalImagenes"] = imagenes.Count;
            return View(zapatilla);
        }

        public async Task<IActionResult> GetImagenes(int idProducto, int pageNumber = 1)
        {
            int pageSize = 1; // Mostramos una imagen a la vez
            var imagenes = await this.repo.GetImagenesByZapatillaAsync(idProducto, pageNumber, pageSize);

            return PartialView("_ImagenesPaginadas", imagenes);
        }
    }
}
