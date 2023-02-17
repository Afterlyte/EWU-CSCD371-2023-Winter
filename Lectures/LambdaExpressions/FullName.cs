using System.Runtime.InteropServices;

namespace LambdaExpressions;
public record FullName(string FirstName, string LastName)
{
    public FullName(string fullName) : this(null!, null!)
    {
        if (fullName.Split(" ") is [string { Length: > 0 } firstName, string { Length: > 0 } lastName])
        {
            FirstName = firstName;
            LastName = lastName;
        }
        else
        {
            throw new ArgumentException("Cannot have empty name");
        }
    }

    public string Name => $"{FirstName} {LastName}";

    public string Initials
    {
        get
        {
            string result = "";
            if (Name.Split(" ") is [ [char firstInitial, ..], [char lastInitial, ..]])
            {
                result = $"{firstInitial}{lastInitial}";
            }
            return result;
        }
    }
}
