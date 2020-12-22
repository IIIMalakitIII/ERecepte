namespace EReceipt.ViewModels
{
    public class ConfidantViewModel
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Passport { get; set; }

        public SelectViewModel Patient { get; set; }
    }
}
