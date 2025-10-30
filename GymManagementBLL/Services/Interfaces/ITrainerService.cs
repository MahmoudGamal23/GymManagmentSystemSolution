using GymManagementBLL.ViewModels.TrainerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    public interface ITrainerService
    {
        IEnumerable<TrainerViewModel> GetAllTrainers();
        bool CreateTrainer(CreateTrainerViewModel createTrainer);
        TrainerToUpdateViewModel? GetTrainerToUpdate(int trainerId);
        bool UpdateTrainerDetails(TrainerToUpdateViewModel updateTrainer, int trainerId);
        bool RemoveTrainer(int trainerId);
        TrainerViewModel? GetTrainerDetails(int trainerId);
    }
}
