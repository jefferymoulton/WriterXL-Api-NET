namespace WriterXL.Domain.Members
{
    public class Member
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public MemberStatus Status { get; set; }
    }

    public enum MemberStatus
    {
        Active,
        Inactive,
        Deleted
    }
}