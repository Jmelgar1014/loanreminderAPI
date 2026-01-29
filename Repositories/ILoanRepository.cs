using LoanAPI.Dtos;
using LoanAPI.Models;

namespace LoanAPI.Repositories;


public interface ILoanRepository
{
    Task<AddLoan> AddLoanData(AddLoan loan);

    Task<IEnumerable<AddLoan>> GetAllLoans();

    Task<AddLoan> GetLoanById(int id);

    Task DeleteLoanById(int id);

}