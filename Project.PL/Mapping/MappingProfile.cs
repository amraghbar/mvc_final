using AutoMapper;
using Project.PL.Areas.Admin.ViewModels.Featured;
using Project.PL.Areas.Admin.ViewModels.InspiredProducts;
using Project.PL.Areas.Admin.ViewModels.LatestBlogs;
using Project.PL.Areas.Admin.ViewModels.NewProduct;
using Project.PL.Areas.Admin.ViewModels.Service;
using Project.PL.Areas.Admin.ViewModels.Offer;

using Project_.DAL.Models;
using Project.PL.Areas.Admin.ViewModels.Offers;

namespace Project.PL.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile() {

            CreateMap<ServiceFormVM, Service>().ReverseMap();
            CreateMap<Service, ServiceVM>().ReverseMap();
            CreateMap<Service, ServiceDetailsVM>().ReverseMap();
            CreateMap<Service, ServiceEditVM>().ReverseMap();

            CreateMap<FeaturedFormVM, Featured>().ReverseMap();
            CreateMap<Featured, FeaturedVM>().ReverseMap();
            CreateMap<Featured, FeaturedDetailsVM>().ReverseMap();
           CreateMap<Featured, FeaturedEditVM>().ReverseMap();

            CreateMap<NewProductFormVM, NewProd>().ReverseMap();
            CreateMap<NewProd, NewProductVM>().ReverseMap();
            CreateMap<NewProd, NewProductDetailsVM>().ReverseMap();
            CreateMap<NewProd, NewProductEditVM>().ReverseMap();

            CreateMap<InspiredProductsFormVM, Inspired>().ReverseMap();
            CreateMap<Inspired, InspiredProductsVM>().ReverseMap();
            CreateMap<Inspired, InspiredProductsDetailsVM>().ReverseMap();
            CreateMap<Inspired, InspiredProductsEditVM>().ReverseMap();


            CreateMap<LatestBlogsFormVM, latest>().ReverseMap();
            CreateMap<latest, LatestBlogsVM>().ReverseMap();
            CreateMap<latest, LatestBlogsDetailsVM>().ReverseMap();
            CreateMap<latest, LatestBlogsEditVM>().ReverseMap();



            CreateMap<OffersFormVM,Offer>().ReverseMap();
            CreateMap<Offer, OffersVM>().ReverseMap();
            CreateMap<Offer, OffersDetailsVM>().ReverseMap();
            CreateMap<Offer, OffersEditVM>().ReverseMap();

            CreateMap<Inspired, CartItem>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Af_Price));

            CreateMap<Featured, CartItem>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Af_Price));

            CreateMap<NewProd, CartItem>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Af_Price));

        }
    }
}
