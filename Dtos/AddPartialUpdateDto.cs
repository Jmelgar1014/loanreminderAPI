namespace LoanAPI.Dtos;


public class AddPartialUpdateDto
{
    public string? Vendor {get; set;}
    public string? Type {get;set;}

    public string? Description {get; set;}
    public decimal? Amount {get;set;}
    public int? DueDate {get;set;}
    public bool? Autopay {get;set;}
    public bool? LoanFinished {get;set;}
}