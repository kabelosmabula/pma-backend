using System.ComponentModel.DataAnnotations;

namespace API.Service.SharedModels
{
    public class CreatePracticePractionerShared : CreateAccountShared
    {

        [Required]
        public Guid PracticeId { get; set; }

        [Required]
        public Guid RoleId { get; set; }

        public string? Phonenumber { get; set; }
    }
}
