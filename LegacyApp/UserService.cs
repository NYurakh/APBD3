using System;

namespace LegacyApp
{
    public class UserService
    {
        public static bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!IsValidInput(firstName, lastName, email, dateOfBirth))
                return false;

            var client = GetClientById(clientId) ?? throw new ArgumentException($"Client with ID {clientId} not found.");

            var user = CreateUser(firstName, lastName, email, dateOfBirth, client);

            if (user.HasCreditLimit && user.CreditLimit < 500)
                return false;

            SaveUser(user);

            return true;
        }

        private static bool IsValidInput(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            return !string.IsNullOrEmpty(firstName) &&
                   !string.IsNullOrEmpty(lastName) &&
                   IsValidEmail(email) &&
                   !IsUnderAge(dateOfBirth);
        }

        private static bool IsValidEmail(string email)
        {
            return !string.IsNullOrEmpty(email) && email.Contains("@") && email.Contains(".");
        }

        private static bool IsUnderAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            var age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
                age--;

            return age < 21;
        }

        private static Client GetClientById(int clientId)
        {
            var clientRepository = new ClientRepository();
            return clientRepository.GetById(clientId);
        }

        private static User CreateUser(string firstName, string lastName, string email, DateTime dateOfBirth, Client client)
        {
            var creditLimit = GetCreditLimit(lastName, dateOfBirth);
            var hasCreditLimit = client.Type != "VeryImportantClient";

            return new User(client, dateOfBirth, email, firstName, lastName, hasCreditLimit, CalculateCreditLimit(client.Type, creditLimit));
        }

        private static int GetCreditLimit(string lastName, DateTime dateOfBirth)
        {
            using var userCreditService = new UserCreditService();
            return userCreditService.GetCreditLimit(lastName, dateOfBirth);
        }

        private static int CalculateCreditLimit(string clientType, int baseCreditLimit)
        {
            return clientType switch
            {
                "VeryImportantClient" => baseCreditLimit,
                "ImportantClient" => baseCreditLimit * 2,
                _ => baseCreditLimit
            };
        }

        private static void SaveUser(User user)
        {
            UserDataAccess.AddUser(user);
        }
    }
}
