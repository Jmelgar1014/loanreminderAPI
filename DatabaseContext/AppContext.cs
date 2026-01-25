using LoanAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace LoanAPI.DatabaseContext;



public class LoanDbContext : DbContext
{
    public LoanDbContext(DbContextOptions<LoanDbContext> options) : base(options)
    {

    }


    public DbSet<AddLoan> Loans {get;set;}
}


