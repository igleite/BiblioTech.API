namespace BiblioTech.API.Models.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public bool Emprestado { get; set; }
        public UserViewModel? EmprestadoPara { get; set; }
    }
}
