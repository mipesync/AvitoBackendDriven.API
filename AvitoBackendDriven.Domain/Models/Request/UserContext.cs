namespace AvitoBackendDriven.Domain.Models.Request;

public class UserContext
{
    public string? UserId { get; set; }
    public required string CountryCode { get; set; }
}