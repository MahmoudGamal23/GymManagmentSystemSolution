using GymManagementBLL.ViewModels.PlanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IPlanService
    {
        IEnumerable<PlanViewModel> GetAllPlans();
        PlanViewModel? GetPlanDetails(int planId);
        UpdatedPlanViewModel? GetPlanToUpdate(int planId);
        bool UpdatePlan(int planId, UpdatedPlanViewModel planToUpdate);
        bool ToggleStatus(int planId);

    }
}
