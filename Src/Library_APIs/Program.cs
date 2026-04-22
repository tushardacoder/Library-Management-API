using Library_APIs;
using Library_APIs.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


var connectionString = "Host=localhost;Port=5432;Username=postgres;Password=softwaredev;Database=LMS";
builder.Services.AddDbContext<AppDbContext>(options => {options.UseNpgsql(connectionString);
                                                        options.AddInterceptors(new AuditInterceptor());

});



//dotnet ef migrations add InitialCreate --project Src/Library_APIs/Library_APIs.csproj
//dotnet ef database update --project Src/Library_APIs/Library_APIs.csproj
var app = builder.Build();


app.MapPost("/books", async (AppDbContext context, BookRequest bookrequest) =>
{

    var bookentity = new Book
    {

        Title = bookrequest.Title,
        Author = bookrequest.Author,
        ISBN = bookrequest.ISBN,
        AvailableCopies = bookrequest.AvailableCopies

    };

       context.Books.Add(bookentity);
       await context.SaveChangesAsync();


      return Results.Ok(bookentity);

});

app.MapPost("/members", async (AppDbContext context, MemberRequest memberrequest) =>
{

    var memberentity = new Member
    {

         FullName= memberrequest.FullName,
         Email= memberrequest.Email,

    };

    context.Members.Add(memberentity);
    await context.SaveChangesAsync();


    return Results.Ok(memberentity);

});


app.MapPost("/loans", async (AppDbContext context, LoanRequest loanrequest) =>
{


    var book= await context.Books.FirstOrDefaultAsync(b=>b.Id==loanrequest.BookId);

    if (book == null) return Results.Ok("Invalid Book ID");

    var member =await context.Members.FirstOrDefaultAsync(m=>m.Id==loanrequest.MemberId);

    if (member == null) return Results.Ok("Invalid Member Id");

    if (book.AvailableCopies == 0) return Results.Ok("Book is not available");


    var loanentity = new Loan
    {
        BookId =loanrequest.BookId,

        MemberId=loanrequest.MemberId,

        BorrowedAt=loanrequest.BorrowedAt,

        DueDate=loanrequest.DueDate,

        

    };


    book.AvailableCopies -= 1;
    context.Loans.Add(loanentity);
  
    await context.SaveChangesAsync();
    return Results.Ok(loanentity);



});



app.MapGet("/loans", async (AppDbContext context) =>
{
    var loans = await context.Loans
       .Include(l => l.Book)
       .Include(l => l.Member)
       .Select(l => new
       {
           l.Id,
           BookTitle = l.Book.Title,
           MemberName = l.Member.FullName,
           l.BorrowedAt,
           l.DueDate
       })
       .ToListAsync();

    return Results.Ok(loans);
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

