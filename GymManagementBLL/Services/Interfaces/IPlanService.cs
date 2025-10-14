using GymManagementBLL.ViewModels.PlanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    internal interface IPlanService
    {
        IEnumerable<PlanViewModel>GetAllPlans();
        PlanViewModel? GetPlanById(int PlanId);

        UpdatedPlanViewModel? GetPlanToUpdate(int PlanId);

        bool UpdatePlan(int id ,UpdatedPlanViewModel UpdatedPlan);
        bool ToggleStatus(int PlanId);
    }
}
