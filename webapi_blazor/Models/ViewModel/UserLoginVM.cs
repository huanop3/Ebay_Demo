public class UserLoginVM
{
    public required string Account { get; set; }
    public required string Password { get; set; }
}

public class UserLoginResult{
    public string Account { get; set; }
    public string AccessToken { get; set; }
}