using GymManagementBLL.ViewModels.TrainerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    internal interface ITrainerService
    {
        IEnumerable<TrainerViewModel> GetTrainers();
        bool CreateTrainer(CreateTrainerViewModel createTrainer);
        TrainerToUpdateViewModel? GetTrainerToUpdate(int trainerId);
        bool UpdateTrainerDetails(UpdateToTrainerViewModel updateTrainer, int trainerId);
        bool RemoveTrainer(int trainerId);
        TrainerViewModel? GetTrainerDetails(int trainerId);
    }
}
