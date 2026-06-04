using Valhalla.Domain.Enums;

namespace Valhalla.Domain.Entities
{
    public class Viking
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int BattlesWon { get; set; }
        public string FavoriteWeapon { get; set; }
        public HonorLevel HonorLevel { get; set; }
        public string DeathCause { get; set; }
        public int ValhallaPoints { get; set; }
        public string Category { get; set; }
        public bool IsApproved { get; set; }
    }
}

