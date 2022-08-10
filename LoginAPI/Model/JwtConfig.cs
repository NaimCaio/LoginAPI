namespace LoginAPI.Model
{
    public class JwtConfig
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SigningKey { get; set; }

        public double Timeout { get; set; }

        public JwtConfig( string issuer, string audience, string signingKey, double timeOut)
        {
            Issuer = issuer;
            Audience = audience;
            SigningKey = signingKey;
            Timeout = timeOut;
        }
    }
}
