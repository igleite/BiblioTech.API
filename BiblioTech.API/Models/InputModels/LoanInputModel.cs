using BiblioTech.API.Entities;
using System.Text.Json.Serialization;

namespace BiblioTech.API.Models.InputModels
{
    public class LoanInputModel
    {
        public int IdClient { get;  set; }
        public int IdBook { get;  set; }
 
    }
}
