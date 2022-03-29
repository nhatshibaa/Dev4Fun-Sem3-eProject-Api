namespace Dev4Fun_Sem3_eProject.Models
{
    public class MailNotice
    {
        public string UserName { get; set; }
        public string UserEmailId { get; set; }
        public int IdApplicant { get; set; }
        public int IdMentor { get; set; }
        public int IdHr { get; set; }
        public int IdVacancy { get; set; }
        public DateTime Time { get; set; }
        public string InterviewLocation { get; set; }

        //public string UserName { get; set; }
        //public string UserEmailId { get; set; }
        //public string Vacancy { get; set; }
        //public string Time { get; set; }
        //public string Who { get; set; }
        //public string InterviewLocation { get; set; }
    }
}
