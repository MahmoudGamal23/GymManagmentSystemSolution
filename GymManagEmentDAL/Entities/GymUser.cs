﻿using GymManagEmentDAL.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagEmentDAL.Entities
{
    public abstract class GymUser : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Address Address { get; set; } = null!;
    }
    [Owned]
    public class Address
    {
        public int BuldingNumber { get; set; }
        public string Street { get; set; }=null!;
        public string City { get; set; } =null!;

    }
}
