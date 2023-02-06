using System;


namespace AttendanceTracker.Utils;


public class HashedPassword
{
    public HashedPassword(string password, string salt)
    {
        Password = password;
        Salt = salt;
    }

    public string Password { get; }

    public string Salt { get; }
}

