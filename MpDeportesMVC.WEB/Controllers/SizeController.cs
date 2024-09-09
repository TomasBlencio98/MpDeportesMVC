using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MpDeportesMVC.Entidades;
using MpDeportesMVC.Servicios.Interfaces;
using MpDeportesMVC.WEB.ViewModels.Sizes;
using X.PagedList;

namespace MpDeportesMVC.WEB.Controllers
{
    public class SizeController : Controller
    {
        private readonly IServicioSize? servicio;
        private readonly IMapper? _mapper;

        public SizeController(IServicioSize? servicio, IMapper? mapper)
        {
            this.servicio = servicio;
            _mapper = mapper;
        }

        public IActionResult Index(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 10;
            var Sizes = servicio?.GetAll
                (orderBy: o => o.OrderBy(c => c.SizeNumber));
            var SizesVm = _mapper?.Map<List<SizeListVm>>(Sizes)
                .ToPagedList(pageNumber, pageSize);
            return View(SizesVm);
        }

        public IActionResult UpSert(int? id)
        {
            if (servicio == null || _mapper == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Dependencias no están configuradas correctamente");
            }
            SizeEditVm SizeVm;
            if (id == null || id == 0)
            {
                SizeVm = new SizeEditVm();
            }
            else
            {
                try
                {
                    Size? Size = servicio.Get(filter: g => g.SizeId == id);
                    if (Size == null)
                    {
                        return NotFound();
                    }
                    SizeVm = _mapper.Map<SizeEditVm>(Size);
                    return View(SizeVm);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the record.");
                }

            }
            return View(SizeVm);

        }


        [HttpPost]
        public IActionResult UpSert(SizeEditVm SizeVm)
        {
            if (!ModelState.IsValid)
            {
                return View(SizeVm);
            }

            if (servicio == null || _mapper == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Dependencias no están configuradas correctamente");
            }

            try
            {
                Size Size = _mapper.Map<Size>(SizeVm);

                if (servicio.Existe(Size))
                {
                    ModelState.AddModelError(string.Empty, "Record already exist");
                    return View(SizeVm);
                }

                servicio.Save(Size);
                TempData["success"] = "Record successfully added/edited";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while editing the record.");
                return View(SizeVm);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            Size? Size = servicio?.Get(filter: g => g.SizeId == id);
            if (Size is null)
            {
                return NotFound();
            }
            try
            {
                if (servicio == null || _mapper == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Dependencias no están configuradas correctamente");
                }
                if (servicio.EstaRelacionado(Size.SizeId))
                {
                    return Json(new { success = false, message = "Related Record... Delete Deny!!" }); ;
                }
                servicio.Delete(Size);
                return Json(new { success = true, message = "Record successfully deleted" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Couldn't delete record!!! " }); ;
            }
        }
    }

}
