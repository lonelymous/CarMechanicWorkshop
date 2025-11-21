using System.Text.RegularExpressions;

namespace CarMechanicWorkshop.Domain.ValueObjects;

public sealed class Email
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new Exception("Email cannot be empty.");

        if (!IsValid(email))
            throw new Exception("Email format is invalid.");

        return new Email(email);
    }

    public static bool IsValid(string email)
    {
        return Regex.IsMatch(email,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.IgnoreCase);
    }

    public override string ToString() => Value;
}
