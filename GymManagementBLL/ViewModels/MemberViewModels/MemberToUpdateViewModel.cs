using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.MemberViewModels
{
    public class MemberToUpdateViewModel
    {
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        [DataType(DataType.EmailAddress)]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email Must be Between 5 And 100 Char")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Email Is Required")]
        [Phone(ErrorMessage = "Invalid Phone Format")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone Must be Valid Egyption PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Bulding Number Is Required")]
        [Range(1, 9000, ErrorMessage = "Bulding Number Must be Between 1 And 9000")]
        public int BuldingNumber { get; set; }

        [Required(ErrorMessage = "Street Is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Street Must be Between 2 And 30 Char")]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "City Is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "City Must be Between 2 And 30 Char")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City Can Contain Only Letters And White Spaces")]
        public string City { get; set; } = null!;
        public string? Photo { get; set; } 
    }
}
