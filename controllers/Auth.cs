using BCrypt.Net;

public class Auth
{
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
    public static bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
    public static string GenerateToken()
    {
        return Guid.NewGuid().ToString();
    }
}