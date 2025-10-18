﻿using GymManagementDAL.Repositories.Interfaces;
using GymManagEmentDAL.Data.Contexts;
using GymManagEmentDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Classes
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymDbContext _dbContext;

        public SessionRepository(GymDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Session> GetAllSessionsWithTrainerAndCategories()
        {
            return _dbContext.Sessions.Include(X => X.SessionTrainer)
                                      .Include(X => X.Category).ToList();
        }

        public int GetcountOfBookedSlots(int sessionId)
        {
            return _dbContext.MemberSessions.Count(X => X.SessionId == sessionId);
        }

        public Session? GetSessionByIdWithTrainerAndCategories(int Id)
        {
            return _dbContext.Sessions.Include(X => X.SessionTrainer)
                                      .Include(X => X.Category)
                                      .FirstOrDefault(X => X.Id == Id);
        }
    }
}
