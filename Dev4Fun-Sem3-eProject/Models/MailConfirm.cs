namespace Dev4Fun_Sem3_eProject.Models
{
    public class MailConfirm
    {
        public string UserName { get; set; }
        public string UserEmailId { get; set; }
        public int IdVacancy { get; set; }
        public int IdApplicant { get; set; }
        public DateTime Time { get; set; }
    }
}
