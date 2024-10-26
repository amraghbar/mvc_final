using AutoMapper;
using Project.PL.Areas.Admin.ViewModels.Featured;
using Project.PL.Areas.Admin.ViewModels.InspiredProducts;
using Project.PL.Areas.Admin.ViewModels.LatestBlogs;
using Project.PL.Areas.Admin.ViewModels.NewProduct;
using Project.PL.Areas.Admin.ViewModels.Service;
using Project.PL.Areas.Admin.ViewModels.Offer;

using Project_.DAL.Models;
using Project.PL.Areas.Admin.ViewModels.Offers;
using Project.PL.Areas.Admin.ViewModels.Product;
using Project.PL.Areas.Admin.ViewModels.Item;

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

           
            CreateMap<ProductsFormVM, Product>().ReverseMap();
            CreateMap<Product, ProductsVM>().ReverseMap();
            CreateMap<Product, ProductsDetailsVM>().ReverseMap();
            CreateMap<Product, ProductsEditVM>().ReverseMap();

            CreateMap<ItemFormVM, Item>().ReverseMap();
            CreateMap<Item, ItemVM>().ReverseMap();
            CreateMap<Item, ItemDetailsVM>().ReverseMap();
            CreateMap<Item, ItemEditVM>().ReverseMap();

        }
    }
}
