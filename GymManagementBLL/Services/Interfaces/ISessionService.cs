﻿using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    internal interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSessions();
        SessionViewModel? GetSessionById(int id);
        bool CreateSession(CreateSessionViewModel createSession);
        UpdateSessionViewModel? GetSessionToUpdate(int sessionId);
        bool UpdateSession(UpdateSessionViewModel updateSession, int sessionId);
        bool RemoveSession(int sessionId);

    }
}
