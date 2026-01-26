using LoanAPI.DatabaseContext;
using LoanAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanAPI.Repositories;


public class LoanRepository : ILoanRepository
{
    private readonly LoanDbContext _dbContext;

    public LoanRepository(LoanDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AddLoan> AddLoanData(AddLoan loan)
    {
        _dbContext.Loans.Add(loan);
        await _dbContext.SaveChangesAsync();
        return loan;
    }

    public async Task<IEnumerable<AddLoan>> GetAllLoans()
    {
        return await _dbContext.Loans.ToListAsync();
    }
}