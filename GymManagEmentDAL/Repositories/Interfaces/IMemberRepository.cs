using GymManagEmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface IMemberRepository
    {
        // Get all members
        IEnumerable<Member> GetAll();

        // Get member by Id
        Member? GetById(int id);

        // Add new member
        int Add(Member member);

        // Update member
        int Update(Member member);

        // Delete member
        int Delete(int id);
    }
}
