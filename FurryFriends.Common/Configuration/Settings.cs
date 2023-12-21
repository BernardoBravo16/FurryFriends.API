namespace FurryFriends.Common.Configuration;

public class ConnectionStrings
{
    public string DefaultConnectionString { get; set; }
}

public class Jwt
{
    public int MinutesToExpire { get; set; }
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}