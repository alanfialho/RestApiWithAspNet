using System.Collections.Generic;

namespace DatabaseIntegration.ViewModels
{
    public class PersonViewModel
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Address { get; }
        public string Gender { get; }
        public IEnumerable<LinkViewModel> Links { get; }
        
        public PersonViewModel(int id, string firstName, string lastName, string address, string gender, IEnumerable<LinkViewModel> links)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Gender = gender;
            Links = links;
        }
    }
}
