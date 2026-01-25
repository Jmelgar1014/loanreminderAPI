namespace LoanAPI.Models;


public class AddLoan {

    required public int Id {get;set;}
    required public string Vendor {get; set;}

    required public string Type {get;set;}

    public string? Description {get; set;}

    required public decimal Amount {get;set;}

    required public DateTime DueDate {get;set;}

    required public bool Autopay {get;set;}

    required public bool LoanFinished {get;set;}

}