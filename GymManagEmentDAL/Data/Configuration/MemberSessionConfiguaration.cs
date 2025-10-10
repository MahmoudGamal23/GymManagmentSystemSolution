using GymManagEmentDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagEmentDAL.Data.Configuration
{
    internal class MemberSessionConfiguaration : IEntityTypeConfiguration<MemberSession>
    {
        public void Configure(EntityTypeBuilder<MemberSession> builder)
        {

            builder.Property(X => X.CreatedAt)
                 .HasColumnName("BookingDate")
                 .HasDefaultValueSql("GETDATE()");
            builder.HasKey(X => new { X.MemberId, X.SessionId });
            builder.Ignore(X => X.Id);
        }
    }
}
