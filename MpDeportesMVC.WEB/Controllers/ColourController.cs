using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MpDeportesMVC.Entidades;
using MpDeportesMVC.Servicios.Interfaces;
using MpDeportesMVC.WEB.ViewModels.Colour;
using X.PagedList;

namespace MpDeportesMVC.WEB.Controllers
{
    public class ColourController : Controller
    {
        private readonly IServicioColour? servicio;
        private readonly IMapper? _mapper;

        public ColourController(IServicioColour? servicio, IMapper? mapper)
        {
            this.servicio = servicio;
            _mapper = mapper;
        }

        public IActionResult Index(int? page, string? searchTerm = null, bool viewAll = false, int pageSize = 10)
        {
            int pageNumber = page ?? 1;
            ViewBag.currentPageSize = pageSize;
            IEnumerable<Colour>? colours;
            if (!viewAll)
            {
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    colours = servicio?
                        .GetAll(orderBy: o => o.OrderBy(c => c.ColourName),
                            filter: c => c.ColourName.Contains(searchTerm));
                    ViewBag.currentSearchTerm = searchTerm;
                }
                else
                {
                    colours = servicio?.GetAll
                        (orderBy: o => o.OrderBy(c => c.ColourName));
                }
            }
            else
            {
                colours = servicio?.GetAll
                        (orderBy: o => o.OrderBy(c => c.ColourName));
            }

            var coloursVm = _mapper?.Map<List<ColourListVm>>(colours)
                .ToPagedList(pageNumber, pageSize);
            return View(coloursVm);
        }

        public IActionResult UpSert(int? id)
        {
            if (servicio == null || _mapper == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Dependencias no están configuradas correctamente");
            }
            ColourEditVm ColourVm;
            if (id == null || id == 0)
            {
                ColourVm = new ColourEditVm();
            }
            else
            {
                try
                {
                    Colour? Colour = servicio.Get(filter: g => g.ColourId == id);
                    if (Colour == null)
                    {
                        return NotFound();
                    }
                    ColourVm = _mapper.Map<ColourEditVm>(Colour);
                    return View(ColourVm);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the record.");
                }

            }
            return View(ColourVm);

        }


        [HttpPost]
        public IActionResult UpSert(ColourEditVm ColourVm)
        {
            if (!ModelState.IsValid)
            {
                return View(ColourVm);
            }

            if (servicio == null || _mapper == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Dependencias no están configuradas correctamente");
            }

            try
            {
                Colour Colour = _mapper.Map<Colour>(ColourVm);

                if (servicio.Existe(Colour))
                {
                    ModelState.AddModelError(string.Empty, "Record already exist");
                    return View(ColourVm);
                }

                servicio.Save(Colour);
                TempData["success"] = "Record successfully added/edited";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while editing the record.");
                return View(ColourVm);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            Colour? Colour = servicio?.Get(filter: g => g.ColourId == id);
            if (Colour is null)
            {
                return NotFound();
            }
            try
            {
                if (servicio == null || _mapper == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Dependencias no están configuradas correctamente");
                }
                if (servicio.EstaRelacionado(Colour.ColourId))
                {
                    return Json(new { success = false, message = "Related Record... Delete Deny!!" }); ;
                }
                servicio.Delete(Colour);
                return Json(new { success = true, message = "Record successfully deleted" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Couldn't delete record!!! " }); ;
            }
        }
    }
}
