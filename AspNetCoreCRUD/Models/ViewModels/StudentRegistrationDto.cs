using System.ComponentModel.DataAnnotations;
namespace AspNetCoreCRUD.Models.ViewModels
{
    public class StudentRegistrationDto
    {
        public StudentRegistrationDto()
        {
            studentRegistrationList = new List<StudentRegistrationDto>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; } = string.Empty;
        public DateTime Dob { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Fee must be a non-negative number.")]
        public int Fee { get; set; }

        public List<StudentRegistrationDto> studentRegistrationList { get; set; }
    }
}

