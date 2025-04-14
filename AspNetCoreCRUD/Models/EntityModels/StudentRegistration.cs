using System.ComponentModel.DataAnnotations;

namespace AspNetCoreCRUD.Models.EntityModels
{
    public class StudentRegistration
    {
        [Key]
        public int StudentId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public DateTime StudentDob { get; set; }
        public string Gender { get; set; }
        public int StudentFee { get; set; }
        public bool is_active { get; set; }
        public int? created_by { get; set; }
        public DateTime? created_date { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
    }

}
