using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MpDeportesMVC.Entidades;
using MpDeportesMVC.Servicios.Interfaces;
using MpDeportesMVC.WEB.ViewModels.Genres;
using X.PagedList;

namespace MpDeportesMVC.WEB.Controllers
{
    public class GenreController : Controller
    {
        private readonly IServicioGenre? servicio;
        private readonly IMapper? _mapper;

        public GenreController(IServicioGenre? servicio, IMapper? mapper)
        {
            this.servicio = servicio;
            _mapper = mapper;
        }

        public IActionResult Index(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;
            var genres = servicio?.GetAll(orderBy: o => o.OrderBy(c => c.GenreName));
            var genresVm=_mapper?.Map<List<GenreListVm>>(genres)
                .ToPagedList(pageNumber, pageSize);
            return View(genresVm);
        }

        public IActionResult UpSert(int? id)
        {
            if (servicio == null || _mapper == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Dependencias no están configuradas correctamente");
            }
            GenreEditVm genreVm;
            if (id == null || id == 0)
            {
                genreVm = new GenreEditVm();
            }
            else
            {
                try
                {
                    Genre? genre = servicio.Get(filter: g => g.GenreId == id);
                    if (genre == null)
                    {
                        return NotFound();
                    }
                    genreVm = _mapper.Map<GenreEditVm>(genre);
                    return View(genreVm);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the record.");
                }

            }
            return View(genreVm);

        }


        [HttpPost]
        public IActionResult UpSert(GenreEditVm genreVm)
        {
            if (!ModelState.IsValid)
            {
                return View(genreVm);
            }

            if (servicio == null || _mapper == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Dependencias no están configuradas correctamente");
            }

            try
            {
                Genre genre = _mapper.Map<Genre>(genreVm);

                if (servicio.Existe(genre))
                {
                    ModelState.AddModelError(string.Empty, "Record already exist");
                    return View(genreVm);
                }

                servicio.Save(genre);
                TempData["success"] = "Record successfully added/edited";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while editing the record.");
                return View(genreVm);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            Genre? genre = servicio?.Get(filter: g => g.GenreId == id);
            if (genre is null)
            {
                return NotFound();
            }
            try
            {
                if (servicio == null || _mapper == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Dependencias no están configuradas correctamente");
                }
                if (servicio.EstaRelacionado(genre.GenreId))
                {
                    return Json(new { success = false, message = "Related Record... Delete Deny!!" }); ;
                }
                servicio.Delete(genre);
                return Json(new { success = true, message = "Record successfully deleted" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Couldn't delete record!!! " }); ;
            }
        }
    }
}
