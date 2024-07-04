namespace Users.Models;

public class User{
    public int Id {get; set;}
    public string Name {get; set;}
    public string Address {get; set;}
    public string Phone {get; set;}
    public DateTime Birthday {get; set;}

    // Nye felt som skal lagres i databasen
    public int PhoneDigitSum {get; set;}
    public bool IsLeapYearBirthday {get; set;}

       public void CalculatePhoneDigitSum()
    {
        PhoneDigitSum = Phone.Where(char.IsDigit).Sum(c => c - '0');
    }

    public void CalculateIsLeapYearBirthday()
    {
        int year = Birthday.Year;
        IsLeapYearBirthday = DateTime.IsLeapYear(year);
    }

    // Kall denne metoden for å oppdatere begge beregningene
    public void UpdateCalculations()
    {
        CalculatePhoneDigitSum();
        CalculateIsLeapYearBirthday();
    }
}