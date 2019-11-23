using System;

namespace UITests.ApplicationUnderTest.Models
{
    public class PersonalDetails
    {
        public PersonalDetails()
        {
            Address = new Address();
        }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Intermediary { get; set; }
        public string NationalInsuranceNumber { get; set; }
        public Address Address { get; set; }
    }
}
