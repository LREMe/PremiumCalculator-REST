using Microsoft.AspNetCore.Mvc;

namespace TranzactProgrammingChallengeWebAPI.Data
{

    /// <summary>
    /// Claas to get parameters from the client
    /// </summary>
    public class PremiumQ
    {
        [FromQuery(Name = "DoB")]
        public DateTime DoB { get; set; }
        [FromQuery(Name = "State")]
        public string? State { get; set; }

        [FromQuery(Name = "Age")]
        public int Age { get; set; }

        [FromQuery(Name = "Plan")]
        public string? Plan { get; set; }

    }
}
