using LoanAPI.Models;

namespace LoanAPI.Repositories;


public interface ILoanRepository
{
    Task<AddLoan> AddLoanData(AddLoan loan);

    Task<IEnumerable<AddLoan>> GetAllLoans();
}