namespace Library_APIs.Entities
{
    public class Loan
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int MemberId { get; set; }

        public DateTime BorrowedAt { get; set; }

        public DateTime DueDate { get; set; }

        public Book Book { get; set; }

        public Member Member { get; set; }
    }
}
