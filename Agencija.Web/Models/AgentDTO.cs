namespace Agencija.Web.Models
{
    public class AgentDTO
    {
        public int ID { get; set; } 
        public string FullName {  get; set; }
        public string Contact { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int EstatesSold { get; set; }
        public int? WorkExperience { get; set; }
    }
}
