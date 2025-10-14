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
    internal class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PlanService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var Plans = _unitOfWork.GetRepository<Plan>().GetAll();
            if (Plans == null || !Plans.Any()) return [];
            return Plans.Select(X => new PlanViewModel()
            {
                Id = X.Id,
                Name = X.Name,
                Description = X.Description,
                DurationDays = X.DurationDays,
                IsActive = X.IsActive,
                Price = X.Price,
            });
        }

        public PlanViewModel? GetPlanById(int PlanId)
        {
           var plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (plan == null) return null;
            return new PlanViewModel()
            {
                Id= plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                IsActive = plan.IsActive,
                Price = plan.Price,
            };
        }

        public UpdatedPlanViewModel? GetPlanToUpdate(int PlanId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (plan == null || HasActiveMemberShip(PlanId)|| plan.IsActive) return null;
            return new UpdatedPlanViewModel()
            {

                PlanName = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Price = plan.Price,
            };
        }
        public bool UpdatePlan(int id, UpdatedPlanViewModel UpdatedPlan)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(id);
            if (plan == null || HasActiveMemberShip(id)) return false;
            try
            {
                (plan.Description, plan.DurationDays, plan.Price, plan.Name, plan.UpdatedAt) =
            (UpdatedPlan.Description, UpdatedPlan.DurationDays, UpdatedPlan.Price, UpdatedPlan.PlanName, DateTime.Now);
                _unitOfWork.GetRepository<Plan>().Update(plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch 
            {
                return false;
            }
        }

        public bool ToggleStatus(int PlanId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (plan == null || HasActiveMemberShip(PlanId)) return false;

            plan.IsActive = plan.IsActive == true ? false : true;

            try
            {
                plan.UpdatedAt = DateTime.Now;
                _unitOfWork.GetRepository<Plan>().Update(plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch 
            { 
                return false;
            }
        }

      
        #region Helper Method
        private bool HasActiveMemberShip(int planId)
        {
            var ActiveMemberShip = _unitOfWork.GetRepository<MemberShip>()
                .GetAll(X=> X.PlanId == planId && X.Status == "Active").Any();
            return ActiveMemberShip;
        }
        #endregion
    }
}
