using System.Text.Json.Serialization;

namespace wpf.ui.model.payloads
{
    public class LoginData
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("accessToken")]
        public string? AccessToken { get; set; }

        [JsonPropertyName("refreshToken")]
        public string? RefreshToken { get; set; }

        [JsonPropertyName("expiresTime")]
        public long ExpiresTime { get; set; }
    }
}
