using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementDAL.Repositories.Classes;
using GymManagementDAL.Repositories.Interfaces;
using GymManagEmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    internal class MemberService : IMemberService
    {
        
       private readonly IUnitOfWork _unitOfWork;
        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var Members = _unitOfWork.GetRepository<Member>().GetAll();
            if (Members == null || Members.Any()) return [];
            // Member = MemberViewModel => Mapping
            #region Way01
            //var MemberViewModels = new List<MemberViewModel>();
            //foreach (var Member in Members)
            //{
            //    var MemberViewModel = new MemberViewModel()
            //    {
            //        Id = Member.Id,
            //        Name = Member.Name,
            //        Phone = Member.Phone,
            //        Email = Member.Email,
            //        Photo = Member.Phone,
            //        Gender = Member.Gender.ToString(),
            //    };
            //    MemberViewModel.Add(MemberViewModel);
            //}
            #endregion
            var MemberViewModels = Members.Select(X => new MemberViewModel()
            {
                Id = X.Id,
                Name = X.Name,
                Phone = X.Phone,
                Email = X.Email,
                Photo = X.Phone,
                Gender = X.Gender.ToString(),


            });
            return MemberViewModels;
        }


        public bool CreateMember(CreateMemberViewModel createMember)
        {
            try
            {
                if (IsEmailExists(createMember.Email) || IsPhoneExists(createMember.Phone)) return false;

                var member = new Member()
                {
                    Name = createMember.Name,
                    Phone = createMember.Phone,
                    Email = createMember.Email,
                    Gender = createMember.Gender,
                    DateOfBirth = createMember.DateOfBirth,
                    Address = new Address()
                    {

                        BuldingNumber = createMember.BuldingNumber,
                        City = createMember.City,
                        Street = createMember.Street,
                    },
                    HealthRecord = new HealthRecord()
                    {
                        Height = createMember.HealthRecordViewModel.Height,
                        Weight = createMember.HealthRecordViewModel.Weight,
                        BloodTybe = createMember.HealthRecordViewModel.BloodType,
                        Note = createMember.HealthRecordViewModel.Note,
                    }

                };
                _unitOfWork.GetRepository<Member>().Add(member);
                return _unitOfWork.SaveChanges() > 0;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public MemberViewModel? GetMemberDetails(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
            if (member is null) return null;

            var memberViewModel = new MemberViewModel()
            {
                Name = member.Name,
                Phone = member.Phone,
                Email = member.Email,
                Photo = member.Pohto,
                Gender = member.Gender.ToString(),
                DateOfBirth = member.DateOfBirth.ToShortDateString(),
                Address = $"{member.Address.BuldingNumber}-{member.Address.Street}-{member.Address.City}"
            };

            var ActiveMemberShip = _unitOfWork.GetRepository<MemberShip>()
                .GetAll(X => X.MemberId == memberId && X.Status == "Active").FirstOrDefault();

            if (ActiveMemberShip is not null)
            {
                memberViewModel.MembershipStartDate = ActiveMemberShip.CreatedAt.ToShortDateString();
                memberViewModel.MembershipEndDate = ActiveMemberShip.EndDate.ToShortDateString();
                var plan = _unitOfWork.GetRepository<Plan>().GetById(ActiveMemberShip.PlanId);
                memberViewModel.PlanName = plan?.Name;
            }
            return memberViewModel;
        }

        public HealthRecordViewModel? GetMemberHealthRecordDetails(int memberId)
        {
            var MemberHealthRecord = _unitOfWork.GetRepository<HealthRecord>().GetById(memberId);
            if (MemberHealthRecord is null) return null;
            return new HealthRecordViewModel()
            {
                BloodType = MemberHealthRecord.BloodTybe,
                Height = MemberHealthRecord.Height,
                Weight = MemberHealthRecord.Weight,
                Note = MemberHealthRecord.Note,
            };
        }

        public MemberToUpdateViewModel? GetMemberToUpdate(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
            if (member is null) return null;
            return new MemberToUpdateViewModel()
            {
                Email = member.Email,
                Name = member.Name,
                Phone = member.Phone,
                Photo = member.Pohto,
                BuldingNumber = member.Address.BuldingNumber,
                City = member.Address.City,
                Street = member.Address.Street,
            };
        }

        public bool UpdateMemberDetails(int Id, MemberToUpdateViewModel UpdateMember)
        {
            try
            {
                if (IsEmailExists(UpdateMember.Email) || IsPhoneExists(UpdateMember.Phone)) return false;
                var Repo = _unitOfWork.GetRepository<Member>();

                var member = Repo.GetById(Id);
                if (member is null) return false;
                member.Email = UpdateMember.Email;
                member.Phone = UpdateMember.Phone;
                member.Address.BuldingNumber = UpdateMember.BuldingNumber;
                member.Address.City = member.Address.City;
                member.Address.Street = member.Address.Street;
                member.UpdatedAt = DateTime.Now;
                Repo.Update(member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool RemoveMember(int MemberId)
        {
            var MemberRepo = _unitOfWork.GetRepository<Member>();
            var MemberShipRepo = _unitOfWork.GetRepository<MemberShip>();
            var member = MemberRepo.GetById(MemberId);
            if (member is null) return false;

            
            var ActiveMemberSession = _unitOfWork.GetRepository<MemberSession>().
            GetAll(X => X.MemberId == MemberId && X.Session.StartDate > DateTime.Now).Any();
            if (ActiveMemberSession) return false;

            var MemberShips = MemberShipRepo.GetAll(X => X.MemberId == MemberId);
            try
            {
                if (MemberShips.Any())
                {
                    foreach (var memberShip in MemberShips)
                        MemberShipRepo.Delete(memberShip);
                }
                MemberRepo.Delete(member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        #region Helper Methods

        private bool IsEmailExists(string Email)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(X => X.Email == Email).Any();
        }
        private bool IsPhoneExists(string Phone)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(X => X.Phone == Phone).Any();

        }

        

        #endregion
    }
}
