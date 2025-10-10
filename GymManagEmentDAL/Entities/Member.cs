using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagEmentDAL.Entities
{
    public class Member : GymUser
    {
        public string? Pohto { get; set; }

        #region RelationShips
        #region Member - HealthRecord

        public HealthRecord HealthRecord { get; set; } = null!;

        #endregion

        #region Member - MemberShip

        public ICollection<MemberShip> MemberShips { get; set; } = null!;
        #endregion

        #region Member - MemberSeesion

        public ICollection<MemberSession> MembersSessions { get; set; } = null!;
        #endregion


        #endregion
    }
}