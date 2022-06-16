namespace KCommerceAPI.Models
{
    public class TokenRelatedSettings
    {
        public int NoOfDaysRefreshTokenValid { get; set; }

        public string SecurityKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int AccessTokenExpiresMin { get; set; }
    }
}