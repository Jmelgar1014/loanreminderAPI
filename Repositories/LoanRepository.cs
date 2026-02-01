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

    public async Task<IEnumerable<AddLoan>> GetAllLoans(string? vendor, decimal? minAmount, decimal? maxAmount, bool? loanFinished)
    {

        var query = _dbContext.Loans.AsQueryable();

        if(!string.IsNullOrEmpty(vendor))
        {
            query = query.Where(l => l.Vendor == vendor);
        }

        if(minAmount.HasValue)
        {
            query = query.Where(l => l.Amount >= minAmount);
        }

        if(maxAmount.HasValue)
        {
            query = query.Where(l => l.Amount <= maxAmount);
        }

        if(loanFinished.HasValue)
        {
            query = query.Where(l => l.LoanFinished == loanFinished);
        }

        return await query.ToListAsync();
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

    public async Task<AddLoan> UpdateLoan(int id, AddLoan updatedLoan)
    {
        var loan = await _dbContext.Loans.FirstOrDefaultAsync(loan => loan.Id == id);

        if(loan == null)
        {
            throw new KeyNotFoundException($"Loan with id: {id} was not found");
        }


        loan.Amount = updatedLoan.Amount;
        loan.Autopay = updatedLoan.Autopay;
        loan.Description = updatedLoan.Description;
        loan.DueDate = updatedLoan.DueDate;
        loan.LoanFinished = updatedLoan.LoanFinished;
        loan.Type = updatedLoan.Type;
        loan.Vendor = updatedLoan.Vendor;

        await _dbContext.SaveChangesAsync();

        return loan;
    }

    public async Task<AddLoan> UpdateLoanPatch(int id, AddPartialUpdateDto updatedLoan)
    {
        var loan = await _dbContext.Loans.FirstOrDefaultAsync(loan => loan.Id == id);

        if(loan == null)
        {
            throw new KeyNotFoundException($"Loan with id: {id} was not found");
        }

      if (updatedLoan.Vendor != null) loan.Vendor = updatedLoan.Vendor;
      if (updatedLoan.Type != null) loan.Type = updatedLoan.Type;
      if (updatedLoan.Description != null) loan.Description = updatedLoan.Description;
      if (updatedLoan.Amount.HasValue) loan.Amount = updatedLoan.Amount.Value;
      if (updatedLoan.DueDate.HasValue) loan.DueDate = updatedLoan.DueDate.Value;
      if (updatedLoan.Autopay.HasValue) loan.Autopay = updatedLoan.Autopay.Value;
      if (updatedLoan.LoanFinished.HasValue) loan.LoanFinished = updatedLoan.LoanFinished.Value;

      await _dbContext.SaveChangesAsync();

      return loan;

    }
}