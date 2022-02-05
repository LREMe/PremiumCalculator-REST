

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranzactProgrammingChallengeWebAPI.Data
{
    /// <summary>
    /// Class to map PremiumRol
    /// </summary>
    public class PremiumRol
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int PremiumRolID { get; set; }
        public string? Carrier { get; set; }
        public string? Plan { get; set; }

        public List<string>? PlanList => (Plan ?? "").Split(',').ToList();

        public string? State { get; set; }
        public string? MonthOfBirth { get; set; }
        public string? Age { get; set; }

        public double Premium { get; set; }
    }
}
