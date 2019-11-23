using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UITests.ApplicationUnderTest.Models;

namespace UITests.Specflow
{
    [Binding]
    public class StepArgumentTransformations
    {
        [StepArgumentTransformation]
        public PersonalDetails MapTableToPersonalDetails(Table table)
        {
            var personalDetails = table.CreateInstance<PersonalDetails>();

            var age = Convert.ToInt32(table.Rows[0]["Age"]);

            personalDetails.DateOfBirth = DateTime.Today.AddYears(-age);

            personalDetails.Address = MapTableToAddress(table);

            return personalDetails;
        }

        private Address MapTableToAddress(Table table)
        {
            var address = table.CreateInstance<Address>();

            var moveInMonth = Convert.ToInt32(table.Rows[0]["MoveInDateMonth"]);
            var moveInYear = Convert.ToInt32(table.Rows[0]["MoveInDateYear"]);

            var moveInDate = new DateTime(moveInYear, moveInMonth, 1);

            address.MoveInDate = moveInDate;

            return address;
        }
    }
}
