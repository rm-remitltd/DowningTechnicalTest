using System;
using UITests.ApplicationUnderTest.Models;

namespace UITests.ApplicationUnderTest.TestData
{
    internal static class Investors
    {
        internal static PersonalDetails MrTest => new PersonalDetails
        {
            Title = "Mr",
            FirstName = "Bob",
            LastName = "Test",
            DateOfBirth = DateTime.Now.AddYears(-20),
            Intermediary = "Mr Test Intermediary",
            PhoneNumber = "07795111222",
            Address = new Address { Postcode = "CF31 5DD", MoveInDate = DateTime.Now.AddYears(-10) }
        };
    }
}
