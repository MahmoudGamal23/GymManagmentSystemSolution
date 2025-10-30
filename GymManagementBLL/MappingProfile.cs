using AutoMapper;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementBLL.ViewModels.PlanViewModels;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementBLL.ViewModels.TrainerViewModels;
using GymManagEmentDAL.Entities;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapSession();
            MapPlan();
            mapMember();
            MapTrainer();
        }

        private void MapSession()
        {
            CreateMap<CreateSessionViewModel, Session>().ReverseMap();
            CreateMap<Session, SessionViewModel>()
                        .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                        .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.SessionTrainer.Name))
                        .ForMember(dest => dest.AvailableSlots, opt => opt.Ignore());
            CreateMap<UpdateSessionViewModel, Session>().ReverseMap();

            CreateMap<Session, SessionViewModel>()
             .ForMember(dest => dest.TrainerName, Options => Options.MapFrom(src => src.SessionTrainer.Name))
             .ForMember(dest => dest.CategoryName, Options => Options.MapFrom(src => src.Category.CategoryName))
             .ForMember(dest => dest.AvailableSlots, Options => Options.Ignore());

            CreateMap<Trainer, TrainerSelectViewModel>();

            CreateMap<Category, CategorySelectViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName));


        }

        private void MapPlan()
        {

            CreateMap<Plan, PlanViewModel>();
            CreateMap<Plan, UpdatedPlanViewModel>();
            CreateMap<UpdatedPlanViewModel, Plan>()
           .ForMember(dest => dest.Name, opt => opt.Ignore())
           .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));


        }

        private void mapMember()
        {


            CreateMap<CreateMemberViewModel, Member>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.HealthRecord, opt => opt.MapFrom(src => src.HealthRecordViewModel));

            CreateMap<CreateMemberViewModel, Address>()
                .ForMember(dest => dest.BuldingNumber, opt => opt.MapFrom(src => src.BuldingNumber))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City));

            CreateMap<HealthRecordViewModel, HealthRecord>().ReverseMap();

            CreateMap<Member, MemberViewModel>()
              .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
              .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
              .ForMember(dest => dest.Address, opt => opt.MapFrom(src =>
                  $"{src.Address.BuldingNumber}, {src.Address.Street}, {src.Address.City}"));

            CreateMap<Member, MemberToUpdateViewModel>()
                .ForMember(dest => dest.BuldingNumber, opt => opt.MapFrom(src => src.Address.BuldingNumber))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City));

            CreateMap<MemberToUpdateViewModel, Member>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.Phone, opt => opt.Ignore())
                .AfterMap((srs, dest) =>
                {
                    dest.Address.BuldingNumber = srs.BuldingNumber;
                    dest.Address.Street = srs.Street;
                    dest.Address.City = srs.City;
                    dest.UpdatedAt = DateTime.Now;
                });


        }

        private void MapTrainer()
        {
            CreateMap<Trainer, TrainerViewModel>()
     .ForMember(dest => dest.Address, opt => opt.MapFrom(src =>
         $"{src.Address.BuldingNumber}, {src.Address.City}, {src.Address.Street}"))
     .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString()));


            CreateMap<CreateTrainerViewModel, Trainer>()
     .ForPath(dest => dest.Address.BuldingNumber, opt => opt.MapFrom(src => src.BuldingNumber))
     .ForPath(dest => dest.Address.Street, opt => opt.MapFrom(src => src.Street))
     .ForPath(dest => dest.Address.City, opt => opt.MapFrom(src => src.City));


            CreateMap<TrainerToUpdateViewModel, Trainer>()
    .ForPath(dest => dest.Address.BuldingNumber, opt => opt.MapFrom(src => src.BuildingNumber))
    .ForPath(dest => dest.Address.Street, opt => opt.MapFrom(src => src.Street))
    .ForPath(dest => dest.Address.City, opt => opt.MapFrom(src => src.City));


            CreateMap<Trainer, TrainerToUpdateViewModel>()
      .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuldingNumber))
      .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
      .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City));

        }
    }

}
