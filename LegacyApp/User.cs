using System;

namespace LegacyApp
{
    public class User
    {
        public object Client { get; }
        public DateTime DateOfBirth { get; }
        public string EmailAddress { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public bool HasCreditLimit { get; }
        public int CreditLimit { get; }

        public User(object client, DateTime dateOfBirth, string emailAddress, string firstName, string lastName, bool hasCreditLimit, int creditLimit)
        {
            Client = client;
            DateOfBirth = dateOfBirth;
            EmailAddress = emailAddress;
            FirstName = firstName;
            LastName = lastName;
            HasCreditLimit = hasCreditLimit;
            CreditLimit = creditLimit;
        }
    }
}
