﻿using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.AnalyticsViewModels;
using GymManagementDAL.Repositories.Interfaces;
using GymManagEmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    public class AnalyticService : IAnalyticService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnalyticService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public AnalyticsViewModel GetAnalyticsData()
        {
            var Sessions = _unitOfWork.SessionRepository.GetAll();
            return new AnalyticsViewModel()
            {
                ActiveMember = _unitOfWork.GetRepository<MemberShip>().GetAll(X => X.Status == "Active").Count(),
                TotalMembers = _unitOfWork.GetRepository<Member>().GetAll().Count(),
                TotalTrainers = _unitOfWork.GetRepository<Trainer>().GetAll().Count(),
                UpcomingSessions = Sessions.Count(X => X.StartDate > DateTime.Now),
                OngoingSessions = Sessions.Count(X => X.StartDate <= DateTime.Now && X.EndDate > DateTime.Now),
                CompletedSessions = Sessions.Count(X => X.EndDate < DateTime.Now),
            };
        }
    }
}
