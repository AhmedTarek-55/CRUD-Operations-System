namespace Data_Access_Tier.Entities
{
    public class Email
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Recipient { get; set; }

    }
}
