namespace EReceipt.DAL.Entities
{
    public class DiseaseHistory
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public string Description { get; set; }

        public Patient Patient { get; set; }
    }
}
