using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.PlanViewModels
{
    internal class UpdatedPlanViewModel
    {
        //[Required(ErrorMessage = "Plan Name Is Required")]
        //[StringLength(50, MinimumLength = 2, ErrorMessage = "Plan Name Must be Between 2 And 50 Char")]
        public string PlanName { get; set; } = null!;
        [Required(ErrorMessage = "Description Is Required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Description Must be Between 2 And 50 Char")]
        public string Description { get; set; } = null!;
        [Required(ErrorMessage = "Duration Days Is Required")]
        [Range(1, 365, ErrorMessage = "Duration Days Must be Between 1 And 365")]
        public int DurationDays { get; set; }
        [Required(ErrorMessage = "Price Is Required")]
        [Range(0.1 ,10000 , ErrorMessage = "Price Days Must be Between 0.1 And 10000")]
        public decimal Price { get; set; }
    }
}
