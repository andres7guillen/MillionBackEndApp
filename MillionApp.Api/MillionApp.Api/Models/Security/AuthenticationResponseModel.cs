namespace MillionApp.Api.Models.Security;

public class AuthenticationResponseModel
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public string EmailUser { get; set; } = string.Empty;
    public string ConnectionId { get; set; } = string.Empty;
    public string GenderId { get; set; } = string.Empty;
    public string UserKey { get; set; }
}
