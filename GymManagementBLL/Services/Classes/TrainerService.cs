using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.TrainerViewModels;
using GymManagementDAL.Repositories.Interfaces;
using GymManagEmentDAL.Entities;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    internal class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TrainerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool CreateTrainer(CreateTrainerViewModel createTrainer)
        {
            try
            {
                var Repo = _unitOfWork.GetRepository<Trainer>();
                if (IsEmailExist(createTrainer.Email) || IsPhoneExist(createTrainer.Phone)) return false;
                var Trainer = new Trainer()
                {
                    Name = createTrainer.Name,
                    Email = createTrainer.Email,
                    Phone = createTrainer.Phone,
                    Gender = createTrainer.Gender,
                    Specialties = createTrainer.Specialties,
                    DateOfBirth = createTrainer.DateOfBirth,
                    Address = new Address()
                    {
                        BuldingNumber = createTrainer.BuldingNumber,
                        City = createTrainer.City,
                        Street = createTrainer.Street,

                    }
                };
                Repo.Add(Trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;

            }
        }
        public IEnumerable<TrainerViewModel> GetTrainers()
        {
            {
                var Trainers = _unitOfWork.GetRepository<Trainer>().GetAll();
                if (Trainers is null || Trainers.Any()) return [];

                return Trainers.Select(x => new TrainerViewModel()
                {
                    Name = x.Name,
                    Email = x.Email,
                    Phone = x.Phone,
                    Id = x.Id,
                    Specialties = x.Specialties.ToString()

                });
            }
        }
        public TrainerViewModel? GetTrainerDetails(int trainerId)
        {
            var Trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if (Trainer is null) return null;
            return new TrainerViewModel()
            {
                Name = Trainer.Name,
                Email = Trainer.Email,
                Phone = Trainer.Phone,
                Specialties = Trainer.Specialties.ToString()

            };
        }
      
        public TrainerToUpdateViewModel? GetTrainerToUpdate(int trainerId)
        {
            var Trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if (Trainer is null) return null;

            return new TrainerToUpdateViewModel()
            {
                Name = Trainer.Name,
                Email = Trainer.Email,
                Phone = Trainer.Phone,
                City = Trainer.Address.City,
                Street = Trainer.Address.Street,
                BuildingNumber = Trainer.Address.BuldingNumber,
                Specialties = Trainer.Specialties
            };
        }
        public bool RemoveTrainer(int trainerId)
        {
            var Repo = _unitOfWork.GetRepository<Trainer>();
            var TrainerToRemove = Repo.GetById(trainerId);
            if (TrainerToRemove is null || HasActiveSessions(trainerId)) return false;
            Repo.Delete(TrainerToRemove);
            return _unitOfWork.SaveChanges() > 0;

        }
        public bool UpdateTrainerDetails(UpdateToTrainerViewModel updatedTrainer, int trainerId)
        {
            var Repo = _unitOfWork.GetRepository<Trainer>();
            var TrainerToUpdate = Repo.GetById(trainerId);
            if (TrainerToUpdate is null || IsEmailExist(updatedTrainer.Email) || IsPhoneExist(updatedTrainer.Phone)) return false;

            TrainerToUpdate.Email = updatedTrainer.Email;
            TrainerToUpdate.Phone = updatedTrainer.Phone;
            TrainerToUpdate.Address.BuldingNumber = updatedTrainer.BuildingNumber;
            TrainerToUpdate.Address.City = updatedTrainer.City;
            TrainerToUpdate.Address.Street = updatedTrainer.Street;
            TrainerToUpdate.Specialties = updatedTrainer.Specialties;
            TrainerToUpdate.UpdatedAt = DateTime.Now;
            Repo.Update(TrainerToUpdate);

            return _unitOfWork.SaveChanges() > 0;
        }


        #region Helper Methods
        private bool IsEmailExist(string email)
        {
            var exising = _unitOfWork.GetRepository<Trainer>().GetAll(
               m => m.Email == email).Any();
            return exising;
        }
        private bool IsPhoneExist(string Phone)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(X => X.Phone == Phone).Any();
        }


        private bool HasActiveSessions(int trainerId)
        {
            return _unitOfWork.GetRepository<Session>()
                .GetAll(X => X.TrainerId == trainerId && X.Description == "Active").Any();
        }

       
        #endregion
    }
}

