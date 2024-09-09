using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MpDeportesMVC.Entidades;
using MpDeportesMVC.Servicios.Interfaces;
using MpDeportesMVC.WEB.ViewModels.Sports;
using X.PagedList;

namespace MpDeportesMVC.WEB.Controllers
{
    public class SportController : Controller
    {
        private readonly IServicioSport? servicio;
        private readonly IMapper? _mapper;

        public SportController(IServicioSport? servicio, IMapper? mapper)
        {
            this.servicio = servicio;
            _mapper = mapper;
        }

        public IActionResult Index(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;
            var Sports = servicio?.GetAll
                (orderBy: o => o.OrderBy(c => c.SportName));
            var SportsVm = _mapper?.Map<List<SportListVm>>(Sports)
                .ToPagedList(pageNumber, pageSize);
            return View(SportsVm);
        }

        public IActionResult UpSert(int? id)
        {
            if (servicio == null || _mapper == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Dependencias no están configuradas correctamente");
            }
            SportEditVm SportVm;
            if (id == null || id == 0)
            {
                SportVm = new SportEditVm();
            }
            else
            {
                try
                {
                    Sport? Sport = servicio.Get(filter: g => g.SportId == id);
                    if (Sport == null)
                    {
                        return NotFound();
                    }
                    SportVm = _mapper.Map<SportEditVm>(Sport);
                    return View(SportVm);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the record.");
                }

            }
            return View(SportVm);

        }


        [HttpPost]
        public IActionResult UpSert(SportEditVm SportVm)
        {
            if (!ModelState.IsValid)
            {
                return View(SportVm);
            }

            if (servicio == null || _mapper == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Dependencias no están configuradas correctamente");
            }

            try
            {
                Sport Sport = _mapper.Map<Sport>(SportVm);

                if (servicio.Existe(Sport))
                {
                    ModelState.AddModelError(string.Empty, "Record already exist");
                    return View(SportVm);
                }

                servicio.Save(Sport);
                TempData["success"] = "Record successfully added/edited";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while editing the record.");
                return View(SportVm);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            Sport? Sport = servicio?.Get(filter: g => g.SportId == id);
            if (Sport is null)
            {
                return NotFound();
            }
            try
            {
                if (servicio == null || _mapper == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Dependencias no están configuradas correctamente");
                }
                if (servicio.EstaRelacionado(Sport.SportId))
                {
                    return Json(new { success = false, message = "Related Record... Delete Deny!!" }); ;
                }
                servicio.Delete(Sport);
                return Json(new { success = true, message = "Record successfully deleted" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Couldn't delete record!!! " }); ;
            }
        }
    }
}
