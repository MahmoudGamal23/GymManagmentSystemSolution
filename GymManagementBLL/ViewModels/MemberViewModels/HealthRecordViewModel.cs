using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.MemberViewModels
{
    public class HealthRecordViewModel
    {
        [Required(ErrorMessage = "Height is Required")]
        [Range(0.1,300, ErrorMessage = "Height Must be Between 0.1 And 300")]
        public decimal Height { get; set; }
        [Required(ErrorMessage = "Weight is Required")]
        [Range(0.1, 500, ErrorMessage = "Weight Must be Between 0.1 And 500")]
        public decimal Weight { get; set; }
        [Required(ErrorMessage = "Blood Type is required")]
        [RegularExpression(@"^(A|B|AB|O)[+-]$", ErrorMessage = "Blood Type must be one of the following: A+, A-, B+, B-, AB+, AB-, O+, O-")]
        [StringLength(3, ErrorMessage = "Blood Type must be Between  3 Characters or 1")]
        public string BloodType { get; set; } = null!;

        public string? Note { get; set; }

    }
}
