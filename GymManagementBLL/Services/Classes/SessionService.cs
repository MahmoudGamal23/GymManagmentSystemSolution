using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Repositories.Interfaces;
using GymManagEmentDAL.Entities;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    public class SessionService : ISessionService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool CreateSession(CreateSessionViewModel ceateSession)
        {
            try
            {
                if (!IsTrainerExists(ceateSession.TrainerId)) return false;
                if (!IsCategoryExists(ceateSession.CategoryId)) return false;
                if (!IsValidDateRange(ceateSession.StartDate, ceateSession.EndDate)) return false;
                var session = _mapper.Map<CreateSessionViewModel, Session>(ceateSession);
                _unitOfWork.SessionRepository.Add(session);

                return _unitOfWork.SaveChanges() > 0;

            }
            catch
            {
                return false;
            }



        }

        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessions = _unitOfWork.SessionRepository.GetAllSessionsWithTrainerAndCategories();
            if (sessions is null || !sessions.Any()) return [];

            #region Manual mapp

            //return sessions.Select(x => new SessionViewModel()
            //{
            //    Id = x.Id,
            //    Capacity = x.Capacity,
            //    EndDate = x.EndDate,
            //    StartDate = x.StartDate,
            //    Description = x.Description,
            //    TrainerName = x.SessionsTrainer.Name,
            //    CategoryName = x.Category.CategoryName,
            //    AvailableSlots = x.Capacity - _unitOfWork.sessionRepository.GetCountOfBookSlots(x.Id)

            //});
            #endregion

            #region Auta Mapping
            var MappedSessions = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(sessions);
            return MappedSessions;
            #endregion
        }

        public SessionViewModel? GetSessionById(int sessionId)
        {
            var session = _unitOfWork.SessionRepository.GetSessionByIdWithTrainerAndCategories(sessionId);
            if (session is null) return null;
            #region Manual mapp
            //return new SessionViewModel()
            //{
            //    Id = session.Id,
            //    Capacity = session.Capacity,
            //    EndDate = session.EndDate,
            //    StartDate = session.StartDate,
            //    Description = session.Description,
            //    TrainerName = session.SessionsTrainer.Name,
            //    CategoryName = session.Category.CategoryName,
            //    AvailableSlots = session.Capacity - _unitOfWork.sessionRepository.GetCountOfBookSlots(session.Id)
            //};
            #endregion
            #region Auta Mapping

            return _mapper.Map<Session, SessionViewModel>(session);

            #endregion

        }

        public UpdateSessionViewModel? GetSessionForUpdate(int sessionId)
        {
            var session = _unitOfWork.SessionRepository.GetById(sessionId);
            if (!IsSessionAvailableForUpdateing(session!)) return null;
            return _mapper.Map<UpdateSessionViewModel>(session!);


        }

        public bool UpdateSession(UpdateSessionViewModel updateSession, int sessionId)
        {
            try
            {
                var session = _unitOfWork.SessionRepository.GetById(sessionId);
                if (!IsSessionAvailableForUpdateing(session!)) return false;
                if (!IsTrainerExists(updateSession.TrainerId)) return false;
                if (!IsValidDateRange(updateSession.StartDate, updateSession.EndDate)) return false;
                _mapper.Map(updateSession, session);
                session!.UpdatedAt = DateTime.Now;
                _unitOfWork.SessionRepository.Update(session!);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveSession(int sessionId)
        {
            try
            {
                var session = _unitOfWork.SessionRepository.GetById(sessionId);
                if (!IsSessionAvailableForRemoving(session!)) return false;
                _unitOfWork.SessionRepository.Delete(session!);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }


        }
        public IEnumerable<TrainerSelectViewModel> GetAllTrainersForDropDown()
        {
            var trainers = _unitOfWork.GetRepository<Trainer>().GetAll();
            return _mapper.Map<IEnumerable<TrainerSelectViewModel>>(trainers);
        }

        public IEnumerable<CategorySelectViewModel> GetAllCategoryForDropDown()
        {
            var categories = _unitOfWork.GetRepository<Category>().GetAll();
            return _mapper.Map<IEnumerable<CategorySelectViewModel>>(categories);
        }


        #region Helpers
        public bool IsTrainerExists(int trainerId)
        {
            return _unitOfWork.GetRepository<Trainer>().GetById(trainerId) is not null;
        }
        public bool IsCategoryExists(int categoryId)
        {
            return _unitOfWork.GetRepository<Category>().GetById(categoryId) is not null;
        }
        public bool IsValidDateRange(DateTime startDate, DateTime endDate)
        {
            return startDate < endDate && startDate > DateTime.Now;
        }


        public bool IsSessionAvailableForUpdateing(Session session)
        {
            if (session is null) return false;

            if (session.EndDate < DateTime.Now) return false;

            if (session.StartDate <= DateTime.Now) return false;

            var HasActiveBookings = _unitOfWork.SessionRepository.GetcountOfBookedSlots(session.Id) > 0;

            if (HasActiveBookings) return false;

            return true;


        }
        public bool IsSessionAvailableForRemoving(Session session)
        {
            if (session is null) return false;

            if (session.StartDate > DateTime.Now) return false;

            if (session.StartDate <= DateTime.Now && session.EndDate > DateTime.Now) return false;

            var HasActiveBookings = _unitOfWork.SessionRepository.GetcountOfBookedSlots(session.Id) > 0;

            if (HasActiveBookings) return false;

            return true;


        }


        #endregion
    }
}

