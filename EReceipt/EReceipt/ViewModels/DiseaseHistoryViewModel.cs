namespace EReceipt.ViewModels
{
    public class DiseaseHistoryViewModel
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public string Description { get; set; }

        public SelectViewModel Patient { get; set; }
    }
}
