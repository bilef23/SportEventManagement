using SportEvents.Enum;

namespace Domain.ViewModels;


public class ParticipantViewModel
{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
}
