using System;

namespace LegacyApp;

public class UserValidator
{
    public bool Validate(string firstName, string lastName, string email, DateTime dateOfBirth, out int age)
    {
        age = CalculateAge(dateOfBirth);
        
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            return false;

        if (!email.Contains('@') || !email.Contains('.'))
            return false;

        return true;
    }

    private int CalculateAge(DateTime dateOfBirth)
    {
        var now = DateTime.Now;
        int age = now.Year - dateOfBirth.Year;
        if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
            age--;

        return age;
    }
}