using AutoMapper;
using LoanAPI.Dtos;
using LoanAPI.Models;

namespace LoanAPI.Profiles;



public class LoanProfile : Profile
{
    public LoanProfile()
    {
        CreateMap<AddLoanDto, AddLoan>();

        CreateMap<AddLoan, LoanResponseDto>();
    }
}