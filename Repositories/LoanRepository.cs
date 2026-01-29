using LoanAPI.DatabaseContext;
using LoanAPI.Dtos;
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

    public async Task<AddLoan> GetLoanById(int id)
    {
        var result = await _dbContext.Loans.FirstOrDefaultAsync(loanId => loanId.Id == id);

        if(result == null)
        {
            throw new KeyNotFoundException($"Loan with ID {id} not found");
        }

        return result;

    }

    public async Task DeleteLoanById(int id)
    {
        var loan = await _dbContext.Loans.FirstOrDefaultAsync(loan => loan.Id == id);

        if(loan == null)
        {
            throw new KeyNotFoundException($"Loan with id: {id} not found");
        }

        _dbContext.Loans.Remove(loan);

        await _dbContext.SaveChangesAsync();
    }
}