namespace EReceipt.DAL.Entities
{
    public class Confidant
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Passport { get; set; }

        public Patient Patient { get; set; }
    }
}
