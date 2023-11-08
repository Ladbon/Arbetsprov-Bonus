using System;

namespace Arbetsprov_Bonus.Entities;

public class Consultant
{
    public Consultant(string firstName, string lastName, DateTime startDate, int totalHoursWorked = 0)
    {
        FirstName = firstName;
        LastName = lastName;
        StartDate = startDate;
        TotalHoursWorked = totalHoursWorked;
    }

    public int Id { get; set; }  
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public int TotalHoursWorked { get; set; } = 0;
}
