using AutoMapper;
using Mono.TextTemplating;
using MpDeportesMVC.Entidades;
using MpDeportesMVC.WEB.ViewModels.Brands;
using MpDeportesMVC.WEB.ViewModels.Colour;
using MpDeportesMVC.WEB.ViewModels.Genres;
using MpDeportesMVC.WEB.ViewModels.Shoes;
using MpDeportesMVC.WEB.ViewModels.Sizes;
using MpDeportesMVC.WEB.ViewModels.Sports;

namespace MpDeportesMVC.WEB.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            LoadGenresMapping();
            LoadBrandMapping();
            LoadSportMapping();
            LoadColourMapping();
            LoadSizeMapping();
            LoadShoeMapping();
        }

        private void LoadShoeMapping()
        {
            CreateMap<Shoe, ShoeListVm>()
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand!.BrandName))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre!.GenreName))
            .ForMember(dest => dest.Sport, opt => opt.MapFrom(src => src.Sport!.SportName))
            .ForMember(dest => dest.Colour, opt => opt.MapFrom(src => src.Colour!.ColourName));

            CreateMap<Shoe, ShoeEditVm>().ReverseMap();
        }

        private void LoadSizeMapping()
        {
            CreateMap<Size, SizeListVm>();
            CreateMap<Size, SizeEditVm>().ReverseMap();
        }

        private void LoadColourMapping()
        {
            CreateMap<Colour, ColourListVm>();
            CreateMap<Colour, ColourEditVm>().ReverseMap();
        }

        private void LoadSportMapping()
        {
            CreateMap<Sport, SportListVm>();
            CreateMap<Sport, SportEditVm>().ReverseMap();
        }

        private void LoadBrandMapping()
        {
            CreateMap<Brand, BrandListVm>();
            CreateMap<Brand, BrandEditVm>().ReverseMap();
        }

        private void LoadGenresMapping()
        {
            CreateMap<Genre, GenreListVm>();
            CreateMap<Genre, GenreEditVm>().ReverseMap();
        }
    }
}
