using AutoMapper;
using Project.PL.Areas.Admin.ViewModels.Service;
using Project_.DAL.Models;

namespace Project.PL.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile() {
            CreateMap<ServiceFormVM,Service>().ReverseMap();
            CreateMap<Service, ServiceVM>().ReverseMap();
            CreateMap<Service, ServiceDetailsVM>().ReverseMap();
            
                            CreateMap<Service, ServiceEditVM>().ReverseMap();

        }
    }
}
