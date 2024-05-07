namespace BiblioTech.API.Models.InputModels
{
    public class BookInputModelUpdate
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
    }
}
