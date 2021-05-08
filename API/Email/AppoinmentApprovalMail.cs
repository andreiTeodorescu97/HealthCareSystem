namespace API.Email
{
    public class AppoinmentApprovalMail
    {
        public int AppoinmentId { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorSecondName { get; set; }
        public string AppoinmentDate { get; set; }
        public string ToEmail { get; set; }

    }
}