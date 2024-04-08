namespace LegacyApp
{
    public class Client
    {
        public string Name { get; }
        public int ClientId { get; }
        public string Email { get; }
        public string Address { get; }
        public string Type { get; }

        public Client(string name, int clientId, string email, string address, string type)
        {
            Name = name;
            ClientId = clientId;
            Email = email;
            Address = address;
            Type = type;
        }
    }
}
