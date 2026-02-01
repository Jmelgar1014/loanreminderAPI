using AutoMapper;
using LoanAPI.Dtos;
using LoanAPI.Models;
using LoanAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LoanAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LoanController : ControllerBase
{

    private readonly ILogger<LoanController> _logger;
    
    private readonly ILoanRepository _loanRepository;

    private readonly IMapper _mapper;

    public LoanController(ILogger<LoanController> logger, ILoanRepository loanRepository, IMapper mapper)
    {
        _logger = logger;

        _loanRepository = loanRepository;

        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<AddLoanDto>> AddData(AddLoanDto loan)
    {
        if(!ModelState.IsValid)
        {
            _logger.LogError("Provided Data is not the correct shape.");
            return BadRequest(ModelState);
        }

        var userRequest = _mapper.Map<AddLoan>(loan);

        _logger.LogInformation("User Request has been mapped to Model");

        var result = await _loanRepository.AddLoanData(userRequest);

        _logger.LogInformation("Data has been added to the database");

        var response = _mapper.Map<LoanResponseDto>(result);

        return Created("",response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LoanResponseDto>>> GetAllLoanData([FromQuery]string? vendor, [FromQuery]decimal? minAmount, [FromQuery]decimal? maxAmount, [FromQuery]bool? loanFinished)
    {
        var result = await _loanRepository.GetAllLoans(vendor,minAmount,maxAmount,loanFinished);

        var response = _mapper.Map<IEnumerable<LoanResponseDto>>(result);

        return Ok(response);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<LoanResponseDto>> LoanById(int id)
    {
        var loan = await _loanRepository.GetLoanById(id);

        var response = _mapper.Map<LoanResponseDto>(loan);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLoan(int id)
    {
        await _loanRepository.DeleteLoanById(id);

        return Ok(new {Response = new {Message = "Loan was successfully deleted"}});

    }

    [HttpPut("{id}")]
    public async Task<ActionResult<LoanResponseDto>> UpdateLoanDetails(int id, AddLoanDto updatedLoanDetails)
    {
        var updatedInformation = _mapper.Map<AddLoan>(updatedLoanDetails);

        var updatedRecord = await _loanRepository.UpdateLoan(id, updatedInformation);

        var updateResponse = _mapper.Map<LoanResponseDto>(updatedRecord);

        return Ok(updateResponse);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<LoanResponseDto>> UpdateLoanDetailsPatch(int id, AddPartialUpdateDto updatedLoanDetailsPatch)
    {
        var updatedLoanDetails = await _loanRepository.UpdateLoanPatch(id, updatedLoanDetailsPatch);

        var response = _mapper.Map<LoanResponseDto>(updatedLoanDetails);

        return Ok(response);
    }
}
