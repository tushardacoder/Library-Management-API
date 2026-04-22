using Library_APIs.Entities;

namespace Library_APIs
{
    public class UserRequest
    {

    }


    public class BookRequest
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public int AvailableCopies { get; set; }

    }

    public class MemberRequest
    {
        public string FullName { get; set; }

        public string Email { get; set; }

       

    }


    public class LoanRequest
    {
      

        public int BookId { get; set; }

        public int MemberId { get; set; }

        public DateTime BorrowedAt { get; set; }

        public DateTime DueDate { get; set; }


    }
}
