using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.PlanViewModels;
using GymManagementDAL.Repositories.Interfaces;
using GymManagEmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    public class PlanService : IPlanService
    {
        public IUnitOfWork _unitOfWork { get; }
        public IMapper _Mapper { get; }

        public PlanService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _Mapper = mapper;
        }


        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var plans = _unitOfWork.GetRepository<Plan>().GetAll();
            if (plans == null || !plans.Any()) return [];
            //return plans.Select(p => new PlanViewModel
            //{
            //    Id = p.Id,
            //    Name = p.Name,
            //    Description = p.Description,
            //    DurationDays = p.DurationDays,
            //    Price = p.Price,
            //    IsActive = p.IsActive
            //}).ToList();
            var MappedPlans = _Mapper.Map<IEnumerable<Plan>, IEnumerable<PlanViewModel>>(plans);
            return MappedPlans;

        }

        public PlanViewModel? GetPlanDetails(int planId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(planId);
            if (plan == null) return null;
            //return new PlanViewModel
            //{
            //    Id = plan.Id,
            //    Name = plan.Name,
            //    Description = plan.Description,
            //    DurationDays = plan.DurationDays,
            //    Price = plan.Price,
            //    IsActive = plan.IsActive
            //};
            var MappedPlans = _Mapper.Map<Plan, PlanViewModel>(plan);
            return MappedPlans;

        }

        public UpdatedPlanViewModel? GetPlanToUpdate(int planId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(planId);
            if (plan == null || plan.IsActive == false || HasActiveMemberShips(planId)) return null;

            var MappedPlans = _Mapper.Map<Plan, UpdatedPlanViewModel>(plan);
            return MappedPlans;


        }

        public bool UpdatePlan(int planId, UpdatedPlanViewModel planToUpdate)
        {
            try
            {
                var PlanRepo = _unitOfWork.GetRepository<Plan>();
                var plan = PlanRepo.GetById(planId);
                if (plan == null || HasActiveMemberShips(planId)) return false;

                _Mapper.Map(planToUpdate, plan);
                plan.UpdatedAt = DateTime.Now;
                PlanRepo.Update(plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool ToggleStatus(int planId)
        {
            var PlanRepo = _unitOfWork.GetRepository<Plan>();
            var plan = PlanRepo.GetById(planId);

            if (plan is null || HasActiveMemberShips(planId)) return false;

            plan.IsActive = plan.IsActive == true ? false : true;
            try
            {

                PlanRepo.Update(plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch { return false; }


        }


        #region Helper Methods
        private bool HasActiveMemberShips(int PlanId)
        {
            return _unitOfWork.GetRepository<MemberShip>()
                .GetAll(X => X.PlanId == PlanId && X.Status == "Active").Any();
        }
        #endregion
    }
}


