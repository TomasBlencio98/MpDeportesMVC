using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MpDeportesMVC.Entidades;
using MpDeportesMVC.Servicios.Interfaces;
using MpDeportesMVC.WEB.ViewModels.Brands;
using MpDeportesMVC.WEB.ViewModels.Shoes;
using System;
using X.PagedList;

namespace MpDeportesMVC.WEB.Controllers
{
    public class BrandController : Controller
    {
        private readonly IServicioBrand? servicio;
        private readonly IWebHostEnvironment? _webHostEnvironment;
        private readonly IMapper? _mapper;

        public BrandController(IServicioBrand? servicio, IMapper? mapper,
            IWebHostEnvironment? webHostEnvironment)
        {
            this.servicio = servicio;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(int? page, string? searchTerm = null, bool viewAll = false, int pageSize = 10)
        {
            int pageNumber = page ?? 1;
            ViewBag.currentPageSize = pageSize;
            IEnumerable<Brand>? brands;
            if (!viewAll)
            {
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    brands = servicio?
                        .GetAll(orderBy: o => o.OrderBy(c => c.BrandName),
                            filter: c => c.BrandName.Contains(searchTerm));
                    ViewBag.currentSearchTerm = searchTerm;
                }
                else
                {
                    brands = servicio?.GetAll
                        (orderBy: o => o.OrderBy(c => c.BrandName));
                }
            }
            else
            {
                brands = servicio?.GetAll
                        (orderBy: o => o.OrderBy(c => c.BrandName));
            }
            
            var BrandsVm = _mapper?.Map<List<BrandListVm>>(brands)
                .ToPagedList(pageNumber, pageSize);
            foreach (var brand in BrandsVm!)
            {
                brand.ZapatillasCount = (int)(servicio?.ContarZapatillasPorMarca(brand.BrandId))!;
            }
            return View(BrandsVm);
        }

        public IActionResult UpSert(int? id)
        {
            if (servicio == null || _mapper == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Dependencias no están configuradas correctamente");
            }
            BrandEditVm BrandVm;
            if (id == null || id == 0)
            {
                BrandVm = new BrandEditVm();
            }
            else
            {
                try
                {
                    string? wwwWebRoot = _webHostEnvironment!.WebRootPath;
                    Brand? Brand = servicio.Get(filter: g => g.BrandId == id);
                    if (Brand == null)
                    {
                        return NotFound();
                    }
                    if (!string.IsNullOrEmpty(Brand.ImageUrl))
                    {
                        var filePath = Path.Combine(wwwWebRoot, Brand.ImageUrl.TrimStart('/'));
                        ViewData["ImageExist"] = System.IO.File.Exists(filePath);
                    }
                    else
                    {
                        ViewData["ImageExist"] = false;
                    }
                    BrandVm = _mapper.Map<BrandEditVm>(Brand);
                    return View(BrandVm);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the record.");
                }

            }
            return View(BrandVm);
        }


        [HttpPost]
        public IActionResult UpSert(BrandEditVm BrandVm)
        {
            if (!ModelState.IsValid)
            {
                return View(BrandVm);
            }
            if (servicio == null || _mapper == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Dependencias no están configuradas correctamente");
            }
            try
            {
                string? wwwWebRoot = _webHostEnvironment!.WebRootPath;
                //se obtiene la ruta raíz del directorio web para poder manejar archivos (como imágenes).
                Brand Brand = _mapper.Map<Brand>(BrandVm);
                if (BrandVm.RemoveImage && !string.IsNullOrEmpty(Brand.ImageUrl))
                {
                    string oldFilePath = Path.Combine(wwwWebRoot, Brand.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                    Brand.ImageUrl = null;
                    //se construye la ruta completa del archivo de imagen con Path.Combine
                    //y se usa System.IO.File.Exists para verificar si el archivo realmente
                    //existe en el servidor.Si el archivo existe, se elimina
                }
                if (BrandVm.ImageFile != null) //verificamos si ingresamos una nueva imagen
                {
                    var permittedExtensions = new string[] { ".png", ".jpg", ".jpeg", ".gif" };
                    var fileExtension = Path.GetExtension(BrandVm.ImageFile.FileName);
                    if (!permittedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError(string.Empty, "File not allowed");
                        return View(BrandVm);
                    }
                    if (Brand.ImageUrl != null) 
                        //si seleccionamos una imagen y ya teniamos otra la eliminamos
                    {
                        string oldFilePath = Path.Combine(wwwWebRoot, Brand.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(BrandVm.ImageFile.FileName)}";
                    string pathName = Path.Combine(wwwWebRoot, "images", fileName);
                    using (var fileStream = new FileStream(pathName, FileMode.Create))
                    {
                        BrandVm.ImageFile.CopyTo(fileStream);
                    }
                    Brand.ImageUrl = $"/images/{fileName}";
                }
                servicio.Save(Brand);
                TempData["success"] = "Record successfully added/edited";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while editing the record.");
                return View(BrandVm);
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var shoe = servicio?.ObtenerZapatillasPorMarca(id);
            if (shoe == null || shoe.Count == 0)
            {
                ViewData["Mensaje"] = "No hay zapatillas asociadas a esta marca.";
            }
            return View(shoe);
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            Brand? Brand = servicio?.Get(filter: g => g.BrandId == id);
            if (Brand is null)
            {
                return NotFound();
            }
            try
            {
                if (servicio == null || _mapper == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Dependencias no están configuradas correctamente");
                }
                if (servicio.EstaRelacionado(Brand.BrandId))
                {
                    return Json(new { success = false, message = "Related Record... Delete Deny!!" }); ;
                }
                servicio.Delete(Brand);
                return Json(new { success = true, message = "Record successfully deleted" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Couldn't delete record!!! " }); ;
            }
        }
        
    }
}
