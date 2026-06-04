using Valhalla.Domain.Entities;
using Valhalla.Shared.DTOs;

namespace Valhalla.Application.Mappers
{
    public static class VikingMapper
    {
        public static VikingDto ToDto(Viking v) => new()
        {
            Id = v.Id,
            Name = v.Name,
            BattlesWon = v.BattlesWon,
            FavoriteWeapon = v.FavoriteWeapon,
            HonorLevel = v.HonorLevel,
            DeathCause = v.DeathCause,
            ValhallaPoints = v.ValhallaPoints,
            Category = v.Category,
            IsApproved = v.IsApproved
        };

        public static Viking ToEntity(VikingDto dto) => new()
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            BattlesWon = dto.BattlesWon,
            FavoriteWeapon = dto.FavoriteWeapon,
            HonorLevel = dto.HonorLevel,
            DeathCause = dto.DeathCause
        };

        public static void ApplyChanges(Viking target, VikingDto dto)
        {
            target.Name = dto.Name;
            target.BattlesWon = dto.BattlesWon;
            target.FavoriteWeapon = dto.FavoriteWeapon;
            target.HonorLevel = dto.HonorLevel;
            target.DeathCause = dto.DeathCause;
        }
    }
}
