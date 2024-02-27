using BiblioTech.API.Entities;
using System.Text.Json.Serialization;

namespace BiblioTech.API.Models.ViewModels
{
    public class LoanViewModel
    {
        public string? ClientName { get;  set; }
        public string ? BookName { get;  set; }
        public string LoanDate { get;  set; } 
        public string Devolution { get;  set; } 
    }
   
}
