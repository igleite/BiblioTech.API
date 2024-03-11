namespace BiblioTech.API.Entities
{
    public class User
    {
        public User()
        {

        }
        public User(string cpf, string name, string email, DateTime? blockedDate)
        {
            Cpf = cpf;
            Name = name;
            Email = email;
            BlockedDate = blockedDate;
        }
        public int Id { get; private set; }
        public string Cpf {  get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime? BlockedDate { get; private set; }

        public void UpdateUser(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public void SetBlockedDate(DateTime? blockedDate)
        {
            BlockedDate = blockedDate;
        }

        public void RemoveBlockedDate()
        {
            BlockedDate = null;
        }
    }
}
