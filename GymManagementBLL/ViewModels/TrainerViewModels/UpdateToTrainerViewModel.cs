using GymManagEmentDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.TrainerViewModels
{
    internal class UpdateToTrainerViewModel
    {
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Phone Is Required")]
        [Phone(ErrorMessage = "Invalid Phone Format")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone Must be Valid Egyption PhoneNumber")]
        public string Phone { get; set; } = null!;
        [Required(ErrorMessage = "Bulding Number Is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Bulding Number Must be Greater than 0")]
        public int BuildingNumber { get; set; }


        [Required(ErrorMessage = "City Is Required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "City Must be Between 2 And 100 Char")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City Can Only Contain Letters And Spaces")]
        public string City { get; set; } = null!;
        [Required(ErrorMessage = "Street Is Required")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Street Must be Between 2 And 150 Char")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Street Can Only Contain Letters And Spaces")]

        public string Street { get; set; } = null!;
        [Required(ErrorMessage = "Specialty Is Required")]
        public Specialties Specialties { get; set; }
    }
}
