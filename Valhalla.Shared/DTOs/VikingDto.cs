using System.ComponentModel.DataAnnotations;
using Valhalla.Domain.Enums;

namespace Valhalla.Shared.DTOs
{
    public class VikingDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Range(0, 999)]
        public int BattlesWon { get; set; }

        [Required]
        public string FavoriteWeapon { get; set; }

        [Required]
        public HonorLevel HonorLevel { get; set; }

        [Required]
        public string DeathCause { get; set; }

        public int ValhallaPoints { get; set; }

        public string? Category { get; set; }

        public bool IsApproved { get; set; }
    }
}
