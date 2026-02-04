using System.ComponentModel.DataAnnotations;

namespace LoanAPI.Dtos;



public class AddLoanDto
{
    [Required]
    required public string Vendor {get; set;}
    [Required]
    required public string Type {get;set;}

    public string? Description {get; set;}
    [Required]
    required public decimal Amount {get;set;}
    [Required]
    required public int DueDate {get;set;}
    [Required]
    required public bool Autopay {get;set;}
    [Required]
    required public bool LoanFinished {get;set;}
}