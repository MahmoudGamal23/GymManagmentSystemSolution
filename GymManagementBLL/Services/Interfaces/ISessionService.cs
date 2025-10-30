using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSessions();

        SessionViewModel? GetSessionById(int sessionId);
        bool CreateSession(CreateSessionViewModel ceateSession);
        UpdateSessionViewModel? GetSessionForUpdate(int sessionId);
        bool UpdateSession(UpdateSessionViewModel updateSession, int sessionId);
        bool RemoveSession(int sessionId);

        IEnumerable<TrainerSelectViewModel> GetAllTrainersForDropDown();
        IEnumerable<CategorySelectViewModel> GetAllCategoryForDropDown();
    }
}
