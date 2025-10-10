﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagEmentDAL.Entities
{
    public class Session : BaseEntity
    {
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        #region RelationShips
        #region Category - Session
        public int CategoryId { get; set; }
        public Category SessionCategory { get; set; } = null!;
        #endregion

        #region Trainer - Session

        public int TrainerId { get; set; }
        public Trainer SessionTrainer { get; set; } = null!;
        #endregion

        #region Session - MemberSession

        public ICollection<MemberSession> SessionMembers { get; set; } = null!;

        #endregion
        #endregion
    }
}
