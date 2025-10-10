﻿using GymManagEmentDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagEmentDAL.Data.Configuration
{
    internal class TrainerConfiguration : GymUserConfiguration <Trainer> , IEntityTypeConfiguration<Trainer>
    {
        public new void Configure(EntityTypeBuilder<Trainer> builder)
        {

            builder.Property(X => X.CreatedAt)
                .HasColumnName("HireData")
                .HasDefaultValueSql("GETDATE()");

            base.Configure(builder);
        }
    }
}
